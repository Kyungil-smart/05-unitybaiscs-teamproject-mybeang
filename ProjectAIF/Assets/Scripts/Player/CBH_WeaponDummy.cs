using UnityEngine;

public class WeaponDummy : MonoBehaviour
{
    public int Damage;
    public float CriticalChance;
    public int CriticalDamage;
    public int Magazine;
}

public class PistolDummy : WeaponDummy { }

public class RifleDummy : WeaponDummy { }

public class GrenadeDummy : MonoBehaviour
{
    public int Damage;
    public int Magazine;
    public float EffectRange;
    public int ChargeTime;
}