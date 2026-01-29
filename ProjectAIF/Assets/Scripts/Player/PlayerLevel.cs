using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Events;

public class PlayerLevel : MonoBehaviour
{
    [Header("Player Level Setting")]
    [SerializeField] private int _startLevel;

    [Header("Player Exp Rule")]
    [Tooltip("플레이어가 1에서 2레벨 될때 필요한 경험치양")]
    [SerializeField] private int _playerLevelupExp;

    [Tooltip("증가량")]
    [SerializeField] private int _playerExpIncrease;

    //레벨
    public int CurrentLevel { get; private set; }
    //현재 누적된 경험치
    public int CurrentExp { get; private set; }
    //현재레벨에서 필요한 경험치
    public int CurrentMaxExp { get; private set; }

    public UnityEvent<int> OnLevelChange;
    public UnityEvent<int, int> OnExpbarChange;
    public UnityEvent<int> OnLevelUp;

    private int MaxPlayerLevel => GameManager.Instance.MaxPlayerLevel;

    // 연속 레벨업 가능 횟수
    public int PendingLevel { get; private set; }

    // 어빌리티 선택창이 열려있는 동안 true (열려있으면 다음 레벨업 진행 X)
    public bool isOpenAblity { get; private set; }

    private void Awake()
    {
        // 인스펙터에서 이벤트를 비워둬도 NullReference 안 나게 초기화
        if (OnLevelChange == null) OnLevelChange = new UnityEvent<int>();
        if (OnExpbarChange == null) OnExpbarChange = new UnityEvent<int, int>();
        if (OnLevelUp == null) OnLevelUp = new UnityEvent<int>();
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        CurrentLevel = _startLevel;
        CurrentExp = 0;
        CurrentMaxExp = ExpCalc(CurrentLevel);

        PendingLevel = 0;
        isOpenAblity = false;

        // TODO(UI): 게임 시작 시 경험치바/레벨 텍스트 초기화 필요
        // - OnExpbarChange(int curExp, int maxExp) -> EXP 게이지/텍스트 갱신 함수 연결
        // - OnLevelChange(int level) -> 레벨 텍스트 갱신 함수 연결
        // UI 갱신 이벤트
        OnExpbarChange?.Invoke(CurrentExp, CurrentMaxExp);
        OnLevelChange?.Invoke(CurrentLevel);
    }

    public void AddExp(int Value)
    {
        if (Value <= 0) return;
        if (CurrentLevel >= MaxPlayerLevel) return;

        CurrentExp += Value;

        // 현재 경험치로 가능한 레벨업 횟수 계산
        RecalculatePendingLevel();

        // 어빌리티 창이 안 열려있으면 레벨업을 1번만 처리하고 창을 띄움
        if (!isOpenAblity)
        {
            TryProcessOneLevelUp();
        }


        // 최종 경험치 바 갱신
        // TODO(UI): 경험치 획득 시 경험치바 갱신 필요
        OnExpbarChange?.Invoke(CurrentExp, CurrentMaxExp);
    }

    /// <summary>
    /// 어빌리티 선택창에서 선택 완료 시 호출
    /// - PendingLevel이 남아있으면 다시 레벨업 1번 처리 -> 선택창 또 뜸
    /// </summary>
    public void ConfirmAbility()
    {
        // TODO(UI): 어빌리티 선택 완료 버튼에서 이 메서드를 호출해야 함

        isOpenAblity = false;

        TryProcessOneLevelUp();

        OnExpbarChange?.Invoke(CurrentExp, CurrentMaxExp);
    }

    /// <summary>
    /// 현재 경험치로 레벨업 가능한 횟수를 계산해서 PendingLevel에 넣는다.
    /// </summary>
    private void RecalculatePendingLevel()
    {
        int simLevel = CurrentLevel;
        int simExp = CurrentExp;
        int simMax = CurrentMaxExp;

        int count = 0;

        while (simExp >= simMax && simLevel < MaxPlayerLevel)
        {
            simExp -= simMax;
            simLevel++;
            count++;
            simMax = ExpCalc(simLevel);
        }

        PendingLevel = count;
    }

    /// <summary>
    /// 레벨업을 "딱 1번만" 실제 적용하고, 바로 어빌리티 창 상태로 전환
    /// </summary>
    private void TryProcessOneLevelUp()
    {
        if (PendingLevel <= 0) return;
        if (CurrentLevel >= MaxPlayerLevel) return;

        // 레벨업 1번 적용
        CurrentExp -= CurrentMaxExp;
        CurrentLevel++;

        CurrentMaxExp = ExpCalc(CurrentLevel);

        // Pending 다시 계산
        RecalculatePendingLevel();


        // UI 갱신 이벤트
        // - OnLevelChange(int level) -> 레벨 텍스트 갱신
        // - OnExpbarChange(int curExp, int maxExp) -> EXP 게이지/텍스트 갱신
        OnLevelChange?.Invoke(CurrentLevel);
        OnExpbarChange?.Invoke(CurrentExp, CurrentMaxExp);

        // 어빌리티 선택창 띄우기 트리거
        // TODO(UI): 어빌리티 선택창 오픈 트리거
        // - OnLevelUp(int level) 이벤트에 "어빌리티 선택 UI Open()" 함수 연결
        // - 이 이벤트가 호출되면 게임을 일시정지(Time.timeScale=0 등)할지, 커서 표시할지 UI담당자가가 결정
        OnLevelUp?.Invoke(CurrentLevel);

        // 창이 열려있는 동안에는 다음 레벨업 진행 금지
        isOpenAblity = true;
    }

    private int ExpCalc(int level)
    {
        int calc = _playerLevelupExp + (_playerExpIncrease * (level - 1));
        return Mathf.Max(1, calc);
    }
#if UNITY_EDITOR
    [ContextMenu("DEBUG/Force OnLevelUp")]
    private void DebugForceOnLevelUp()
    {
        Debug.Log("[PlayerLevel] Force OnLevelUp");
        OnLevelUp?.Invoke(CurrentLevel);
    }
#endif
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            int before = CurrentLevel;
            Debug.Log($"[PlayerLevel] DEBUG AddExp 999 (before level = {before})");

            AddExp(300);

            int after = CurrentLevel;
            Debug.Log($"[PlayerLevel] after level = {after}, level gained = {after - before}");
        }
    }
#endif


}
