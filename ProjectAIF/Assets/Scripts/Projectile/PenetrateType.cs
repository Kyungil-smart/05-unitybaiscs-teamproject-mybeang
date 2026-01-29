using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetrateType : MonoBehaviour
{
    [SerializeField] private RangeMonster _shootingMonster;
    [SerializeField] private Transform _shootingTransform;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _damageRate;
    
    private Coroutine _penetrateCoroutine; 

    private void OnEnable()
    {
        transform.position = _shootingTransform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!(other is IDamageable)) return;
        
        if(_penetrateCoroutine != null) return;
        
        _penetrateCoroutine = StartCoroutine(PenetrateDamageCoroutine(other));
    }

    private void Update()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
    }

    private IEnumerator PenetrateDamageCoroutine(Collider other)
    {
        while (true)
        {
            (other as IDamageable).TakeDamage(_shootingMonster.DamageCalc(_shootingMonster.Damage));
            Debug.Log($"{_shootingMonster.name} : 명중! {_shootingMonster.DamageCalc(_shootingMonster.Damage)} 피해");
            yield return YieldContainer.WaitForSeconds(_damageRate);
        }
    }
}
