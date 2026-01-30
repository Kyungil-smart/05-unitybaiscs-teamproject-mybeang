using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("Status")]
    public int CurrentHp;
    public int TotalHp;
    public float MoveSpeed;
    public int Defense;
    public int Exp;
    public int Level;
    
    [Header("Min/Max Value")]
    public int MinHp;
    public int MaxHp;
    public int MinMoveSpeed;
    public int MaxMoveSpeed;
    public int MinDefense;
    public int MaxDefense;
}
