using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseEntity
{
    private EntityStat stat;

    protected override void Start()
    {
        base.Start();
        Debug.Log("Enemy ( " + name + " ) 생성");

        // 고유 ID, 최대 HP, 최대 MP, 공격력, 공격속도, 공격사거리, 스킬유무 순으로 초기화
        stat = new EntityStat(50, 12, 0, 1f, 0.8f, 1.1f, false);

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
        if (_curstate == State.Skill)
        {
            Skill();
        }
    }


    public void Skill()
    {
        if (_curstate == State.Skill)
        {

            StopAllCoroutines();
            if (isAttack)
            {

                BaseEntity target = FindTarget().GetComponent<BaseEntity>();
                Debug.Log("타겟의 적에게 2배의 데미지로 한번 공격" + " " + (atkDmg * 2) + "데미지");
                target.cur_Hp -= atkDmg * 2;
                Debug.Log(target.cur_Hp + " " + target.name);
            }
            else
            {
                return;
            }
            cur_Mp = 0;
            ChangeState(State.Idle);
        }
    }
}