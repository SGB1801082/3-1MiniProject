using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStat : MonoBehaviour
{
    public float max_Hp;
    public float cur_hp;
    public float max_Mp;
    public float atkDmg;
    public float atkSpd;
    public float atkRange;

    public EntityStat(float max_Hp, float cur_hp, float max_Mp, float atkDmg, float atkSpd, float atkRange)
    {
        this.max_Hp = max_Hp;
        this.cur_hp = cur_hp;  
        this.max_Mp = max_Mp;
        this.atkDmg = atkDmg;
        this.atkSpd = atkSpd;
        this.atkRange = atkRange;
    }
}
