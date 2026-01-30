using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    [Header("권총")]
    public int Damage; //데미지
    [Range(0f, 1f)]
    public float CriticalChance; //치명타 확률
    public int CriticalDamage; //치명타 데미지
    public int Magazine; //장탄수
    public float AttackRate; //공격 속도

    public virtual void Fire()
    {
        Debug.Log("권총 사격");
    }
}



