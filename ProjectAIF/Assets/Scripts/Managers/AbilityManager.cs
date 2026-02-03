using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;
using System.Linq;

/// <summary>
/// 어빌리티 추가시,
/// - "Ability Value" 에 Range 관련 데이터 추가
/// - Enum 에 어빌리티 이름 추가
/// - AbilityBase 를 상속받는 Ability 본체 추가
/// </summary>
public class AbilityManager : SingleTon<AbilityManager>
{
    // Ability Values
    [Header("Ability Value")]
    public IntRange HpRange;
    public FloatRange MoveSpeedRange;
    public IntRange DefenseRange;
    public IntRange PistolDamageRange;
    public FloatRange PistolCriticalChanceRange;
    public IntRange PistolCriticalDamageRange;
    public IntRange PistolMagazineRange;
    public IntRange RifleDamageRange;
    public FloatRange RifleCriticalChanceRange;
    public IntRange RifleCriticalDamageRange;
    public IntRange RifleMagazineRange;
    public IntRange GrenadeDamageRange;
    public IntRange GrenadeMagazineRange;
    
    // 여기가 혹시 하이라이키 프리팹
    [Header("Data Target")]
    [SerializeField] private PlayerStatus _playerStatus;
    [SerializeField] private PistolStatus pistolStatusStatus;
    [SerializeField] private RifleStatus rifleStatusStatus;
    [SerializeField] private GrenadeStatus grenadeStatusStatus;
    
    // Audio
    private AudioSource _audioSource;
    [SerializeField] AudioClip _disSound;
    [SerializeField] AudioClip _incSound;
    
    // Data 
    public Dictionary<AbilityName, Ability.AbilityBase> AbilityData = new ();
    public Dictionary<AbilityName, Ability.AbilityBase> CurrentAbilityData = new ();
    
    // UI
    [Header("UI")] 
    private bool _isSelectAbility;
    private AbilityName _selectedAbilityName;
    [SerializeField] private TextMeshProUGUI _leftAbilityText;
    [SerializeField] private TextMeshProUGUI _centerAbilityText;
    [SerializeField] private TextMeshProUGUI _rightAbilityText;

