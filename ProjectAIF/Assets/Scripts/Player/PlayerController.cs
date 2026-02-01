using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _runSpeed = 8f;
    private float _moveSpeed;

    [Header("Mouse Look")]
    [SerializeField] private Transform _cameraTr;   // 자식 카메라 Transform 넣기
    [SerializeField] private float _mouseSensitivity = 2f;
    [SerializeField] private float _minPitch = -80f;
    [SerializeField] private float _maxPitch = 80f;

    [Header("Sounds")]
    [SerializeField] private AudioClip _walkSound;
    [SerializeField] private AudioClip _jumpSound;
    
    private Rigidbody _rb;
    private Vector3 _dir = Vector3.zero;
    private float _pitch;           // 카메라 상하 각도 누적
    private bool _isJumping;
    private bool _isWalking;
    private Coroutine _walkCoroutine;

    private void Awake()
    {
        _moveSpeed = _walkSpeed;
        _isJumping = false;
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // FPS처럼 마우스 커서 잠그기
        CursorLook();
        StartCoroutine(PlayWalkSoundCoroutine());
    }

    private void Update()
    {
        if (GameManager.Instance.IsPaused)
        {
            CursorUnlock();   
        }
        else
        {
            CursorLook();
            MouseLook();
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.IsPaused) return;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _moveSpeed = _runSpeed;
        }
        else
        {
            _moveSpeed = _walkSpeed;
        }
        Jump();
        MoveRigidbody();
        Debug.Log(_isWalking);
    }

    private void CursorLook()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void CursorUnlock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    private void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        // 1) 몸체 좌우 회전
        transform.Rotate(0f, mouseX, 0f);

        // 2) 카메라 상하 회전 + 각도 제한
        _pitch -= mouseY;
        _pitch = Mathf.Clamp(_pitch, _minPitch, _maxPitch);

        if (_cameraTr != null)
            _cameraTr.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
    }

    private void MoveRigidbody()
    {
        _dir = Vector3.zero;
        _dir.x = Input.GetAxisRaw("Horizontal");
        _dir.z = Input.GetAxisRaw("Vertical");
        if (_dir == Vector3.zero)
        {
            _isWalking = false;
            return;
        }
        Vector3 moveDir = transform.TransformDirection(_dir);
        _rb.MovePosition(transform.position + moveDir.normalized * (_moveSpeed * Time.deltaTime));
        _isWalking = true;
    }

    private IEnumerator PlayWalkSoundCoroutine()
    {
        while (true)
        {
            if (_isWalking) AudioManager.Instance.PlaySound(_walkSound);
            yield return YieldContainer.WaitForSeconds((1f / _moveSpeed) * 10f);
        }
        yield return null;
    }
    
    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && !_isJumping)
        {
            _isJumping = true;
            AudioManager.Instance.PlaySound(_jumpSound);
            _rb.AddForce(transform.up * 7, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log($"OnCollisionEnter");
            _isJumping = false;
        }
    }
}

