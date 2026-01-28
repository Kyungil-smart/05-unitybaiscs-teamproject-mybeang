using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMonster : Monster, IAttackable, IDamageable, ITargetable
{
    private int _currentHealth;
    [Tooltip("0이면 Transform의 현재 y값에서 감지, 숫자 바뀌면 y값 보정")] 
    [SerializeField][Range(-10,2)] private float _detectionHeight;
    [SerializeField][Range(0,50)] private float _attackRange;
    [SerializeField] private LayerMask _attackTargetLayer;
    
    
    private ITargetable _targetable;
    private Transform _targetTransform;

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
        Attack(AttValue);
    }

    private void Init()
    {
        _currentHealth = BaseHp;
    }
    
    public void Attack(int damage)
    {
        if (_targetable == null || !(_targetable is IDamageable)) return;

        StartCoroutine(AttackCoroutine(damage));
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position + new Vector3(0, _detectionHeight, 0), transform.forward * _attackRange);
    }

    private IEnumerator AttackCoroutine(int damage)
    {
        while (true)
        {
            // TODO :  공격 애니메이션 길이 보고 수정
            float animationTime = 0.5f;
            
            if(_targetTransform == null) yield break;
            
            // TODO : 이 시점에 공격 애니메이션 출력
            
            yield return YieldContainer.WaitForSeconds(animationTime);
            
            if(_targetTransform == null) yield break;
            
            (_targetable as IDamageable).TakeDamage(damage);
            Debug.Log($"{transform.name}: {_targetTransform.name}을/를 {damage} 로 공격");
            
            yield return YieldContainer.WaitForSeconds(AttackRate);
        }
    }

    public void TakeDamage(int damage)
    {
        // TODO : 이 시점에 피격 애니메이션 출력
        
        _currentHealth -= damage;
        
        Debug.Log($"{transform.name}: {damage} 피격");
    }

    private void DetectTarget()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, _detectionHeight, 0), transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _attackRange, _attackTargetLayer))
        {
            if (_targetTransform == hit.transform) return;

            if (_targetable != null) _targetable.UnsetTarget();
            
            _targetTransform = hit.transform;
            _targetable = hit.transform.GetComponent<ITargetable>();
            _targetable.SetTarget();
        }
        else
        {
            _targetable?.UnsetTarget();
            _targetable = null;
            _targetTransform = null;
        }
    }

    public void SetTarget()
    {
        
    }

    public void UnsetTarget()
    {
        
    }
}
