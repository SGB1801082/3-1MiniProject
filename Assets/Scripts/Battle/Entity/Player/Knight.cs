using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Knight : Ally
{
    //Transform cur_target;


    protected override void Start()
    {
        base.Start();
        Debug.Log("Knight 생성");

        foreach (GameObject player in BattleManager.Instance.party_List)
        {
            BaseEntity player_Stat = player.GetComponent<BaseEntity>();

            if (player_Stat.entity_index == GetComponent<BaseEntity>().entity_index)
            {
                InitStat(entity_index);
            }
        }

        /*// 고유 id, 최대 HP, 최대 MP, 공격력, 공격속도, 공격사거리 순으로 초기화
        stat = new EntityStat
            (3, 30f, 10f, 3f, 1f, 1.1f, false);

        entity_id = stat.id;
        max_Hp = stat.max_Hp;
        cur_Hp = max_Hp;
        max_Mp = stat.max_Mp;
        cur_Mp = 0;
        atkDmg = stat.atkDmg;
        SetAttackSpeed(stat.atkSpd);
        atkRange = stat.atkRange;
        able_Skill = stat.isSkill;
        isMelee = true; // 임시로 근접 유닛과 똑같은 방식으로 공격 추후에 투사체를 발사하는 방식으로 바꿀 예정*/


        //SetAttackSpeed(atkSpd);
    }
   

    protected override void Update()
    {
        base.Update();
        /*if (_curstate == State.Skill)
        {
            Skill();
        }*/
        //cur_target = target;
    }


    /*public void Skill()
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
    }*/
}