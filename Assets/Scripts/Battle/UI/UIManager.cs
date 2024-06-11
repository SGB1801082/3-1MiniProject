using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Battle(UI)")]
    public GameObject player_Statbar;
    public GameObject item_Bar;
    public GameObject party_List;
    public GameObject battleStart;
    public GameObject nextRoom;


    [Header("BattleEnd_Popup")]
    public GameObject popup_Bg;
    public GameObject vic_Popup;
    public GameObject def_Popup;


    [Header("Tutorial")]
    public GameObject item_Tutorial;

    private void Start()
    {
        player_Statbar.SetActive(true);
        item_Bar.SetActive(true);
    }


    private void FixedUpdate()
    {
        if (BattleManager.Instance._curphase == BattleManager.BattlePhase.Rest || BattleManager.Instance._curphase == BattleManager.BattlePhase.End)
        {
            nextRoom.SetActive(true);
            party_List.SetActive(false);
            battleStart.SetActive(false);
        }
        else
        {
            nextRoom.SetActive(false); 
        }

        if (BattleManager.Instance._curphase == BattleManager.BattlePhase.Battle)
        {
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

        if (BattleManager.Instance._curphase == BattleManager.BattlePhase.Rest || BattleManager.Instance._curphase == BattleManager.BattlePhase.Deploy)
        {
            item_Bar.SetActive(true);
        }
    }
}
