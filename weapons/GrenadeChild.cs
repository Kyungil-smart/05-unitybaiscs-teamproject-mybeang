using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGrenade : Grenade
{
    private void Start()
    {
	// 표 기준 초기값 기입 부분
    }

    // 혹시모를 내용을 추가 부분
    private void OnCollisionEnter(Collision collision)
    {
        Explode(); // 폭발 실행
        Destroy(gameObject); // 사용 후 삭제
    }
}

