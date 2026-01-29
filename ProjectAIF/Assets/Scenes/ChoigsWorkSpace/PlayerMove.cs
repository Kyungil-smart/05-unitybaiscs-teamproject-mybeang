using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour, ITargetable
{
    [Header("Move")]
    [SerializeField] private float _moveSpeed = 5f;

    [Header("Mouse Look")]
    [SerializeField] private Transform _cameraTr;   // 자식 카메라 Transform 넣기
    [SerializeField] private float _mouseSensitivity = 2f;
    [SerializeField] private float _minPitch = -80f;
    [SerializeField] private float _maxPitch = 80f;

    private Rigidbody _rb;
    private Vector2 _moveInput;     // (x: 좌우, y: 앞뒤)
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
        ReadMoveInput();
        MouseLook();
    }

    private void FixedUpdate()
    {
        MoveRigidbody();
    }

    private void ReadMoveInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        _moveInput = new Vector2(h, v).normalized;
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
        Vector3 moveDir = (transform.right * _moveInput.x) + (transform.forward * _moveInput.y);
        Vector3 targetVel = moveDir * _moveSpeed;

        targetVel.y = _rb.velocity.y;
        _rb.velocity = targetVel;
    }

    public void SetTarget()
    {
        
    }

    public void UnsetTarget()
    {
        
    }
}