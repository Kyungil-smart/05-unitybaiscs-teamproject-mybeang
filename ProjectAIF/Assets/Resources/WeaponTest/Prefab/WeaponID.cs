using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponID : MonoBehaviour
{
    public WeaponType ID;
}

public enum WeaponType
{
    BasePistol,    // 권총
    BaseRifle,     // 소총
    BaseGrenade    // 수류탄
}