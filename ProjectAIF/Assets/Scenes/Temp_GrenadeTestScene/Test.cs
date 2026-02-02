using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO : 기능 확인 끝나시면 삭제하셔도 됩니다.

public class Test : MonoBehaviour, IDamageable
{
    public void TakeDamage(int damage)
    {
        Debug.Log(damage);
    }
}
