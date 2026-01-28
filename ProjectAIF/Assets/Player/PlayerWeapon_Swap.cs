using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSwap : MonoBehaviour
{
    [SerializeField] GameObject Player;

    Transform _pistolSlot;
    Transform _aRSlot;
    Transform _gGrenadeSlot;

    Vector3 _basePosPistol;
    Vector3 _downPosPistol;

    Vector3 _basePosAR;
    Vector3 _downPosAR;

    Vector3 _basePosGre;
    Vector3 _downPosGre;

    Vector3 _velocityPistol;
    Vector3 _velocityAR;
    Vector3 _velocityGre;

    float _smoothTime = 0.15f;

    bool _pickPistol = false;
    bool _pickAR = false;
    bool _pickGrenade = false;

    public int PlayerCurrentWeapon = 0;

    private void Awake()
    {
        WeaponPosCheck();
    }

    private void Update()
    {
        CheckCurWeapon();
        VisualSwap();
    }

    void WeaponPosCheck()
    {
        foreach (Transform Pistol in Player.GetComponentsInChildren<Transform>())
        {
            if (Pistol.name == "PistolSlot")
            {
                _pistolSlot = Pistol;
                break;
            }
        }
        _basePosPistol = _pistolSlot.localPosition;
        _downPosPistol = _basePosPistol + Vector3.down * 0.7f;


        foreach (Transform AR in Player.GetComponentsInChildren<Transform>())
        {
            if (AR.name == "ARSlot")
            {
                _aRSlot = AR;
                break;
            }
        }
        _basePosAR = _aRSlot.localPosition;
        _downPosAR = _basePosAR + Vector3.down * 0.7f;


        foreach (Transform Gre in Player.GetComponentsInChildren<Transform>())
        {
            if (Gre.name == "GrenadeSlot")
            {
                _gGrenadeSlot = Gre;
                break;
            }
        }
        _basePosGre = _gGrenadeSlot.localPosition;
        _downPosGre = _basePosGre + Vector3.down * 0.7f;
    }

    void CheckCurWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerCurrentWeapon = 1;
            _pickPistol = true;
            _pickAR = false ;
            _pickGrenade = false ;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerCurrentWeapon = 2;
            _pickPistol = false;
            _pickAR = true;
            _pickGrenade = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerCurrentWeapon = 3;
            _pickPistol = false;
            _pickAR = false;
            _pickGrenade = true;
        }
    }

    void VisualSwap()
    {
        Vector3 TargetPistol = _pickPistol ? _basePosPistol : _downPosPistol;

        _pistolSlot.localPosition = Vector3.SmoothDamp(
            _pistolSlot.localPosition,
            TargetPistol,
            ref _velocityPistol,
            _smoothTime
        );

        Vector3 TargetAR = _pickAR ? _basePosAR : _downPosAR;

        _aRSlot.localPosition = Vector3.SmoothDamp(
            _aRSlot.localPosition,
            TargetAR,
            ref _velocityAR,
            _smoothTime
        );

        Vector3 TargetGre = _pickGrenade ? _basePosGre : _downPosGre;

        _gGrenadeSlot.localPosition = Vector3.SmoothDamp(
            _gGrenadeSlot.localPosition,
            TargetGre,
            ref _velocityGre,
            _smoothTime
        );
    }
}
