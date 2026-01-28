using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KYC_PlayerDummy : MonoBehaviour, IDamageable, ITargetable
{
    [SerializeField] private int _currentHealth;
    [SerializeField] private int Defence;
    
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget()
    {
        
    }

    public void UnsetTarget()
    {
        
    }
}
