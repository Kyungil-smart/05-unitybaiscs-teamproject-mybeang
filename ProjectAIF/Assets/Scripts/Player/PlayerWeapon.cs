using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using static PlayerManager;

public class PlayerWeapon : MonoBehaviour, IAttackable
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _grenadePoint;
    private Camera _camera;

    [Header("Weapon")]
    public PistolStatus PistolStatus;
    public RifleStatus RifleStatus;
    public GrenadeStatus GrenadeStatus;
    private WeaponStatusBase[] _weapons = new WeaponStatusBase[3];

    [SerializeField] private GameObject _pistolObject;
    [SerializeField] private GameObject _rifleObject;
    [SerializeField] private GameObject _grenadeObject;
    private GameObject[] _weaponObjects = new GameObject[3];
    
    [SerializeField] private Transform _enPistolPos;
    [SerializeField] private Transform _enRiflePos;
    [SerializeField] private Transform _enGrenadePos;
    private Transform[] _enWpPosArr = new Transform[3];
    
    [SerializeField] private Transform _disPistolPos;
    [SerializeField] private Transform _disRiflePos;
    [SerializeField] private Transform _disGrenadePos;
    private Transform[] _disWpPosArr = new Transform[3];
    private int _curWpIndex;
    
    [Header("Attack")]
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _attackTargetLayer;
    private Coroutine _attackCoroutine;

    [Header("Sounds")] 
    [SerializeField] private AudioClip _pistolAttackSound;
    [SerializeField] private AudioClip _rifleAttackSound;
    [SerializeField] private AudioClip _grenadeAttackSound;
    
    [SerializeField] private AudioClip _pistolSwapSound;
    [SerializeField] private AudioClip _rifleSwapSound;

    [SerializeField] private AudioClip _ReloadSound;
    
    [SerializeField] private AudioClip _attackSound;
    
    private IDamageable _targetDamagable;
    private Transform _targetTransform;
    
    private bool _reLoading;
    private bool _isThrowing;
    private bool _isTrhowCoroutin;
    private bool _isSwapable;
    private Quaternion _targetRotation;
    private Vector3 _idleArmPos;
    private Ray _ray;

    private void Awake()
    {
        Init();
    }
    
    private void Start()
    {
        ReadyWeapon();
    }
    private void Update()
    {
        DetectTarget();
        OnButton();
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(_ray.origin, _ray.direction * _attackRange);
    }

    private void Init()
    {
        _weapons[0] = PistolStatus;
        _weapons[1] = RifleStatus;
        _weapons[2] = GrenadeStatus;
        _weaponObjects[0] = _pistolObject;
        _weaponObjects[1] = _rifleObject;
        _weaponObjects[2] = _grenadeObject;
        _enWpPosArr[0] = _enPistolPos;
        _enWpPosArr[1] = _enRiflePos;
        _enWpPosArr[2] = _enGrenadePos;
        _disWpPosArr[0] = _disPistolPos;
        _disWpPosArr[1] = _disRiflePos;
        _disWpPosArr[2] = _disGrenadePos;
        
        _camera = Camera.main;
        _curWpIndex = 0;
        _reLoading = false;
        _isThrowing = false;
        _isTrhowCoroutin = false;
        _isSwapable = true;
    }

    void ReadyWeapon()
    {
        for (int i = 0; i < _weaponObjects.Length; i++)
        {
            _weaponObjects[i].transform.position = _disWpPosArr[i].position;
        }
        _weaponObjects[_curWpIndex].transform.position = _enWpPosArr[_curWpIndex].position;
    }
    
    // 플레이어가 입력하는 모든 버튼입력을 처리하는 함수
    private void OnButton()
    {
        // 공격
        if (Input.GetMouseButtonDown(0))
        {
            if (GameManager.Instance.IsPaused) return;
            AudioManager.Instance.PlaySound(_attackSound);
            _attackCoroutine = StartCoroutine(AttackCoroutine(_weapons[_curWpIndex].Damage));
            _isSwapable = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);    
            }
            _isSwapable = true;
        }
        // Reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (_curWpIndex < 2)
            {
                Reload();    
            }
        }
        // 아래는 Swap
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AudioManager.Instance.PlaySound(_pistolSwapSound);
            StartCoroutine(VisualSwap(0));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AudioManager.Instance.PlaySound(_rifleSwapSound);
            StartCoroutine(VisualSwap(1));
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(VisualSwap(2));
        }
    }

    // 무기가 스왑되는 시각적인 효과를 제공한다
    private IEnumerator VisualSwap(int index)
    {
        if (!_isSwapable || index == _curWpIndex)
        {
            yield break;
        }
        _isSwapable = false;
        float elapsedTime = 0f;
        float duration = 0.5f;
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float interval = elapsedTime / duration;
            _weaponObjects[index].SetActive(true);
            _weaponObjects[index].transform.position = 
                Vector3.Lerp(_disWpPosArr[index].position, _enWpPosArr[index].position, interval);
            _weaponObjects[_curWpIndex].transform.position = 
                Vector3.Lerp(_enWpPosArr[_curWpIndex].position, _disWpPosArr[_curWpIndex].position, interval);
            _weaponObjects[_curWpIndex].SetActive(false);
            yield return null;
        }
        _isSwapable = true;
        _curWpIndex = index;
    }

    public void Attack(int damage)
    {
        if (_curWpIndex < 2)
        {
            if (_weapons[_curWpIndex].CurrentMagazine <= 0)
            {
                Reload();
            }
            if (!_reLoading)
            {
                Fire(damage);
            }
        }
        else
        {
            if (!_isThrowing)
            {
                Throw();
                if (_isThrowing && !_isTrhowCoroutin)
                {
                    StartCoroutine(ThrowArmRotation());    
                }
            }
        }
    }

    public IEnumerator AttackCoroutine (int damage)
    {
        while (true)
        {
            Attack(damage);
            if (_curWpIndex == 2)
            {
                yield break;
            }
            yield return YieldContainer.WaitForSeconds(_weapons[_curWpIndex].AttackRate);
        }
    }

    private void DetectTarget()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(_ray, out hit, _attackRange, _attackTargetLayer))
        {
            if (_targetTransform == hit.transform) return;
            _targetTransform = hit.transform;
            _targetDamagable = hit.transform.GetComponent<IDamageable>();
        }
        else
        {
            _targetDamagable = null;
            _targetTransform = null;
        }
    }

    private void Fire(int damage)
    {
        _isSwapable = false;
        if (_curWpIndex == 0)
        {
            // pistol
            AudioManager.Instance.PlaySound(_pistolAttackSound);
        }
        else
        {
            // rifle
            AudioManager.Instance.PlaySound(_rifleAttackSound);
        }
        _weapons[_curWpIndex].CurrentMagazine--;
        if (_targetDamagable == null || !(_targetDamagable is IDamageable) || _curWpIndex == 2)
        {
            return;
        }
        _targetDamagable.TakeDamage(damage);
        _isSwapable = true;
    }

    //장전할것은 명령하는 함수
    private void Reload()
    {
        if (_reLoading) return;

        _reLoading = true;
        _isSwapable = false;
        AudioManager.Instance.PlaySound(_ReloadSound);
        StartCoroutine(ReloadCoroutine());
    }
    
    private IEnumerator ReloadCoroutine()
    {
        _isSwapable = false;
        float elapsedTime = 0f;
        float duration = 0.3f;

        Quaternion origin = _weaponObjects[_curWpIndex].transform.localRotation;
        Quaternion target = origin * Quaternion.Euler(0, 0, 45);
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float interval = elapsedTime / duration;
            _weaponObjects[_curWpIndex].transform.localRotation = Quaternion.Slerp(origin, target, interval);
            yield return null;
        }
        
        yield return new WaitForSeconds(1f);
        _weapons[_curWpIndex].CurrentMagazine = _weapons[_curWpIndex].TotalMagazine;
        
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float interval = elapsedTime / duration;
            _weaponObjects[_curWpIndex].transform.localRotation = Quaternion.Slerp(target, origin, interval);
            yield return null;
        }

        _reLoading = false;
        _isSwapable = true;
    }

    // 수류탄을 던지는 모션과 수류탄생성및 포물선 각도로 발사하는 기능을 담은 함수
    private void Throw()
    {
        _isThrowing = true;
        _weapons[_curWpIndex].CurrentMagazine--;
        // 수류탄 투척시 팔회전 각도 계산
        AudioManager.Instance.PlaySound(_grenadeAttackSound);
    }

    IEnumerator ThrowArmRotation()
    {
        _isTrhowCoroutin = true;
        Vector3 origin = _grenadeObject.transform.position;
        _grenadeObject.transform.position = _grenadePoint.transform.position;

        GrenadeInstantiate();
        yield return new WaitForSeconds(GrenadeStatus.AttackRate);

        _grenadeObject.transform.position = origin;

        _isThrowing = false;
        _isTrhowCoroutin = false;
        _isSwapable = true;
    }

    void GrenadeInstantiate()
    {
        GameObject grenadeObj = Instantiate(_grenadeObject, 
            _grenadePoint.transform.position, _grenadePoint.transform.rotation);
        
        Rigidbody rb = grenadeObj.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = true;

        SphereCollider col = grenadeObj.AddComponent<SphereCollider>();
        col.radius = 0.15f;      
        col.isTrigger = false;

        Vector3 throwDir = _grenadePoint.transform.forward * 14f + _grenadePoint.transform.up * 8f;

        rb.AddForce(throwDir, ForceMode.Impulse);
    }
}
