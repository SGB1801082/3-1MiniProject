using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject player_Statbar;
    public GameObject item_Bar;
    public GameObject party_list;
    public GameObject deploy_area;
    public GameObject battleStart;
    public GameObject unit_deloy_area;


    private void FixedUpdate()
    {
        if (player_Statbar != null && item_Bar != null && party_list != null)
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
                party_list.SetActive(true);
                deploy_area.SetActive(true);
                battleStart.SetActive(true);
                unit_deloy_area.SetActive(true);
            }

            else
            {
                party_list.SetActive(false);
                deploy_area.SetActive(false);
                battleStart.SetActive(false);
                unit_deloy_area.SetActive(false);
            }
        }
    }
}
