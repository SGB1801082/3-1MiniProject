using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

/*public enum State { 
    Ranger, 
    Magician,
    Knight
}*/

public class PartyData
{
    //Party Panel Act
    public string strPartyName;
    public string strName;

    public string Type;

    public int cost = 128;
    //public int index;

    public Sprite spPartyIcon;

    //Party Stat .. hp, atk, ... 여기넣어야되나
    //public State myState;//플레이어블 프리펩을 결정할 상태변수

    // Ranger.cs에서 고유 id, 최대 HP, 최대 MP, 공격력, 공격속도, 공격사거리 순으로 초기화
    public int partyJobIndex;// 고유 id == 직업인덱스
    public float partyHp;
    public float partyMp;
    public float partyAtk;
    public float partyAtkSpd;
    public float partyAtkRange;

    public bool partyAbleSkill;

    public PartyData(GameObject prefab, int _Lvel)
    {
        BaseEntity player = prefab.GetComponent<BaseEntity>();

        partyJobIndex = player.entity_id;

        //GenerateStat(partyJobIndex, _Lvel, player);
        //이 아래의 정보는 추후 제거
        partyHp = player.max_Hp;
        partyMp = player.max_Mp;
        partyAtk = player.atkDmg;
        partyAtkSpd = player.atkSpd;
        partyAtkRange = player.atkRange;
        partyAbleSkill = player.able_Skill;
        cost = Random.Range(50 + _Lvel*10, 100+ _Lvel*50);
        spPartyIcon = player.GetComponent<SpriteRenderer>().sprite;
    }


    public void GenerateStat(int _Code, int _Lvel, BaseEntity entity)
    {
        switch (_Code)
        {
            case 1:
                Debug.Log("Type Ranger, Generate Code: "+_Code);
                partyHp = entity.max_Hp + (0.01f* _Lvel);
                partyMp = entity.max_Mp + (0.01f * _Lvel);
                partyAtk = entity.atkDmg;
                partyAtkSpd = entity.atkSpd;
                partyAtkRange = entity.atkRange;
                break;
            case 2:
                break;
            case 3:
                break;
            default: 
                break;
        }
    }

}
