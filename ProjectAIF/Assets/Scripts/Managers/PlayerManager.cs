using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : SingleTon<PlayerManager>
{
    [SerializeField] GameObject Player;
    public bool IsDead = false;

    private void Awake()
    {
        SingleTonInit();
    }
}
