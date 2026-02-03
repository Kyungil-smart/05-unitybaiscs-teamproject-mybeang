using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSkill : MonoBehaviour
{
    [SerializeField] private string enemyTag = "Enemy";

    public bool IsUsed;

    public void Activate()
    {
        if (IsUsed) return;
        IsUsed = true;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (var e in enemies)
        {
            MeleeMonster mMonster = e.GetComponent<MeleeMonster>();
            if (mMonster != null)
            {
                mMonster.Death();
            }

            RangeMonster rMonster = e.GetComponent<RangeMonster>();
            if (rMonster != null)
            {
                rMonster.Death();
            }
        }
    }
}