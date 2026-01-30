using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using static PlayerManager;

public class PlayerWeapon : MonoBehaviour, IAttackable
{
    [SerializeField] GameObject Player;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject GrenadePoint;

    [Header("Weapon")]
    public GameObject Pistol;
    public GameObject AR;
    public GameObject Grenade;

    private int _maxMagazine; // 탄창 Max
    private int _currentMagazine = 30; // 현재 남은 수


    [Header("Attack")]
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _attackTargetLayer;
    [SerializeField] private int _attackDamage;

    private IDamageable _targetDamagable;
    private Transform _targetTransform;

    [Header("Slot")]
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
    float _roatationSpeed = 2.0f;

    bool _pickPistol = false;
    bool _pickAR = false;
    bool _pickGrenade = false;
    bool _reLoading = false;
    bool _isThrowing = false;
    bool _isTrhowCoroutin = false;
    bool _isSwapable = true;
    Quaternion targetRotation;
    Vector3 ArmPos;
    Vector3 IdleArmPos;

    PlayerManager _playerManager;

    //============================= 플레이어 무기동작에 관련한 변수목록========================
    public int PlayerCurrentWeapon = 0;
    int Damage;

    

    private void Start()
    {
        WeaponPosCheck();
        _playerManager = PlayerManager.Instance;
        GiveBaseWeapon();
    }
    private void Update()
    {
        if(_playerManager != null) WeaponChange();
        CheckCurWeapon();
        OnButton();
        VisualSwap();
        if (Input.GetMouseButtonDown(0)) Attack(Damage);
        
        if (_isThrowing && !_isTrhowCoroutin)
        {
            StartCoroutine(ThrowArmRotation());
        }
        
    }

    void GiveBaseWeapon()
    {
        Pistol = _playerManager.Pistol;
        GameObject pistol = Instantiate(Pistol, _pistolSlot);
        pistol.transform.SetParent(_pistolSlot);

        AR = _playerManager.AR;
        GameObject ar = Instantiate(AR, _aRSlot);
        ar.transform.SetParent(_aRSlot);

        Grenade = _playerManager.Grenade;
        GameObject grenade = Instantiate(Grenade, _gGrenadeSlot);
        grenade.transform.SetParent(_gGrenadeSlot);
    }
    
    void WeaponChange()
    {
        /*
        if (Pistol.GetComponent<WeaponID>().ID == _playerManager.Pistol.GetComponent<WeaponID>().ID && 
            AR.GetComponent<WeaponID>().ID == _playerManager.AR.GetComponent<WeaponID>().ID &&
            Grenade.GetComponent<WeaponID>().ID == _playerManager.Grenade.GetComponent<WeaponID>().ID)
        {
            Debug.Log($"{Pistol.GetComponent<WeaponID>().ID} == {_playerManager.Pistol.GetComponent<WeaponID>().ID}");
            return; 
        }
        */

        Pistol = _playerManager.Pistol;
        GameObject pistol = Instantiate(Pistol, _pistolSlot);
        pistol.transform.SetParent(_pistolSlot);


        AR = _playerManager.AR;
        GameObject ar = Instantiate(AR, _aRSlot);
        ar.transform.SetParent(_aRSlot);

        Grenade = _playerManager.Grenade;
        GameObject grenade = Instantiate(Grenade, _gGrenadeSlot);
        grenade.transform.SetParent(_gGrenadeSlot);
    }
    
    
    // 플레이어가 입력하는 모든 버튼입력을 처리하는 함수
    private void OnButton()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    // 시작과 동시에 무기슬롯이 어느위치에 놓여야하는지 계산하는 함수입니다
    // TODO : 간이적으로 수치가 조정되어있으니 차후에 더욱 적절한 값 대입요망
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

