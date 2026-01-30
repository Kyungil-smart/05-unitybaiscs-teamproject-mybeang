using UnityEngine;

public abstract class WeaponStatusBase : MonoBehaviour
{
    [Header("기본 성능")]
    public int Damage;
    public int CurrentMagazine;
    public int TotalMagazine;
}
