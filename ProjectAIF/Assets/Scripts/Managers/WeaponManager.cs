using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponManager : SingleTon<WeaponManager>
{
    // PlayerManager 에 있는 무기 가져오던 코드랑 뭐가 다른거지?
    GameObject BasePisotl;
    GameObject BasePisotl2;
    GameObject BaseAR;
    GameObject BaseGrenade;
    private void Awake()
    {
        LoadWeaponPrefabs();
        SetWeaponID();
    }

    void LoadWeaponPrefabs()
    {
        BasePisotl = Resources.Load<GameObject>("Test/Prefab/TestPistol");
        BasePisotl2 = Resources.Load<GameObject>("Test/Prefab/TestPistol2");
        BaseAR = Resources.Load<GameObject>("Test/Prefab/TestAR");
        BaseGrenade = Resources.Load<GameObject>("Test/Prefab/TestGre");
    }

    void SetWeaponID()
    {
        BasePisotl.GetComponent<WeaponID>().ID = WeaponType.BasePistol;
        BasePisotl2.GetComponent<WeaponID>().ID = WeaponType.BasePistol2;
        BaseAR.GetComponent<WeaponID>().ID = WeaponType.BaseAR;
        BaseGrenade.GetComponent<WeaponID>().ID = WeaponType.BaseGrenade;
    }
}

public enum WeaponType
{
    BasePistol,
    BasePistol2,
    BaseAR,
    BaseGrenade
}
