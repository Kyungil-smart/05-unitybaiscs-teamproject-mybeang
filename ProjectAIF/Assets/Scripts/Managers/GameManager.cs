using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : SingleTon<GameManager>
{
    // Game Data
    public bool IsGameOver { get; private set; }
    public UnityEvent OnGameOver;
    public int StageNumber { get; private set; }
    public bool IsPaused;
    public bool IsOpenedAbilityManagerUI;
    private bool _isGameStart;
    private bool _isGameOverScene;
    private int _timerSeconds;
    public UnityEvent OnTimerSecondsChanged = new UnityEvent();
    public int TimerSeconds
    {
        get { return _timerSeconds; }
        set
        {
            _timerSeconds = value;
            OnTimerSecondsChanged?.Invoke();
        }
    }
    public int GameTime;

    // Game Objects Data
    [SerializeField] private int _maxPlayerLevel = 100;
    public int MaxPlayerLevel => _maxPlayerLevel;
    
    // 아래 값을 이용하여 Enemy 가
    // Player 를 쫒아 갈 것/공격 할 것 인지
    // 혹은 Crystal 을 쫒아 갈 것/공격할 것 인지
    // 결정하게 됨.
    public bool IsCrystalNearPlayer = true;

    private void Awake()
    {
        IsPaused = false;
        IsGameOver = false;
        _isGameStart = false;
        _isGameOverScene = false;
        SingleTonInit();
    }

    private void Update()
    {
        // 한 게임당 시간 600 초
        if (_isGameStart && _timerSeconds <= 0 && !_isGameOverScene)
        {
            StartCoroutine(WaitSomeSeconds());
            GameClear();
        }
    }
    
    public void GameOver()
    {
        IsGameOver = true;
        _isGameOverScene = true;
        StopAllCoroutines();
        SceneManager.LoadScene(3);
        OnGameOver?.Invoke();
    }

    public void GameClear()
    {
        StopAllCoroutines();
        _isGameOverScene = true;
        SceneManager.LoadScene(3);
    }

    public void GoToNextStage()
    {
        StageNumber++;
    }

    public void GameStart()
    {
        TimerSeconds = GameTime;
        _isGameStart = true;
        StartCoroutine(GameTimer());
    }
    
    private IEnumerator GameTimer()
    {
        while (!IsGameOver)
        {
            yield return YieldContainer.WaitForRealSeconds(1f);
            if (!IsPaused) TimerSeconds--;
        }
        yield return null;
    }

    private IEnumerator WaitSomeSeconds()
    {
        yield return YieldContainer.WaitForRealSeconds(1f);
    }
}