        IdleArmPos = _gGrenadeSlot.localPosition;
    }

    // 1,2,3 키입력을 받고 현재 어떤 무기를 선택중인지 저장하는 함수
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

    // 무기가 스왑되는 시각적인 효과를 제공한다
    void VisualSwap()
    {
        if (_isSwapable == false)
        {
            return;
        }

        Vector3 TargetPistol = _pickPistol ? _basePosPistol : _downPosPistol;

        _pistolSlot.localPosition = Vector3.SmoothDamp
        (
        _pistolSlot.localPosition,
        TargetPistol,
        ref _velocityPistol,
        _smoothTime
        );

        Vector3 TargetAR = _pickAR ? _basePosAR : _downPosAR;

        _aRSlot.localPosition = Vector3.SmoothDamp
        (
        _aRSlot.localPosition,
        TargetAR,
        ref _velocityAR,
        _smoothTime
        );

        Vector3 TargetGre = _pickGrenade ? _basePosGre : _downPosGre;

        _gGrenadeSlot.localPosition = Vector3.SmoothDamp
        (
        _gGrenadeSlot.localPosition,
        TargetGre,
        ref _velocityGre,
        _smoothTime
        );
    }

    

    public void Attack(int damage)
    {
        if (_currentMagazine <= 0)
        {
            Reload();
        }

        if (PlayerCurrentWeapon == 1 || PlayerCurrentWeapon == 2)
        {
            DetectTarget();
            if (_reLoading != true)
            {
                Fire();
            }
        }

        if (PlayerCurrentWeapon == 3)
        {
            if (_isThrowing == false) Throw();
        }
    }

    private void DetectTarget()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _attackRange, _attackTargetLayer))
        {
            if (_targetTransform != hit.transform)
            {
                _targetTransform = hit.transform;
                _targetDamagable = hit.transform.GetComponent<IDamageable>();
            }
        }
        else
        {
            _targetDamagable = null;
            _targetTransform = null;
        }
    }

    //권총 AR의 경우 데미지를줌
    private void Fire()
    {
        _isSwapable = false;
        if (_targetDamagable == null)
        {
            return;
        }
        if (PlayerCurrentWeapon == 3)
        {
            return;
        }

        // TODO : 몬스터에게 데미지를 주는 기능

        _isSwapable = true;
    }

    //장전할것은 명령하는 함수
    private void Reload()
    {
        if (_reLoading) return;

        _reLoading = true;
        _isSwapable = false;
        StartCoroutine(ReLoadRoutine());
    }

    // 장전하는 모션 총구를 살짝 플레이쪽으로 돌리며 현재 총알수가 장전됩니다
    private IEnumerator ReLoadRoutine()
    {

        if (PlayerCurrentWeapon == 1)
        {
            targetRotation = _pistolSlot.localRotation * Quaternion.Euler(0, -45, 0);
            
            // TODO : 사운드 입력  철컥! 장전소리!


            while (Quaternion.Angle(_pistolSlot.localRotation, targetRotation) > 0.1f)
            {
                _pistolSlot.localRotation = Quaternion.RotateTowards
                (
                _pistolSlot.localRotation,
                targetRotation,
                100f * Time.deltaTime
                );

                yield return null;
            }

            yield return new WaitForSeconds(1f);
            _currentMagazine = _maxMagazine;

            targetRotation = _pistolSlot.localRotation * Quaternion.Euler(0, 45, 0);
            while (Quaternion.Angle(_pistolSlot.localRotation, targetRotation) > 0.1f)
            {
                _pistolSlot.localRotation = Quaternion.RotateTowards
                (
                _pistolSlot.localRotation,
                targetRotation,
                100f * Time.deltaTime
                );

                yield return null;
            }

            _reLoading = false;
            _isSwapable = true;
        }

        if (PlayerCurrentWeapon == 2)
        {
            targetRotation = _aRSlot.localRotation * Quaternion.Euler(0, -45, 0);

            // TODO : 사운드 입력  철컥! 장전소리!


            while (Quaternion.Angle(_aRSlot.localRotation, targetRotation) > 0.1f)
            {
                _aRSlot.localRotation = Quaternion.RotateTowards
                (
                _aRSlot.localRotation,
                targetRotation,
                100f * Time.deltaTime
                );

                yield return null;
            }

            yield return new WaitForSeconds(1f);
            _currentMagazine = _maxMagazine;

            targetRotation = _aRSlot.localRotation * Quaternion.Euler(0, 45, 0);
            while (Quaternion.Angle(_aRSlot.localRotation, targetRotation) > 0.1f)
            {
                _aRSlot.localRotation = Quaternion.RotateTowards
                (
                _aRSlot.localRotation,
                targetRotation,
                100f * Time.deltaTime
                );

                yield return null;
            }

            _reLoading = false;
            _isSwapable = true;
        }


    }

    // 수류탄을 던지는 모션과 수류탄생성및 포물선 각도로 발사하는 기능을 담은 함수
    void Throw()
    {
        _isThrowing = true;
        _isSwapable = false;
        // 수류탄 투척시 팔회전 각도 계산
        ArmPos = _gGrenadeSlot.localPosition + new Vector3(0.6f, 1.6f, -0.45f);
    }

    IEnumerator ThrowArmRotation()
    {
        _isTrhowCoroutin = true;

        while (Vector3.Distance(_gGrenadeSlot.localPosition, ArmPos) > 0.01f)
        {
            _gGrenadeSlot.localPosition = Vector3.SmoothDamp
            (
                _gGrenadeSlot.localPosition,
                ArmPos,
                ref _velocityGre,
                _smoothTime
            );

            yield return null;
        }

        GrenadeInstantiate();
        yield return new WaitForSeconds(0.5f);
        _velocityGre = Vector3.down * 0.5f;

        while (Vector3.Distance(_gGrenadeSlot.localPosition, IdleArmPos) > 0.01f)
        {
            _gGrenadeSlot.localPosition = Vector3.SmoothDamp
            (
                _gGrenadeSlot.localPosition,
                IdleArmPos,
                ref _velocityGre,
                _smoothTime
            );

            yield return null;
        }

        _isThrowing = false;
        _isTrhowCoroutin = false;
        _isSwapable = true;
    }

    void GrenadeInstantiate()
    {
        GameObject grenadeObj = Instantiate(Grenade, GrenadePoint.transform.position, GrenadePoint.transform.rotation);

        Rigidbody rb = grenadeObj.GetComponent<Rigidbody>();
        rb.isKinematic = false;

        SphereCollider col = grenadeObj.AddComponent<SphereCollider>();
        col.radius = 0.15f;      
        col.isTrigger = false;

        Vector3 throwDir = GrenadePoint.transform.forward * 14f + GrenadePoint.transform.up * 8f;

        rb.AddForce(throwDir, ForceMode.Impulse);
    }

    
}
