
using UnityEngine;
using UnityEngine.AI;

public class MonsterMovement : MonoBehaviour
{
    [SerializeField] private Transform _crystalTF;
    [SerializeField] private Transform _playerTF;
    [SerializeField] private Monster _monster;
    private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _monster.MoveSpeed;
        _navMeshAgent.angularSpeed = 200f;
        _navMeshAgent.acceleration = _monster.MoveSpeed * 1.5f;
        _navMeshAgent.stoppingDistance = _monster.AttackRange;
    }

    private void Update()
    {
        float distance;
        if (GameManager.Instance.IsCrystalNearPlayer)
        {
            distance = Vector3.Distance(_monster.transform.position, _crystalTF.position);
            if (distance > _navMeshAgent.stoppingDistance)
            {
                _monster.MonsterAnimator.SetBool("IsMove", true);
                _navMeshAgent.SetDestination(_crystalTF.position);
            }
            else
            {
                _monster.MonsterAnimator.SetBool("IsMove", false);
                _navMeshAgent.SetDestination(_monster.transform.position);
            }
        }
        else
        {
            distance = Vector3.Distance(_monster.transform.position, _playerTF.position);
            if (distance > _navMeshAgent.stoppingDistance)
            {
                _monster.MonsterAnimator.SetBool("IsMove", true);
                _navMeshAgent.SetDestination(_playerTF.position);
            }
            else
            {
                _monster.MonsterAnimator.SetBool("IsMove", false);
            }
        }
    }
}