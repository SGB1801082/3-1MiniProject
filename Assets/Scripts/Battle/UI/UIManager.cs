using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Battle(UI)")]
    public GameObject player_Statbar;
    public GameObject mini_Map;
    public GameObject item_Bar;
    public GameObject party_List;
    public GameObject battleStart;
    public GameObject in_Portal;
    public GameObject out_Portal;


    [Header("BattleEnd_Popup")]
    public GameObject popup_Bg;
    public GameObject vic_Popup;
    public GameObject def_Popup;


    [Header("Tutorial")]
    public GameObject item_Tutorial;
    public GameObject ui_Tutorial_Rest;
    public GameObject ui_Tutorial_Deploy;


    [Header("Dialogue")]
    public GameObject dialogue_Box;
    public GameObject dialogue_Bg;

    private void Start()
    {
        player_Statbar.SetActive(true);
        item_Bar.SetActive(true);
    }


    private void FixedUpdate()
    {
        if (BattleManager.Instance._curphase == BattleManager.BattlePhase.Rest || BattleManager.Instance._curphase == BattleManager.BattlePhase.End)
        {
            party_List.SetActive(false);
            battleStart.SetActive(false);
        }

        if (BattleManager.Instance._curphase == BattleManager.BattlePhase.Battle)
        {
            item_Bar.SetActive(false);
        }

        if (BattleManager.Instance._curphase == BattleManager.BattlePhase.Deploy)
        {
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
