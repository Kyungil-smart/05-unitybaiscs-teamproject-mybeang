using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [Header("수류탄")]
    public int Damage;          // 공격력
    public float EffectRange;   // 폭발 범위
    public int Magazine;        // 장탄수
    public float ChargeTime;    // 차징 속도

    // 나중에 혹시모를 '특수 수류탄'
    public virtual void Explode()
    {
        // 폭발
        Debug.Log(EffectRange);
    }
}

