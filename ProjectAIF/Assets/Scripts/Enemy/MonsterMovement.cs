
using UnityEngine;
using UnityEngine.AI;

public class MonsterMovement : MonoBehaviour
{
    [SerializeField] private Transform _crystalTF;
    [SerializeField] private Transform _playerTF;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Monster _monster;

    private void Start()
    {
        
        _navMeshAgent.speed = _monster.MoveSpeed;
        _navMeshAgent.angularSpeed = 200f;
        _navMeshAgent.acceleration = _monster.MoveSpeed * 0.5f;
        _navMeshAgent.stoppingDistance = _monster.AttackRange;
    }

    private void Update()
    {
        if (GameManager.Instance.IsCrystalNearPlayer)
        {
            _navMeshAgent.SetDestination(_crystalTF.position);    
        }
        else
        {
            _navMeshAgent.SetDestination(_playerTF.position);
        }
    }
}