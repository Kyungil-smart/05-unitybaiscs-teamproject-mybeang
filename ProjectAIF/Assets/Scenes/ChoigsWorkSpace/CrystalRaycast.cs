using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalRaycast : MonoBehaviour
{
    public float maxDistance = 8f;

    private Camera cam;
    private CrystalOutline current;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        CrystalOutline target = RaycastCrystal();

        if (target != current)
        {
            if (current != null)
                current.LockOn(false);

            current = target;

            if (current != null)
                current.LockOn(true);

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
