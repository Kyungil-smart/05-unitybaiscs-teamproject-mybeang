using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Monster : MonoBehaviour
{
    
    /// <summary>
    /// 헤더와 툴팁이 추상클래스에서는 작동 안하는거같아서 주석달아서 표기
    /// </summary>
    
    [field:SerializeField]public int Damage { get; protected set; }
    
    [field:SerializeField]public int Defence { get; protected set; }
    
    // Init할때 몬스터가 들고 있는 _currentHp의 기준치
    // _currentHp <= 0일때 사망
    [field:SerializeField]public int Hp { get; protected set; }
    
    [field:SerializeField]public float MoveSpeed { get; protected set; }
    
    // AttackRate는 공격 간격으로 숫자가 낮을수록 빨라짐
    [field:SerializeField]public float AttackRate { get; protected set; }
    
    [field:SerializeField]public float AttackRange { get; protected set; }
    
    // 몬스터 처치 시 얻는 EXP
    [field:SerializeField]public int Exp {get; protected set;}

    [SerializeField] public PlayerStatus Player;
    // 그 밖에 연산을 위해 공통으로 갖고 있어야할 변수
    protected Animator _animator;
    public Animator MonsterAnimator => _animator;
}
