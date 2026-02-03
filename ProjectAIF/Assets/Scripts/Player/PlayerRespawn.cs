using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private Transform _playerTf;
    [SerializeField] private Transform _respawnPoint;
    [SerializeField] private AudioClip _playerRespawnSound;

    public void PlayerToRespawnPoint() 
    {
        _playerTf.position = _respawnPoint.position;
        AudioManager.Instance.PlaySound(_playerRespawnSound);
    }
}
