using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalRaycast : MonoBehaviour
{
    [SerializeField]private float maxDistance = 8f;

    [Header("Hold Settings")]
    [SerializeField] private float holdSeconds = 5f;

    private Camera cam;
    private CrystalOutline current;
    private CrystalSkill currentSkill;

    private float holdTimer = 0f;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        CrystalOutline target = RaycastCrystal();

        // 타겟 변경되면 초기화
        if (target != current)
        {
            if (current != null)
            {
                current.LockOn(false);
                current.SetHoldVisual(false);
            }

            current = target;
            holdTimer = 0f;

            if (current != null)
            {
                current.LockOn(true);
                currentSkill = current.GetComponentInParent<CrystalSkill>();
            }
            else
            {
                currentSkill = null;
            }
        }

        if (current == null) return;

        //F 홀드: 누르는 동안 빨간색, 5초 채우면 발동
        if (Input.GetKey(KeyCode.F))
        {
            current.SetHoldVisual(true);
            holdTimer += Time.deltaTime;

            if (holdTimer >= holdSeconds)
            {
                if (currentSkill != null)
                    currentSkill.Activate(); //맵 전체 몬스터 제거

                holdTimer = 0f;
                current.SetHoldVisual(false); // 발동 후 흰색으로 복귀(원하면 유지로 바꿔도 됨)
            }
        }
        else
        {
            holdTimer = 0f;
            current.SetHoldVisual(false);
        }
    }

    CrystalOutline RaycastCrystal()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            return hit.collider.GetComponentInParent<CrystalOutline>();
        }

        return null;
    }
}
