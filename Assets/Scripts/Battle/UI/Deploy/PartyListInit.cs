using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyListInit : MonoBehaviour
{
    public GameObject party_Prefab;
    private List<GameObject> party = new List<GameObject>();

/*    private void Start()
    {
        if (BattleManager.Instance._curphase == BattleManager.BattlePhase.Deploy)
        {
            SpawnPartyList();
        }
    }*/

    private void OnEnable()
    {
         DestroyPartyList();
         SpawnPartyList();
    }


    private void SpawnPartyList()
    {
        if (BattleManager.Instance.party_List.Count <= 0)
        {
            Debug.Log("파티 리스트에 플레이어가 없습니다");
            return;
        }
        /*else if (party.Count > 0)
        {
            foreach (GameObject party_obj in party) 
            {
                if (!party_obj.activeSelf)
                {
                    party_obj.SetActive(true);
                }
            }
        }*/
        else
        {
            for (int i = 0; i < BattleManager.Instance.party_List.Count; i++)
            {
                if (GameMgr.playerData[i].cur_Player_Hp <= 0)
                {
                    Debug.Log("죽은 파티원은 생성되지 않음");
                    continue;
                }
                else
                {
                    GameObject obj = Instantiate(party_Prefab, transform);
                    UnitPlacement unit = obj.GetComponent<UnitPlacement>();

                    unit.InitList(BattleManager.Instance.party_List[i], BattleManager.Instance.party_List[i].GetComponent<SpriteRenderer>().sprite, GameMgr.playerData[i]);
                    party.Add(obj);
                }
            }
        }



        
        /*for (int i = 0; i < GameUiMgr.single.poolMoveInSlot.Count; i++)
        {
            if (GameUiMgr.single.poolMoveInSlot[i].partyData != null)
            {
                GameObject obj = Instantiate(party_Prefab, transform);
                UnitPlacement unit = obj.GetComponent<UnitPlacement>();

                unit.InitList(GameUiMgr.single.poolMoveInSlot[i].partyData.obj_Data , GameUiMgr.single.poolMoveInSlot[i].partyData.obj_Data.GetComponent<SpriteRenderer>().sprite);

                party.Add(obj);
            }
        }*/
    }

    private void DestroyPartyList()
    {
        foreach (GameObject obj in party)
        {
            if (obj != null)
            {
                Destroy(obj); // 이전에 생성된 클론 제거
            }
            else
            {
                return;
            }
        }
        party.Clear();
    }
}
