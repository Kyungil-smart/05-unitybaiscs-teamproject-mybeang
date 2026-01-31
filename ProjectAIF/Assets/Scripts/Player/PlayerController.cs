using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float _moveSpeed = 5f;

    [Header("Mouse Look")]
    [SerializeField] private Transform _cameraTr;   // 자식 카메라 Transform 넣기
    [SerializeField] private float _mouseSensitivity = 2f;
    [SerializeField] private float _minPitch = -80f;
    [SerializeField] private float _maxPitch = 80f;

    private Rigidbody _rb;
    private Vector3 _dir = Vector3.zero;
    private float _pitch;           // 카메라 상하 각도 누적


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // FPS처럼 마우스 커서 잠그기
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        MouseLook();
    }

    private void FixedUpdate()
    {
        MoveRigidbody();
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
        _dir.x = Input.GetAxisRaw("Horizontal");
        _dir.z = Input.GetAxisRaw("Vertical");
        
        Vector3 moveDir = transform.TransformDirection(_dir);
        _rb.MovePosition(transform.position + moveDir.normalized * (_moveSpeed * Time.deltaTime));
    }
}

