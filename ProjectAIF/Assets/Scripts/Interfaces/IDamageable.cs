using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    // TODO : 현재 Hp를 저장하는 int _currentHP는 인터페이스에서 강제 할수 없음으로 스텟 부분에서 추가 해주셔야 합니다.
    public void TakeDamage(int damage);
}
