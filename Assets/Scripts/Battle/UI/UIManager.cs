using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject player_Statbar;
    public GameObject item_Bar;
    public GameObject party_List;
    public GameObject deploy_Area;
    public GameObject battleStart;
    public GameObject unit_Deloy_Area;

    private void FixedUpdate()
    {
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
            deploy_Area.SetActive(true);
            battleStart.SetActive(true);
            unit_Deloy_Area.SetActive(true);
        }
        else
        {
            party_List.SetActive(false);
            deploy_Area.SetActive(false);
            battleStart.SetActive(false);
            unit_Deloy_Area.SetActive(false);
        }
    }
}
