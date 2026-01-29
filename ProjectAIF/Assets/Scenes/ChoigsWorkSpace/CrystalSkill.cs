using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSkill : MonoBehaviour
{
    [SerializeField] private string enemyTag = "Enemy";

    private bool used = false;

    public void Activate()
    {
        if (used) return;
        used = true;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (var e in enemies)
            Destroy(e);
    }


}
