using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalOutline : MonoBehaviour
{
    [SerializeField] Behaviour[] outlines;

    void Awake()
    {
        LockOn(false);
    }

    public void LockOn(bool isLockOn)
    {
        if (outlines == null) return;

        for (int i = 0; i < outlines.Length; i++)
        {
            if (outlines[i] == null) continue;
            outlines[i].enabled = isLockOn;

        }

    }
}