    private void Awake()
    {
        SingleTonInit();
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null) _audioSource = gameObject.AddComponent<AudioSource>();
        InitAbilityData();
    }

    private void InitAbilityData()
    {
        AbilityData.Clear();
        AbilityData.Add(AbilityName.Hp, new Ability.Hp(_playerStatus));
        AbilityData.Add(AbilityName.MoveSpeed, new Ability.MoveSpeed(_playerStatus));
        AbilityData.Add(AbilityName.Defense, new Ability.Defense(_playerStatus));
        AbilityData.Add(AbilityName.PistolDamage, new Ability.PistolDamage(pistolStatusStatus));
        AbilityData.Add(AbilityName.PistolCriticalChance, new Ability.PistolCriticalChance(pistolStatusStatus));
        AbilityData.Add(AbilityName.PistolCriticalDamage, new Ability.PistolCriticalDamage(pistolStatusStatus));
        AbilityData.Add(AbilityName.PistolMagazine, new Ability.PistolMagazine(pistolStatusStatus));
        AbilityData.Add(AbilityName.RifleDamage, new Ability.RifleDamage(rifleStatusStatus));
        AbilityData.Add(AbilityName.RifleCriticalChance, new Ability.RifleCriticalChance(rifleStatusStatus));
        AbilityData.Add(AbilityName.RifleCriticalDamage, new Ability.RifleCriticalDamage(rifleStatusStatus));
        AbilityData.Add(AbilityName.RifleMagazine, new Ability.RifleMagazine(rifleStatusStatus));
        AbilityData.Add(AbilityName.GrenadeMagazine, new Ability.GrenadeMagazine(grenadeStatusStatus));
        AbilityData.Add(AbilityName.GrenadeDamage, new Ability.GrenadeDamage(grenadeStatusStatus));
    }

    /// <summary>
    /// 버튼에서 클릭시
    /// </summary>
    /// <param name="name">Ability 이름</param>
    /// 
    // 디버깅 로그 확인용 코드 추가
    public void ApplyAbility()
    {
        if (_isSelectAbility)
        {
            PlaySound(CurrentAbilityData[_selectedAbilityName].ApplyAbility());
            Debug.Log("Applying Ability: " + _selectedAbilityName);
            // TODO: Apply 후 본래의 게임 화면으로 돌아가는 내용 추가 구현 필요    
        }
        _isSelectAbility = false;
    }
    // 로그찍어보고 싶은사람은 밑에 코드로
    //public void ApplyAbility()
    //{
    //    if (!_isSelectAbility)
    //    {
    //        Debug.LogWarning("[Ability] ApplyAbility called but _isSelectAbility is false");
    //        return;
    //    }

    //    if (CurrentAbilityData == null || !CurrentAbilityData.ContainsKey(_selectedAbilityName))
    //    {
    //        Debug.LogError($"[Ability] ApplyAbility failed: CurrentAbilityData does not contain {_selectedAbilityName}");
    //        return;
    //    }

    //    float before = Debug_GetNumericValueByAbilityName(_selectedAbilityName);

    //    bool result = CurrentAbilityData[_selectedAbilityName].ApplyAbility();

    //    float after = Debug_GetNumericValueByAbilityName(_selectedAbilityName);
    //    float delta = after - before;

    //    string signText = delta > 0f ? "증가" : (delta < 0f ? "감소" : "변화없음");

    //    if (Debug_IsFloatAbility(_selectedAbilityName))
    //    {
    //        Debug.Log($"[Ability] APPLY {_selectedAbilityName} | {signText} {delta:+0.###;-0.###;0} | BEFORE={before:0.###} -> AFTER={after:0.###} | result={result}");
    //    }
    //    else
    //    {
    //        // int 계열은 보기 좋게 반올림해서 출력
    //        int b = Mathf.RoundToInt(before);
    //        int a = Mathf.RoundToInt(after);
    //        int d = a - b;
    //        Debug.Log($"[Ability] APPLY {_selectedAbilityName} | {signText} {d:+0;-0;0} | BEFORE={b} -> AFTER={a} | result={result}");
    //    }

    //    PlaySound(result);
    //    _isSelectAbility = false;// 이거 추가했음
    //}




    private void PlaySound(bool isLucky)
    {
        if (isLucky && _incSound != null)
        {
            _audioSource?.PlayOneShot(_incSound);
        } else if (_disSound != null)
        {
            _audioSource?.PlayOneShot(_disSound);
        }
    }
    
    /// <summary>
    /// 어빌리티 중 3개 선택. 중복 없도록 조정.
    /// </summary>
    public void ReadyToThreeAbilities()
    {
        _isSelectAbility = false;
        CurrentAbilityData.Clear();
        List<AbilityName> selected = new List<AbilityName>();
        int cnt = AbilityData.Count - 1;
        for (int i = 0; i < 3; i++)
        {
            while (true)
            {
                AbilityName name = (AbilityName)Random.Range(0, cnt);
                if (!selected.Contains(name))
                {
                    selected.Add(name);
                    CurrentAbilityData.Add(name, AbilityData[name]);
                    break;
                }
            }
        }
        _leftAbilityText.text = $"{selected[0].ToString()}\n\n{CurrentAbilityData[selected[0]].Description}";
        _centerAbilityText.text = $"{selected[1].ToString()}\n\n{CurrentAbilityData[selected[1]].Description}";
        _rightAbilityText.text = $"{selected[2].ToString()}\n\n{CurrentAbilityData[selected[2]].Description}";
    }
    public void Debug_LogCurrentChoices(string tag)
    {
        if (CurrentAbilityData == null || CurrentAbilityData.Count == 0)
        {
            Debug.LogWarning($"[Ability] Choices ({tag}) => (empty)");
            return;
        }

        string keys = string.Join(", ", CurrentAbilityData.Keys.Select(k => k.ToString()));
        Debug.Log($"[Ability] Choices ({tag}) => {keys}");
    }
    //로도 확인용 로그 로그 확인후 삭제할꺼임
    private string Debug_GetValueByAbilityName(AbilityName name)
    {
        switch (name)
        {
            case AbilityName.Hp:
                return _playerStatus != null ? $"_playerStatus.TotalHp={_playerStatus.TotalHp}" : "_playerStatus=null";

            case AbilityName.MoveSpeed:
                return _playerStatus != null ? $"_playerStatus.MoveSpeed={_playerStatus.MoveSpeed}" : "_playerStatus=null";

            case AbilityName.Defense:
                return _playerStatus != null ? $"_playerStatus.Defense={_playerStatus.Defense}" : "_playerStatus=null";

            case AbilityName.PistolDamage:
                return pistolStatusStatus != null ? $"pistolStatusStatus.Damage={pistolStatusStatus.Damage}" : "pistolStatusStatus=null";

            case AbilityName.PistolCriticalChance:
                return pistolStatusStatus != null ? $"pistolStatusStatus.CriticalChance={pistolStatusStatus.CriticalChance}" : "pistolStatusStatus=null";

            case AbilityName.PistolCriticalDamage:
                // 현재 AbilityBase.cs에서 PistolCriticalDamage가 Damage를 건드리는 구조라 우선 Damage 기준으로 확인
                return pistolStatusStatus != null ? $"pistolStatusStatus.Damage={pistolStatusStatus.Damage}" : "pistolStatusStatus=null";

            case AbilityName.PistolMagazine:
                return pistolStatusStatus != null ? $"pistolStatusStatus.TotalMagazine={pistolStatusStatus.TotalMagazine}" : "pistolStatusStatus=null";

            case AbilityName.RifleDamage:
                return rifleStatusStatus != null ? $"rifleStatusStatus.Damage={rifleStatusStatus.Damage}" : "rifleStatusStatus=null";

            case AbilityName.RifleCriticalChance:
                return rifleStatusStatus != null ? $"rifleStatusStatus.CriticalChance={rifleStatusStatus.CriticalChance}" : "rifleStatusStatus=null";

            case AbilityName.RifleCriticalDamage:
                return rifleStatusStatus != null ? $"rifleStatusStatus.CriticalDamage={rifleStatusStatus.CriticalDamage}" : "rifleStatusStatus=null";

            case AbilityName.RifleMagazine:
                return rifleStatusStatus != null ? $"rifleStatusStatus.TotalMagazine={rifleStatusStatus.TotalMagazine}" : "rifleStatusStatus=null";

            case AbilityName.GrenadeDamage:
                return grenadeStatusStatus != null ? $"grenadeStatusStatus.Damage={grenadeStatusStatus.Damage}" : "grenadeStatusStatus=null";

            case AbilityName.GrenadeMagazine:
                return grenadeStatusStatus != null ? $"grenadeStatusStatus.TotalMagazine={grenadeStatusStatus.TotalMagazine}" : "grenadeStatusStatus=null";

            default:
                return "(unknown ability)";
        }
    }
    //이것도 로그 확인용 코드이다 확인후 삭제 예정
    private float Debug_GetNumericValueByAbilityName(AbilityName name)
    {
        switch (name)
        {
            case AbilityName.Hp:
                return _playerStatus != null ? _playerStatus.TotalHp : 0f;

            case AbilityName.MoveSpeed:
                return _playerStatus != null ? _playerStatus.MoveSpeed : 0f;

            case AbilityName.Defense:
                return _playerStatus != null ? _playerStatus.Defense : 0f;

            case AbilityName.PistolDamage:
                return pistolStatusStatus != null ? pistolStatusStatus.Damage : 0f;

            case AbilityName.PistolCriticalChance:
                return pistolStatusStatus != null ? pistolStatusStatus.CriticalChance : 0f;

            case AbilityName.PistolCriticalDamage:
                // 현재 AbilityBase에서 PistolCriticalDamage는 pistolStatusStatus.Damage를 변경하고 있음
                return pistolStatusStatus != null ? pistolStatusStatus.Damage : 0f;

            case AbilityName.PistolMagazine:
                return pistolStatusStatus != null ? pistolStatusStatus.TotalMagazine : 0f;

            case AbilityName.RifleDamage:
                return rifleStatusStatus != null ? rifleStatusStatus.Damage : 0f;

            case AbilityName.RifleCriticalChance:
                return rifleStatusStatus != null ? rifleStatusStatus.CriticalChance : 0f;

            case AbilityName.RifleCriticalDamage:
                return rifleStatusStatus != null ? rifleStatusStatus.CriticalDamage : 0f;

            case AbilityName.RifleMagazine:
                return rifleStatusStatus != null ? rifleStatusStatus.TotalMagazine : 0f;

            case AbilityName.GrenadeDamage:
                return grenadeStatusStatus != null ? grenadeStatusStatus.Damage : 0f;

            case AbilityName.GrenadeMagazine:
                return grenadeStatusStatus != null ? grenadeStatusStatus.TotalMagazine : 0f;

            default:
                return 0f;
        }
    }

    private bool Debug_IsFloatAbility(AbilityName name)
    {
        switch (name)
        {
            case AbilityName.MoveSpeed:
            case AbilityName.PistolCriticalChance:
            case AbilityName.RifleCriticalChance:
                return true;
            default:
                return false;
        }
    }



    //public void ClickLeftSelectBt()
    //{
    //    _selectedAbilityName = Enum.Parse<AbilityName>(_leftAbilityText.text);
    //    _isSelectAbility = !_isSelectAbility;
    //    // TODO: 클릭시 글자 Box 에 Outline 추가 필요. 재 클릭하면 빈값이 되도록 하는 것도 필요
    //}
    //public void ClickCenterSelectBt()
    //{
    //    _selectedAbilityName = Enum.Parse<AbilityName>(_centerAbilityText.text);
    //    _isSelectAbility = !_isSelectAbility;
    //    // TODO: 클릭시 글자 Box 에 Outline 추가 필요. 재 클릭하면 빈값이 되도록 하는 것도 필요
    //}
    //public void ClickRightSelectBt()
    //{
    //    _selectedAbilityName = Enum.Parse<AbilityName>(_rightAbilityText.text);
    //    _isSelectAbility = !_isSelectAbility;
    //    // TODO: 클릭시 글자 Box 에 Outline 추가 필요. 재 클릭하면 빈값이 되도록 하는 것도 필요
    //}
    public void ClickLeftSelectBt()
{
    AbilityName parsed = Enum.Parse<AbilityName>(_leftAbilityText.text);

    // 이미 선택된 상태에서 같은 버튼을 다시 누르면 선택 해제
    if (_isSelectAbility && _selectedAbilityName.Equals(parsed))
    {
        _isSelectAbility = false;
        return;
    }

    // 그 외에는 무조건 선택 상태 true로 세팅
    _selectedAbilityName = parsed;
    _isSelectAbility = true;
}

