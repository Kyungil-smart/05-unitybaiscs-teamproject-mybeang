using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowType : MonoBehaviour
{
    [SerializeField] private RangeMonster _shootingMonster;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _shootingTransform;

    private void OnEnable()
    {
        transform.position = _shootingTransform.position;
        transform.rotation = _shootingTransform.rotation;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log($"충돌 : {other.gameObject.name}");
        
        other.gameObject.GetComponent<IDamageable>()?.TakeDamage(_shootingMonster.DamageCalc(_shootingMonster.Damage));
        Debug.Log($"{_shootingMonster.name} : 명중! {_shootingMonster.DamageCalc(_shootingMonster.Damage)} 피해");
        gameObject.SetActive(false);
    }

    private void Update()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
    }

    
}
