using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    
    /// <summary>
    /// 헤더와 툴팁이 추상클래스에서는 작동 안하는거같아서 주석달아서 표기
    /// </summary>
    
    [field:SerializeField]public int AttValue { get; protected set; }
    
    // 데미지 감소율로 하자고 했음 : 25입력 > 25%임
    [field:SerializeField]public float DefValue { get; protected set; }
    
    // Init할때 몬스터가 들고 있는 _currentHp의 기준치
    // _currentHp <= 0일때 사망
    [field:SerializeField]public int BaseHp { get; protected set; }
    
    // 치명타 확률 = 100%가 최대임
    [field:SerializeField]public float CritChance { get; protected set; }
    
    // 101입력 > 101%
    [field:SerializeField]public float CritMultiValue { get; protected set; }
    
    [field:SerializeField]public float MoveSpeed { get; protected set; }
    
    // AttackRate는 공격 간격으로 숫자가 낮을수록 빨라짐
    [field:SerializeField]public float AttackRate { get; protected set; }
    
    // 몬스터 처치 시 얻는 EXP
    [field:SerializeField]public int Exp{get; protected set;}
}
