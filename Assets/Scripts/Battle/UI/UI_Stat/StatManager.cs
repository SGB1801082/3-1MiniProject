using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    public BaseEntity player;

    [Header("Player_Stat")]
    public Slider hp;
    public Slider mp;

    [Header("Player_Text")]
    public TMP_Text level_Text;
    public TMP_Text name_Text;

    [Header("Image")]
    public Image player_Icon;
    public GameObject entry_Check;
    public GameObject dead_Check;
    bool isDeploy;

    public void InitStat(BaseEntity player, Sprite icon)
    {
        this.player = player;
        this.player_Icon.sprite = icon;
        isDeploy = true;
    }

/*    private void Start()
    {
        UnitStat();
    }*/
/*
    private void UnitStat()
    {
        foreach (GameObject obj in BattleManager.Instance.party_List)
        {
            BaseEntity entity = obj.GetComponent<BaseEntity>();
            player = entity;
            Debug.Log("정보 넣기 " + obj);
            break;
        }
    }*/

    private void DeployUnitCheck()
    {
        foreach (GameObject deploy in BattleManager.Instance.deploy_Player_List)
        {
            foreach (GameObject obj in BattleManager.Instance.party_List)
            {
                if (deploy.gameObject != obj.gameObject)
                {
                    isDeploy = false;
                }
                else
                {
                    isDeploy = true;
                }
            }
        }
    }


    private void Update()
    {
        UpdateStatus();
        if (BattleManager.Instance._curphase == BattleManager.BattlePhase.Battle)
        {
            DeployUnitCheck();
        }
    }

    public void UpdateStatus()
    {
        if (player != null && player.cur_Hp >= 0) 
        {
            hp.value = player.cur_Hp / player.max_Hp;
            mp.value = player.cur_Mp / player.max_Mp;
            dead_Check.SetActive(false);
        }

        if (isDeploy && player != null && player.cur_Hp >= 0)
        {
            entry_Check.SetActive(false);
        }
        else
        {
            entry_Check.SetActive(true);
        }


        if (player != null && player.cur_Hp <= 0 && player._curstate == BaseEntity.State.Death)
        {
            entry_Check.SetActive(false);
            dead_Check.SetActive(true);
        }
    }

}
