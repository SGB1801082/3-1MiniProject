using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject player_Statbar;
    public GameObject item_Bar;
    public GameObject party_List;
    public GameObject battleStart;
    public GameObject nextRoom;

    private void FixedUpdate()
    {
        if (BattleManager.Instance._curphase == BattleManager.BattlePhase.Rest || BattleManager.Instance._curphase == BattleManager.BattlePhase.End)
        {
            nextRoom.SetActive(true);
            player_Statbar.SetActive(false);
            item_Bar.SetActive(false);
            party_List.SetActive(false);
            battleStart.SetActive(false);
        }
        else
        {
            nextRoom.SetActive(false); 
        }


        if (BattleManager.Instance._curphase == BattleManager.BattlePhase.Battle)
        {
            player_Statbar.SetActive(true);
            item_Bar.SetActive(true);
        }
        else
        {
            player_Statbar.SetActive(false);
            item_Bar.SetActive(false);
        }

        if (BattleManager.Instance._curphase == BattleManager.BattlePhase.Deploy)
        {
            party_List.SetActive(true);
            battleStart.SetActive(true);
        }
        else
        {
            party_List.SetActive(false);
            battleStart.SetActive(false);
        }
    }
}
