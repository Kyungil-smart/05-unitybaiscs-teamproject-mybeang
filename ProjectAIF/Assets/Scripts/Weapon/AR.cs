using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR : MonoBehaviour
{
    [Header("소총")]
    public int Damage; // 기본 데미지
    [Range(0f, 1f)]
    public float CriticalChance; //치명타 확률
    public int CriticalDamage; //치명타 데미지
    public int Magazine; //장탄수
    public float AttackRate; // 사격 속도

    public virtual void Fire()
    {
        Debug.Log("소총 발사");
    }
}
