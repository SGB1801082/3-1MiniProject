using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public float max_Hp;
    public float max_Mp;
    public float atkDmg;
    public float atkSpd;
    public float atkRange;

    public PlayerStat(float max_Hp, float max_Mp, float atkDmg, float atkSpd, float atkRange)
    {
        this.max_Hp = max_Hp;
        this.max_Mp = max_Mp;
        this.atkDmg = atkDmg;
        this.atkSpd = atkSpd;
        this.atkRange = atkRange;
    }
}
