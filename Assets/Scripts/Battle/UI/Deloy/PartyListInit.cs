using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyListInit : MonoBehaviour
{
    public GameObject party_Prefab;

    private void Start()
    {
        for (int i = 0; i < BattleManager.Instance.party_List.Count; i++)
        {
            GameObject obj = Instantiate(party_Prefab, transform);
            UnitPlacement unit = obj.GetComponent<UnitPlacement>();

            unit.InitList(BattleManager.Instance.party_List[i], BattleManager.Instance.party_List[i].GetComponent<SpriteRenderer>().sprite);
        }
    }
}
