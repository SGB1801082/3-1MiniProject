using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEngine.UI;

public class Hero : Ally
{
    protected override void Start()
    {
        /*base.Start();
        Debug.Log("Player 생성");
        foreach (GameObject _player in BattleManager.Instance.party_List)
        {
            BaseEntity player_Stat = _player.GetComponent<BaseEntity>();

            if (player_Stat.entity_index == GetComponent<BaseEntity>().entity_index)
            {
                InitStat(entity_index);
            }
        }*/

        foreach (var _party in BattleManager.Instance.party_List)
        {
            for (int i = 0; i < GameMgr.playerData.Count; i++)
            {
                if (_party.GetComponent<PartyData>() == GameMgr.playerData[i].partySlotData)
                {
                    InitStat(i, GameMgr.playerData[i].partySlotData);
                }
            }
        }
    }

    protected override void Update()
    {
        base.Update();

        /*if (_curstate == State.Skill)
        {
            Skill();
        }*/
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