using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;

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
    public FloatRange SpeedRange;
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
    
    [Header("Data Target")]
    [SerializeField] private PlayerStatusDummy _playerStatus;
    [SerializeField] private PistolDummy _pistolStatus;
    [SerializeField] private RifleDummy _rifleStatus;
    [SerializeField] private GrenadeDummy _grenadeStatus;
    
    // Audio
    AudioSource _audioSource;
    AudioClip _audioClip;
    
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
        AbilityData.Add(AbilityName.Hp, 
            new Ability.Hp(_playerStatus, _audioSource, _audioClip));
        AbilityData.Add(AbilityName.MoveSpeed, 
            new Ability.MoveSpeed(_playerStatus, _audioSource, _audioClip));
        AbilityData.Add(AbilityName.Defense, 
            new Ability.Defense(_playerStatus, _audioSource, _audioClip));
        AbilityData.Add(AbilityName.PistolDamage, 
            new Ability.PistolDamage(_pistolStatus, _audioSource, _audioClip));
        AbilityData.Add(AbilityName.PistolCriticalChance, 
            new Ability.PistolCriticalChance(_pistolStatus, _audioSource, _audioClip));
        AbilityData.Add(AbilityName.PistolCriticalDamage, 
            new Ability.PistolCriticalDamage(_pistolStatus, _audioSource, _audioClip));
        AbilityData.Add(AbilityName.PistolMagazine, 
            new Ability.PistolMagazine(_pistolStatus, _audioSource, _audioClip));
        AbilityData.Add(AbilityName.RifleDamage, 
            new Ability.RifleDamage(_rifleStatus, _audioSource, _audioClip));
        AbilityData.Add(AbilityName.RifleCriticalChance, 
            new Ability.RifleCriticalChance(_rifleStatus, _audioSource, _audioClip));
        AbilityData.Add(AbilityName.RifleCriticalDamage, 
            new Ability.RifleCriticalDamage(_rifleStatus, _audioSource, _audioClip));
        AbilityData.Add(AbilityName.RifleMagazine, 
            new Ability.RifleMagazine(_rifleStatus, _audioSource, _audioClip));
        AbilityData.Add(AbilityName.GrenadeMagazine, 
            new Ability.GrenadeMagazine(_grenadeStatus, _audioSource, _audioClip));
        AbilityData.Add(AbilityName.GrenadeDamage, 
            new Ability.GrenadeDamage(_grenadeStatus, _audioSource, _audioClip));
    }

    /// <summary>
    /// 버튼에서 클릭시
    /// </summary>
    /// <param name="name">Ability 이름</param>
    public void ApplyAbility()
    {
        if (_isSelectAbility)
        {
            CurrentAbilityData[_selectedAbilityName].ApplyAbility();
            Debug.Log("Applying Ability: " + _selectedAbilityName);
            // TODO: Apply 후 본래의 게임 화면으로 돌아가는 내용 추가 구현 필요    
        }
    }
    
    /// <summary>
    /// 어빌리티 중 3개 선택. 중복 없도록 조정.
    /// </summary>
    public void ReadyToThreeAbilities()
    {
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
        _leftAbilityText.text = selected[0].ToString();
        _centerAbilityText.text = selected[1].ToString();
        _rightAbilityText.text = selected[2].ToString();
    }

    public void ClickLeftSelectBt()
    {
        _selectedAbilityName = Enum.Parse<AbilityName>(_leftAbilityText.text);
        _isSelectAbility = !_isSelectAbility;
        // TODO: 클릭시 글자 Box 에 Outline 추가 필요. 재 클릭하면 빈값이 되도록 하는 것도 필요
    }
    public void ClickCenterSelectBt()
    {
        _selectedAbilityName = Enum.Parse<AbilityName>(_centerAbilityText.text);
        _isSelectAbility = !_isSelectAbility;
        // TODO: 클릭시 글자 Box 에 Outline 추가 필요. 재 클릭하면 빈값이 되도록 하는 것도 필요
    }
    public void ClickRightSelectBt()
    {
        _selectedAbilityName = Enum.Parse<AbilityName>(_rightAbilityText.text);
        _isSelectAbility = !_isSelectAbility;
        // TODO: 클릭시 글자 Box 에 Outline 추가 필요. 재 클릭하면 빈값이 되도록 하는 것도 필요
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
