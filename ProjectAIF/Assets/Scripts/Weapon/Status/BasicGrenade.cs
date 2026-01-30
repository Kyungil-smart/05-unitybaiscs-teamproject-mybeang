using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGrenade : MonoBehaviour
{
    // 기본 수류탄 초기값
    [Header("기본 수류탄")]
    public int Damage = 50;
    public float EffectRange = 10f; // 폭발 범위
    public int Magazine = 3;
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
    public GameObject explosionEffectPrefab; // 이펙트

    private bool hasExploded = false; // 두 번 방지
    
    // 충돌하면 폭발
    private void OnCollisionEnter(Collision collision)
    {
        // 닿았을 때 터지기
        if (hasExploded) return; 
        if (collision.gameObject.CompareTag("Player")) return; 

        Explode();
    }

    public virtual void Explode()
    {
        hasExploded = true; // 터지는 거 체크

        // 이펙트 미리 준비
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }

        // 주변 물체 밀기
        Collider[] colliders = Physics.OverlapSphere(transform.position, EffectRange); // 범위 내 물체 찾기
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // 터질 때 밀어내기
                rb.AddExplosionForce(ExplosionForce, transform.position, EffectRange, 2.0f);
            }
            

        }

        Debug.Log("수류탄 폭발");

        // 수류탄 삭제
        Destroy(gameObject);
    }
}
