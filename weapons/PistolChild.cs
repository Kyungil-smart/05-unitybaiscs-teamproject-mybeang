using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPistol : Pistol
{
    private void Start()
    {
        // 표 기준 초기값
    }

    private void Update()
    {
        // 마우스 클릭 시 발사
        if (Input.GetButtonDown("Fire1"))
        {
            Fire(); 
        }
    }
}

