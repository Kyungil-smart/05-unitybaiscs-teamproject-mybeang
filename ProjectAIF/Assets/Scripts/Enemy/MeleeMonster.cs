using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MeleeMonster : Monster, IAttackable, IDamageable, ITargetable
{
    private int _currentHp;
    [Tooltip("0이면 Transform의 현재 y값에서 감지, 숫자 바뀌면 y값 보정")] 
    [SerializeField] private float _detectionHeight;
    [SerializeField] private LayerMask _attackTargetLayer;
    [FormerlySerializedAs("_attackAnimationTime")]
    [Tooltip("공격모션 길이에 따라서 타격 순간까지 공격판정 대기함")] 
    [SerializeField] private float _attackAnimationDelay;
    
    private Coroutine _attackCoroutine;
    private Coroutine _deathCoroutine;
    private ITargetable _targetable;
    private Transform _targetTransform;

    //private PlayerStatus _playerStatus;
    
    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        DetectTarget();
    }

    private void LateUpdate()
    {
        if (_targetable is IDamageable)
        {
            Attack(DamageCalc(Damage));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + new Vector3(0, _detectionHeight, 0), transform.forward * AttackRange);
    }
    
    private void Init()
    {
        _currentHp = Hp;
    }
    
    public void Attack(int damage)
    {
        if (_targetable == null) return;

        if (_attackCoroutine  != null) return;
        
        _attackCoroutine = StartCoroutine(AttackCoroutine(damage));
    }

    private int DamageCalc(int attValue)
    {
        //TODO : 플레이어/크리스탈 방어력 들어있는 컴포넌트 지정해서 삽입
        int targetDef = 0;
        return attValue - targetDef;
    }

    private IEnumerator AttackCoroutine(int damage)
    {
        while (true)
        {
            if (_targetTransform == null)
            {
                _attackCoroutine = null;
                yield break;
            }
            
            _animator.SetTrigger("SetAttack");
            Debug.Log($"{gameObject.name} : 공격모션 출력");

            if (_targetTransform == null)
            {
                _attackCoroutine = null;
                yield break;
            }
            
            yield return YieldContainer.WaitForSeconds(_attackAnimationDelay);
            
            if (_targetTransform == null)
            {
                _attackCoroutine = null;
                yield break;
            }
            
            (_targetable as IDamageable).TakeDamage(damage);
            Debug.Log($"{transform.name}: {_targetTransform.name}을/를 {damage} 로 공격");
            
            yield return YieldContainer.WaitForSeconds(AttackRate);
        }
    }

    public void TakeDamage(int damage)
    {
        _currentHp -= damage;
        
        Debug.Log($"{transform.name}: {damage} 피격");

        if (_currentHp <= 0) Death();
    }

    private void Death()
    {
        if (_deathCoroutine  != null) return;
        
        _deathCoroutine = StartCoroutine(DeathCoroutine());
    }

    private IEnumerator DeathCoroutine()
    {
        _animator.SetTrigger("SetDeath");
        yield return YieldContainer.WaitForSeconds(1f);
        Player.Exp += Exp;
        Destroy(gameObject);
    }

    private void DetectTarget()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, _detectionHeight, 0), transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, AttackRange, _attackTargetLayer))
        {
            if (_targetTransform == hit.transform) return;

            if (_targetable != null) _targetable.UnsetTarget();
            
            _targetTransform = hit.transform;
            _targetable = hit.transform.GetComponent<ITargetable>();
            
            //_targetDef = hit.transform.gameObject.Defense();
            Debug.Log($"{gameObject.name} : 목표 포착");
        }
        else
        {
            _targetable = null;
            _targetTransform = null;
            //_targetDef = 0;
            Debug.Log($"{gameObject.name} : 목표 수색중");
        }
    }
    
    public void SetTarget()
    {
        
    }

    public void UnsetTarget()
    {
        
    }
}
