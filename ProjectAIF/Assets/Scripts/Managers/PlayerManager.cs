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


    // 무기 인식 및 탄창 관리기능

    // 수류탄 탄창 및 차징 관리기능

    // 플레이어 체력 & 사망 관련 관리기능









}
