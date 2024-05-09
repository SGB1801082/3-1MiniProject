using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss : BaseEntity
{
    private EntityStat stat;

    protected override void Start()
    {
        base.Start();
        Debug.Log("Enemy ( " + name + " ) 생성");

        // 고유 ID, 최대 HP, 최대 MP, 공격력, 공격속도, 공격사거리, 스킬유무 순으로 초기화
        stat = new EntityStat(51, 45, 0, 5f, 0.5f, 1.8f, false);

        entity_id = stat.id;
        max_Hp = stat.max_Hp;
        cur_Hp = max_Hp;
        max_Mp = stat.max_Mp;
        cur_Mp = 0;
        atkDmg = stat.atkDmg;
        SetAttackSpeed(stat.atkSpd);
        atkRange = stat.atkRange;
        able_Skill = stat.isSkill;
        isMelee = true;
    }

    protected override void Update()
    {
        base.Update();
    }
}
