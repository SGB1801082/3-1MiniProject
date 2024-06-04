using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Ranger : BaseEntity
{
    private EntityStat stat;
    //Transform cur_target;
    int chek = 0;

    protected override void Start()
    {
        base.Start();
        Debug.Log("Ranger 생성");

        // 고유 id, 최대 HP, 최대 MP, 공격력, 공격속도, 공격사거리 순으로 초기화
        /*foreach (var _slot in GameUiMgr.single.lsastDeparture)
        {
            if (_slot.partyData.partyJobIndex == 2)
            {
                chek++;
            }
        }


        if (chek == 1)
        {
            foreach (var _slot in GameUiMgr.single.lsastDeparture)
            {
                if (_slot.partyData.partyJobIndex == 2)
                {
                    stat = new(1, _slot.partyData.partyHp, 5, 2, 1, 8, false);
                }
            }
        }
        else if (chek >= 2)
        {
            for (int i = 0; i < GameUiMgr.single.lsastDeparture.Count; i++)
            {
                if (true)
                {

                }
            }
        }
        else
        {
            return;
        }*/


        /*stat = new(1, 25, 5, 2, 1, 8, false);


        entity_id = stat.id;
        max_Hp = stat.max_Hp;
        cur_Hp = max_Hp;
        max_Mp = stat.max_Mp;
        cur_Mp = 0;
        atkDmg = stat.atkDmg;
       
        atkRange = stat.atkRange;
        able_Skill = stat.isSkill;
        isMelee = false; // 임시로 근접 유닛과 똑같은 방식으로 공격 추후에 투사체를 발사하는 방식으로 바꿀 예정
*/
        SetAttackSpeed(atkSpd);
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