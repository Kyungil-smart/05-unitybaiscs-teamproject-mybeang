using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStatus : MonoBehaviour, IDamageable, ITargetable
{
    [Header("Status")]
    [SerializeField] private int _currentHp;
    public UnityEvent<int, int> OnCurrentHpChanged;
    public int CurrentHp
    {
        get => _currentHp;
        set
        {
            _currentHp = value;
            OnCurrentHpChanged?.Invoke(_currentHp, _totalHp);
        }
    }
    [SerializeField] private int _totalHp;
    public UnityEvent<int, int> OnTotalHpChanged;
    public int TotalHp
    {
        get => _totalHp;
        set
        {
            _totalHp = value;
            //OnCurrentHpChanged?.Invoke(_currentHp, _totalHp);
            OnTotalHpChanged?.Invoke(_currentHp, _totalHp);
        }
    }
    public float MoveSpeed;
    public int Defense;
    
    [Header("Min/Max Value")]
    public int MinHp;
    public int MaxHp;
    public int MinMoveSpeed;
    public int MaxMoveSpeed;
    public int MinDefense;
    public int MaxDefense;
    
    [Header("Sounds")]
    [SerializeField] private AudioClip _damagedSound;

    public void TakeDamage(int damage)
    {
        CurrentHp -= damage;
        AudioManager.Instance.PlaySound(_damagedSound);
        if (CurrentHp <= 0)
        {
            PlayerManager.Instance.IsDead = true;
            GameManager.Instance.GameOver();
        }
    }

    public void SetTarget() { }

    public void UnsetTarget() { }
}
