using UnityEngine;
using UnityEngine.Events;

public class RifleStatus : WeaponStatusBase
{
    [Range(0, 1)] public float CriticalChance = 0;
    public int CriticalDamage = 15;
    public float AttackRate = 0.3f;

    [Header("강화 제한")]
    public int MinDamage = 5;
    public int MaxDamage = 30;

    public float MinCritChance = 0f;
    public float MaxCritChance = 0.2f;

    public int MinCritDamage = 10;
    public int MaxCritDamage = 40;

    public int MinMagazine = 30;
    public int MaxMagazine = 45;

    private void Awake()
    {
        OnCurrentMagazineChanged = new UnityEvent<int, int>();
        OnTotalMagazineChanged = new UnityEvent<int, int>();
        Damage = 10;
        TotalMagazine = 30;
        CurrentMagazine = TotalMagazine;
    }
}

