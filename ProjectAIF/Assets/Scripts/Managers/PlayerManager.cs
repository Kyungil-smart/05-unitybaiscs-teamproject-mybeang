using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : SingleTon<PlayerManager>
{
    [SerializeField] GameObject Player;
    PlayerWeapon _playerWeapon;

    public GameObject Pistol;
    public GameObject AR;
    public GameObject Grenade;
    float timer = 0f;

    // 무기 인식 및 탄창 관리기능 = 관련 변수목록


    private void Awake()
    {
        
        SingleTonInit();

        _playerWeapon = Player.GetComponent<PlayerWeapon>();
        Pistol = Resources.Load<GameObject>("Test/Prefab/TestPistol");
        AR = Resources.Load<GameObject>("Test/Prefab/TestAR");
        Grenade = Resources.Load<GameObject>("Test/Prefab/TestGre");
        
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 5f && timer < 5.2)
        {
            Debug.Log("5초경과");
            Pistol = Resources.Load<GameObject>("Test/Prefab/TestPistol2");
        }

        if (timer >= 6f && timer < 6.2)
        {
            Debug.Log("6초경과");
            Pistol = Resources.Load<GameObject>("Test/Prefab/TestPistol");
        }
    }


    // 무기 인식 및 탄창 관리기능

    // 수류탄 탄창 및 차징 관리기능

    // 플레이어 체력 & 사망 관련 관리기능









}
