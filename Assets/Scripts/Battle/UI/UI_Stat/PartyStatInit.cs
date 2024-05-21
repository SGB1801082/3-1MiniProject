using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyStatInit : MonoBehaviour
{
    public GameObject party_Stat_Prefab;
    private List<GameObject> stats = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < BattleManager.Instance.party_List.Count; i++)
        {
            GameObject obj = Instantiate(party_Stat_Prefab, transform);
            StatManager stat_Obj = obj.GetComponent<StatManager>();
            stats.Add(obj);

            BaseEntity player = BattleManager.Instance.party_List[i].GetComponent<BaseEntity>();
            Sprite sprite = BattleManager.Instance.party_List[i].GetComponent<SpriteRenderer>().sprite;

            stat_Obj.InitStat(player, sprite);
        }
    }


    private void OnEnable()
    {
        UpdateStatInit();
    }


    public void UpdateStatInit()
    {
        for (int i = 0; i < BattleManager.Instance.party_List.Count; i++)
        {
            BaseEntity player = BattleManager.Instance.party_List[i].GetComponent<BaseEntity>();
            Sprite sprite = BattleManager.Instance.party_List[i].GetComponent<SpriteRenderer>().sprite;

            stats[i].GetComponent<StatManager>().InitStat(player, sprite);
        }
    }



}
