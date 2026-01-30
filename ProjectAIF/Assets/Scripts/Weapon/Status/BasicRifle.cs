using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRifle : MonoBehaviour
{
    [Header("기본 소총")]
    public int Damage = 10;
    [Range(0, 1)] public float CriticalChance = 0;
    public int CriticalDamage = 15;
    public int Magazine = 30;
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

    public virtual void Fire() { }
}

