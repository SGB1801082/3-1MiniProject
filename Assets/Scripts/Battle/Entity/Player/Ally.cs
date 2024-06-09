using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : BaseEntity
{
    public enum JobClass
    {
        Hero,
        Knight,
        Ranger,
        Wizard
    }

    public JobClass job;

    private string strName;
    private string strLv;

    protected override void Update()
    {
        base.Update();
        if (BattleManager.Instance._curphase == BattleManager.BattlePhase.Battle)
        {
            UpdateCurrentHPToSingle();
        }
    }

    public void UpdateCurrentHPToSingle()
    {
        GameMgr.playerData[entity_index].cur_Player_Hp = cur_Hp;
    }

    public void Init(int index, PlayerData player)
    {
        entity_index = index;
        strName = player.GetPlayerName();
        strLv = player.player_level.ToString();
        
        max_Hp = player.max_Player_Hp;
        cur_Hp = max_Hp;
        max_Mp = player.max_Player_Mp;
        cur_Mp = 0;
        atkDmg = player.base_atk_Dmg;
        atkSpd = player.atk_Speed;
        atkRange = player.atk_Range;
        isMelee = player.isMelee;
        able_Skill = player.skill_Able;

        Debug.Log("Index: "+index + "\tName: " + strName + "\tLv: " + strLv);
    }

    public void InitStat(int index)
    {
        this.stat = new(
            GameMgr.playerData[index].max_Player_Hp,
            GameMgr.playerData[index].cur_Player_Hp,
            GameMgr.playerData[index].max_Player_Mp,
            GameMgr.playerData[index].base_atk_Dmg,
            GameMgr.playerData[index].atk_Speed,
            GameMgr.playerData[index].atk_Range
            );

        max_Hp = stat.max_Hp;
        cur_Hp = stat.cur_hp;
        max_Mp = stat.max_Mp;
        cur_Mp = 0f;
        atkDmg = stat.atkDmg;
        SetAttackSpeed(stat.atkSpd);
        atkRange = stat.atkRange;
    }
}