public void ClickCenterSelectBt()
{
    AbilityName parsed = Enum.Parse<AbilityName>(_centerAbilityText.text);

    if (_isSelectAbility && _selectedAbilityName.Equals(parsed))
    {
        _isSelectAbility = false;
        return;
    }

    _selectedAbilityName = parsed;
    _isSelectAbility = true;
}

public void ClickRightSelectBt()
{
    AbilityName parsed = Enum.Parse<AbilityName>(_rightAbilityText.text);

    if (_isSelectAbility && _selectedAbilityName.Equals(parsed))
    {
        _isSelectAbility = false;
        return;
    }

    _selectedAbilityName = parsed;
    _isSelectAbility = true;
}

}

public enum AbilityName
{
    Hp,
    MoveSpeed,
    Defense,
    PistolDamage,
    PistolCriticalChance,
    PistolCriticalDamage,
    PistolMagazine,
    RifleDamage,
    RifleCriticalChance,
    RifleCriticalDamage,
    RifleMagazine,
    GrenadeDamage,
    GrenadeMagazine,
}


[Serializable]
public struct IntRange
{
    [Range(-100, 100)] public int min;
    [Range(-100, 100)] public int max;
}

[Serializable]
public struct FloatRange
{
    [Range(-1f, 1f)] public float min;
    [Range(-1f, 1f)] public float max;
}


