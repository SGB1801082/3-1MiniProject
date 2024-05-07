using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyListInit : MonoBehaviour
{
    public GameObject party_Prefab;
    private List<GameObject> party = new List<GameObject>();

    private void Start()
    {
        SpawnPartyList();
    }

    private void OnEnable()
    {
        DestroyPartyList();
        SpawnPartyList();
    }


    private void SpawnPartyList()
    {
        for (int i = 0; i < BattleManager.Instance.party_List.Count; i++)
        {
            GameObject obj = Instantiate(party_Prefab, transform);
            UnitPlacement unit = obj.GetComponent<UnitPlacement>();

            unit.InitList(BattleManager.Instance.party_List[i], BattleManager.Instance.party_List[i].GetComponent<SpriteRenderer>().sprite);

            party.Add(obj);
        }
    }

    private void DestroyPartyList()
    {
        foreach (GameObject obj in party)
        {
            Destroy(obj); // 이전에 생성된 클론 제거
        }
        party.Clear();
    }
}
