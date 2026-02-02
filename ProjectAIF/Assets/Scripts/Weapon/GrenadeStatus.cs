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
    
    //private void Awake()
    //{
    //    Damage = 50;
    //    TotalMagazine = 3;
    //    AttackRate = 10f;
    //    CurrentMagazine = TotalMagazine;
    //}
    
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
