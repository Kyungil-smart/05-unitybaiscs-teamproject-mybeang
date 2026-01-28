using System.Collections.Generic;
using UnityEngine;
using Ability;

public enum AbilityName
{
    Hp,
    MoveSpeed,
    Defense,
    PistolDamage,
    PistolCriticalRate,
    PistolCriticalDamage,
    PistolMagazine,
    RifleDamage,
    RifleCriticalRate,
    RifleCriticalDamage,
    RifleMagazine,
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
    
    // Data 
    public Dictionary<AbilityName, Ability.Ability> AbilityData = new ();

    public void Init()
    {
        AbilityData.Clear();
        // AbilityData.Add(AbilityName.Hp, Ability.Ability.Hp);
    }
}
