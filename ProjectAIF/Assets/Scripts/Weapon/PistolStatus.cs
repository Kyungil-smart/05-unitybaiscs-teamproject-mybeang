using UnityEngine;

public class PistolStatus : WeaponStatusBase
{
    [Header("기본 권총")]
    [Range(0, 1)] public float CriticalChance = 0;
    public int CriticalDamage = 15;
    public float AttackRate = 1f;

    [Header("강화 제한")]
    public int MinDamage = 2;
    public int MaxDamage = 10;
    
    public float MinCritChance = 0f;
    public float MaxCritChance = 0.7f;
    
    public int MinCritDamage = 4;
    public int MaxCritDamage = 30;

    public int MinMagazine = 7;
    public int MaxMagazine = 15;
    
    private void Awake()
    {
        Damage = 5;
        TotalMagazine = 7;
        CurrentMagazine = TotalMagazine;
    }
}

