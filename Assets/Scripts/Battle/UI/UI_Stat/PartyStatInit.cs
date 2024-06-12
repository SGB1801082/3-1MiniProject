using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyStatInit : MonoBehaviour
{
    public GameObject party_Stat_Prefab;
    private List<GameObject> stats = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < BattleManager.Instance.party_List.Count; i++)
        {
            GameObject obj = Instantiate(party_Stat_Prefab, transform);
            StatManager stat_Obj = obj.GetComponent<StatManager>();
            stats.Add(obj);

            PlayerData data = GameMgr.playerData[i];

            stat_Obj.InitStat(data, data.job, data.player_level, data.GetPlayerName());
        }
    }
}
