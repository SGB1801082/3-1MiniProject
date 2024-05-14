using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyStat : MonoBehaviour
{
    public static PartyStat single;

    private EntityStat stat;
    private void Awake()
    {
        single = this;
    }
 /*   public void GenratePartyStat()
    {
        foreach (listPartyData)
        {
            switch (listPartyData.partyJobIndex)
            {
                case 1: Ranger();
                    isMelee = false; // 임시로 근접 유닛과 똑같은 방식으로 공격 추후에 투사체를 발사하는 방식으로 바꿀 예정  
                    break;
            }
        }
        Debug.Log("뭐시깽이 생성");
    }
    public void CreateParty(PartyData _partyData)
    {
        // 고유 id, 최대 HP, 최대 MP, 공격력, 공격속도, 공격사거리 순으로 초기화
        *//*stat = new EntityStat
            (1, 25, 5, 2, 1, 8, false);*//*

        GameUiMgr.single.objListPlayable[1].GetComponent<BaseEntity>().entity_id = _partyData.partyJobIndex;
        entity_id = _partyData.partyJobIndex;// 실제 표시될 파티원 리스트순서라고 생각해도 되는듯?
        max_Hp = _partyData.partyHp;
        cur_Hp = max_Hp;
        max_Mp = _partyData.partyMp;
        cur_Mp = 0;
        atkDmg = _partyData.partyAtk;
        SetAttackSpeed(_partyData.partyAtkSpd);
        atkRange = _partyData.partyAtkRange;
        able_Skill = _partyData.partyAbleSkill;
        
    }
    public void qusehd(int _lv)
    {
        if (파티데이터.스테이트 == State.Knight)
        {
            maxhp = maxhp + (maxhp * 0.1 * _lv);
        }
    }*/

}
