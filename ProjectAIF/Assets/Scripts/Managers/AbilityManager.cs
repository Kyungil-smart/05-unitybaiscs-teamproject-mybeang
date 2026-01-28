using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


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

public class AbilityManager : SingleTon<AbilityManager>
{
    // Ability Values
    [Header("Ability Value")]
    public (int min, int max) HpRange;
    public (float min, float max) SpeedRange;
    public (int min, int max) DefenseRange;
    public (int min, int max) PistolDamageRange;
    public (float min, float max) PistolCriticalChanceRange;
    public (int min, int max) PistolCriticalDamageRange;
    public (int min, int max) PistolMagazineRange;
    public (int min, int max) RifleDamageRange;
    public (float min, float max) RifleCriticalChanceRange;
    public (int min, int max) RifleCriticalDamageRange;
    public (int min, int max) RifleMagazineRange;
    public (int min, int max) GrenadeDamageRange;
    public (int min, int max) GrenadeMagazineRange;
    
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
    public void ApplyAbility(AbilityName name)
    {
        CurrentAbilityData[name].ApplyAbility();
    }
    
    /// <summary>
    /// 어빌리티 중 3개 선택. 중복 없도록 조정.
    /// </summary>
    public void ReadyToThreeAbilities()
    {
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
    }
}
