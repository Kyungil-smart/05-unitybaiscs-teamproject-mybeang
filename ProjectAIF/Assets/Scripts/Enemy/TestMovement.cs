
using UnityEngine;
using UnityEngine.AI;

public class TestMovement : MonoBehaviour
{
    public Transform playerTF;
    public NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent.SetDestination(playerTF.position);
    }
}