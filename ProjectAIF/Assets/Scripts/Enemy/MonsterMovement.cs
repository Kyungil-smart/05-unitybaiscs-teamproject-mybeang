
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class MonsterMovement : MonoBehaviour
{
    [SerializeField] private Transform _crystalTF;
    [SerializeField] private Transform _playerTF;
    [SerializeField] private Monster _monster;
    private NavMeshAgent _navMeshAgent;
    private bool _preMovingState;
    private bool _isMoving;
    public bool IsMoving
    {
        get { return _isMoving; }
        private set
        {
            _isMoving = value;
            if (_preMovingState != value)
            {
                OnMovingEvent?.Invoke(value);
                _preMovingState = value;
            }
        }
    }
    public UnityEvent<bool> OnMovingEvent;

    private void Awake()
    {
        OnMovingEvent =  new UnityEvent<bool>();
        FindCrystal();
        FindPlayer();
    }
    
    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _monster.MoveSpeed;
        _navMeshAgent.angularSpeed = 200f;
        _navMeshAgent.acceleration = _monster.MoveSpeed * 1.5f;
        _navMeshAgent.stoppingDistance = _monster.AttackRange;
        IsMoving = false;
    }

    private void Update()
    {
        float distance;
        if (GameManager.Instance.IsCrystalNearPlayer)
        {
            distance = Vector3.Distance(_monster.transform.position, _crystalTF.position);
            if (distance > _navMeshAgent.stoppingDistance)
            {
                IsMoving = true;
                // _monster.MonsterAnimator.SetBool("IsMove", true);
                _navMeshAgent.SetDestination(_crystalTF.position);
            }
            else
            {
                IsMoving = false;
                // _monster.MonsterAnimator.SetBool("IsMove", false);
                _navMeshAgent.SetDestination(_monster.transform.position);
                transform.LookAt(_crystalTF.position);
            }
        }
        else
        {
            distance = Vector3.Distance(_monster.transform.position, _playerTF.position);
            if (distance > _navMeshAgent.stoppingDistance)
            {
                IsMoving = true;
                // _monster.MonsterAnimator.SetBool("IsMove", true);
                _navMeshAgent.SetDestination(_playerTF.position);
            }
            else
            {
                IsMoving = false;
                // _monster.MonsterAnimator.SetBool("IsMove", false);
                _navMeshAgent.SetDestination(_monster.transform.position);
                transform.LookAt(_playerTF.position);
            }
        }
    }

    private void FindCrystal()
    {
        _crystalTF = GameObject.FindGameObjectWithTag("Crystal").transform;
    }
    private void FindPlayer()
    {
        _playerTF = GameObject.FindGameObjectWithTag("Player").transform;
    }
}