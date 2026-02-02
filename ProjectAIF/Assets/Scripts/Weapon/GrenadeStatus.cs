using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;

public class GrenadeStatus : WeaponStatusBase
{
    [SerializeField] private LayerMask _explosionLayer;
    public float EffectRange = 10f; // 폭발 범위
    public float ChargeTime = 3f;

    [Header("강화 제한")]
    public int MinDamage = 25;
    public int MaxDamage = 100;
    public float MinRange = 10f;
    public float MaxRange = 10f;
    public int MinMagazine = 0;
    public int MaxMagazine = 6;
    
    // 설정
    [Header("터지기")]
    public float ExplosionForce = 700f; // 폭발
    public GameObject ExplosionEffectPrefab; // 이펙트
    [SerializeField] private float _explosionDestroyTime;
    [SerializeField] AudioClip _explodeSound;
    
    // TODO : 필요는 없을 것 같은데 적어놓으셔서...의도파악이 힘들어서 주석으로 처리해놓겠습니다
    // private bool _hasExploded = false; // 두 번 방지
    
    // TODO : 기초적인 스테이터스들을 Awake에서 고정값으로 두고 있는데, 만약 인스펙터에서 수정 가능하길 원하는거면 초기화 내용도 손볼필요는 있을 것 같아요
    private void Awake()
    {
        Damage = 50;
        TotalMagazine = 3;
        AttackRate = 10f;
        CurrentMagazine = TotalMagazine;
    }
    
    // 충돌하면 폭발
    private void OnCollisionEnter(Collision collision)
    {
        // 닿았을 때 터지기
        /*
        if (_hasExploded)
        {
            return;
        }
        */

        if ((_explosionLayer.value & (1 << collision.gameObject.layer)) == 0) return;
        
        Explode();
    }

    public void Explode()
    {
        // play explosion effect
        if (ExplosionEffectPrefab != null)
        {
            GameObject explosion = Instantiate(ExplosionEffectPrefab, transform.position, Quaternion.identity);
            Destroy(explosion,_explosionDestroyTime);
        }

        // play audio
        if (_explodeSound != null)
        {
            AudioManager.Instance.PlaySound(_explodeSound);
        }
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, EffectRange);
        
        foreach (Collider nearbyObject in colliders)
        {
            // Damage
            IDamageable damageable = nearbyObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable?.TakeDamage(Damage);
            }

            // Knock-back
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(ExplosionForce, transform.position, EffectRange, 2.0f);
            }
        }
        
        Destroy(gameObject);
    }
}
