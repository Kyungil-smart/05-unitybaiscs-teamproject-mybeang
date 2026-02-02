using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Crystal : MonoBehaviour, IDamageable, ITargetable
{
    // Crystal HP
    public UnityEvent OnHpChanged = new();
    private int _currentHp;
    public int CurrentHp
    {
        get => _currentHp;
        set
        {
            _currentHp = value;
            OnHpChanged?.Invoke();
        }
    }
    [SerializeField] private int _maxHp;
    public int MaxHp
    {
        get => _maxHp;
    }
    
    [SerializeField] private Transform _playerTF;
    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private List<AudioClip> _nonsenseSounds;
    
    private Ray _ray;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        StartCoroutine(PlayNonsenseSoundCoroutine());
    }
    
    private void LateUpdate()
    {
        DetectPlayerRange();
    }

    private void Init()
    {
        CurrentHp = _maxHp;
        _nonsenseSounds.Add(Resources.Load<AudioClip>("CrystalSay001"));
        _nonsenseSounds.Add(Resources.Load<AudioClip>("CrystalSay002"));
        _nonsenseSounds.Add(Resources.Load<AudioClip>("CrystalSay003"));
        _nonsenseSounds.Add(Resources.Load<AudioClip>("CrystalSay004"));
        _nonsenseSounds.Add(Resources.Load<AudioClip>("CrystalSay005"));
    }
    
    private Vector3 GetDirectionToPlayer()
    {
        return (_playerTF.position - transform.position).normalized;
    }
    
    private void DetectPlayerRange()
    {
        Vector3 directionToPlayer = GetDirectionToPlayer();
        _ray = new Ray(transform.position, directionToPlayer);
        
        RaycastHit hit;
        if (Physics.Raycast(_ray, out hit, _distance, _playerLayer))
        {
            GameManager.Instance.IsCrystalNearPlayer = true;
        }
        else
        {
            GameManager.Instance.IsCrystalNearPlayer = false;
        }
    }

    private void OnDrawGizmos()
    {
         Gizmos.color = Color.red;
         Gizmos.DrawRay(_ray.origin, _ray.direction * _distance);
    }

    public void TakeDamage(int damage)
    {
        CurrentHp -= damage;
        if (CurrentHp <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    private IEnumerator PlayNonsenseSoundCoroutine()
    {
        while (true)
        {
            int index = Random.Range(0, _nonsenseSounds.Count - 1);
            AudioManager.Instance.PlaySound(_nonsenseSounds[index]);
            float length = _nonsenseSounds[index].length;
            yield return YieldContainer.WaitForSeconds(length + 10f);
        }
    }

    public void SetTarget() { }

    public void UnsetTarget() { }
}
