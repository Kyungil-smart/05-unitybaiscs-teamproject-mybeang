using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalOutline : MonoBehaviour
{
    [SerializeField] Behaviour[] outlines; //그대로 유지

    [Header("Quick Outline Colors")]
    [SerializeField] private Color defaultColor = Color.white; // 기본 흰색
    [SerializeField] private Color holdColor = Color.red;      // F 누르는 동안 빨간색

    void Awake()
    {
        LockOn(false);
    }

    public void LockOn(bool isLockOn)
    {
        if (outlines == null)
        {
            return;
        }

        for (int i = 0; i < outlines.Length; i++)
        {
            if (outlines[i] == null)
            {
                continue;
            }
            outlines[i].enabled = isLockOn;
        }

        // 락온되면 기본색(흰색) 적용
        if (isLockOn)
        {
            SetHoldVisual(false);
        }
           
    }

    //  F키 누르는 동안 빨간색 / 떼면 흰색
    public void SetHoldVisual(bool isHolding)
    {
        if (outlines == null)
        {
            return;
        }

        Color color = isHolding ? holdColor : defaultColor;

        for (int i = 0; i < outlines.Length; i++)
        {
            if (outlines[i] == null)
            {
                continue;
            }

            if (outlines[i] is Outline CrystalOutline)
            {
                CrystalOutline.OutlineColor = color;
            }
                
        }
    }
}
