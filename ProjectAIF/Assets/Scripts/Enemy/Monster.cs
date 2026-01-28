using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster
{
    public int AttValue { get; protected set; }
    public int DefValue { get; protected set; }
    public int BaseHp { get; protected set; }
    public int CritChance { get; protected set; }
    public float CritMultiValue { get; protected set; }
    public float MoveSpeed { get; protected set; }
    public float AttackRate { get; protected set; }
    public int Exp{get; protected set;}
}
