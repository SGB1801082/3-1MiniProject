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
            if (i == GameMgr.playerData.Count - 1)
            {
                Debug.Log("playerData.Countcont: " + i);
            }

            if (i == GameMgr.playerData.Count)
            {
                Debug.Log("playerData.Count: " + i);
            }
        }

        for (int i = 0; i < GameUiMgr.single.lastDeparture.Count; i++)
        {
            if (i == GameUiMgr.single.lastDeparture.Count -1)
            {
                Debug.Log("lastDeparture cnt: "+i);
            }
        }
    }
}
