using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAR : Rifle
{
    private void Start()
    {
	// 안에 
    }

    private void Update()
    {
        // 사격 버튼(수정가능)
        if (Input.GetButton("Fire1"))
        {
            Fire();
        }
    }
}
