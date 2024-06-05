using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPDList : MonoBehaviour
{
    public List<PlayerData> ld = new();

    public List<PartySlot> ps = new();

    private void Start()
    {
        ld.Clear();
        ps.Clear();
        for (int i = 0; i < GameMgr.playerData.Count; i++)
        {
            Debug.Log("playerData.Countcont: " + GameMgr.playerData.Count);
        }

        for (int i = 0; i < GameUiMgr.single.lastDeparture.Count; i++)
        {
            Debug.Log("lastDeparture cnt: " + GameUiMgr.single.lastDeparture.Count);
        }
    }
}
