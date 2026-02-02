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
        transform.rotation = _shootingTransform.rotation;
    }

    private void OnTriggerStay(Collider other)
    {
        if(_penetrateCoroutine != null) return;

        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Crystal"))
        {
            _penetrateCoroutine = StartCoroutine(PenetrateDamageCoroutine(other));
            Debug.Log("지속데미지 시작");
        }
    }

    private void Update()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
    }

    private void OnDisable()
    {
        StopCoroutine(_penetrateCoroutine);
        _penetrateCoroutine = null;
        Debug.Log("공격시간 종료");
    }

    private IEnumerator PenetrateDamageCoroutine(Collider other)
    {
        Debug.Log($"충돌 : {other.gameObject.name}");
        while (true)
        {
            if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Crystal"))
            {
                _penetrateCoroutine = null;
                Debug.Log("범위 벗어나서 종료");
                yield break;
            }
            
            if (!other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<IDamageable>()?.TakeDamage(_shootingMonster.DamageCalc());
                Debug.Log($"{_shootingMonster.name} : 명중! {_shootingMonster.DamageCalc()} 피해");
            }
            
            yield return YieldContainer.WaitForSeconds(_damageRate);
        }
    }
}
