using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseEntity
{
    protected override void Update()
    {
        base.Update();
    }

    public void InitStat(float max_Hp, float max_Mp, float atkDmg, float atkSpd, float atkRange)
    {
        float cur_hp = max_Hp;

        stat = new(
            max_Hp,
            cur_hp,
            max_Mp,
            atkDmg,
            atkSpd,
            atkRange
            );

        this.max_Hp = stat.max_Hp;
        this.cur_Hp = stat.cur_hp;
        this.max_Mp = stat.max_Mp;
        this.cur_Mp = 0f;
        this.atkDmg = stat.atkDmg;
        SetAttackSpeed(stat.atkSpd);
        this.atkRange = stat.atkRange;
    }
}