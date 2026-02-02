using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private Transform _playerTf;
    [SerializeField] private Transform _respawnPoint;
    public void PlayerToRespawnPoint() 
    {
        _playerTf.position = _respawnPoint.position;
    }
        
}
