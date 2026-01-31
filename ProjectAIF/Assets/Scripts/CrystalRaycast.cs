using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalRaycast : MonoBehaviour
{
    [SerializeField] private float _maxDistance = 8f;

    [Header("Hold Settings")]
    [SerializeField] private float _holdSeconds = 5f;

    private Camera _cam;
    private CrystalOutline _current;
    private CrystalSkill _currentSkill;
    private Ray _ray;

    private float holdTimer = 0f;

    void Awake()
    {
        _cam = Camera.main;
    }

    void Update()
    {
        CrystalOutline target = RaycastCrystal();

        // 타겟 변경되면 초기화
        if (target != _current)
        {
            if (_current != null)
            {
                _current.LockOn(false);
                _current.SetHoldVisual(false);
            }

            _current = target;
            holdTimer = 0f;

            if (_current != null)
            {
                _current.LockOn(true);
                _currentSkill = _current.GetComponentInParent<CrystalSkill>();
            }
            else
            {
                _currentSkill = null;
            }
        }

        if (_current == null) return;

        //F 홀드: 누르는 동안 빨간색, 5초 채우면 발동
        if (Input.GetKey(KeyCode.F))
        {
            _current.SetHoldVisual(true);
            holdTimer += Time.deltaTime;

            if (holdTimer >= _holdSeconds)
            {
                if (_currentSkill != null)
                    _currentSkill.Activate(); //맵 전체 몬스터 제거

                holdTimer = 0f;
                _current.SetHoldVisual(false); // 발동 후 흰색으로 복귀(원하면 유지로 바꿔도 됨)
            }
        }
        else
        {
            holdTimer = 0f;
            _current.SetHoldVisual(false);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(_ray.origin, _ray.direction * _maxDistance);
    }

    CrystalOutline RaycastCrystal()
    {
        _ray = _cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(_ray, out hit, _maxDistance))
        {
            return hit.collider.GetComponentInParent<CrystalOutline>();
        }

        return null;
    }
}
