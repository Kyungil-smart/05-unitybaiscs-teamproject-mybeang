using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField] private Transform _playerTF;
    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _playerLayer;
    private Ray _ray;

    private void LateUpdate()
    {
        DetectPlayerRange();
    }
    
    private Vector3 GetDirectionToPlayer()
    {
        return (_playerTF.position - transform.position).normalized;
    }
    
    private void DetectPlayerRange()
    {
        Vector3 directionToPlayer = GetDirectionToPlayer();
        _ray = new Ray(transform.position, directionToPlayer);
        
        RaycastHit hit;
        if (Physics.Raycast(_ray, out hit, _distance, _playerLayer))
        {
            GameManager.Instance.IsCrystalNearPlayer = true;
            // Debug.Log("가까이 있어");
        }
        else
        {
            GameManager.Instance.IsCrystalNearPlayer = false;
            // Debug.Log("멀리 있어");
        }
    }

    private void OnDrawGizmos()
    {
         Gizmos.color = Color.red;
         Gizmos.DrawRay(_ray.origin, _ray.direction * _distance);
    }
}
