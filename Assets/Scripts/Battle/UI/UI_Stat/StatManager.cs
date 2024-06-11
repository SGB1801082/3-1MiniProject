using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    public PlayerData player;

    [Header("Player_Stat")]
    public Slider hp;
    public Slider mp;

    [Header("Player_Text")]
    public TMP_Text level_Text;
    public TMP_Text name_Text;

    [Header("Image")]
    public Image player_Icon;
    public GameObject deploy_Check;
    public GameObject dead_Check;
    bool isDeploy;

    public void InitStat(PlayerData player, Sprite icon, int level, string name)
    {
        this.player = player;
        this.player_Icon.sprite = icon;
        this.level_Text.text = level.ToString();
        this.name_Text.text = name;
    }

    private void Start()
    {
        UnitStat();
    }
    
    private void UnitStat()
    {
        foreach (GameObject obj in BattleManager.Instance.party_List)
        {
            Ally players = obj.GetComponent<Ally>();
                
            if (players.entity_index == player.playerIndex)
            {
                UpdateStatus();
                break;
            }
        }
    }

    private void DeployUnitCheck()
    {
        isDeploy = false; // 초기화
        foreach (GameObject deploy in BattleManager.Instance.deploy_Player_List)
        {
            Ally deploy_Ally = deploy.GetComponent<Ally>();
            if (deploy_Ally.entity_index == player.playerIndex)
            {
                isDeploy = true;
                break;
            }
        }
    }

    private void Update()
    {
        UpdateStatus();
        if (BattleManager.Instance._curphase == BattleManager.BattlePhase.Deploy)
        {
            DeployUnitCheck();
        }

        if (BattleManager.Instance._curphase == BattleManager.BattlePhase.Rest)
            isDeploy = true;
    }

    public void UpdateStatus()
    {
        if (player != null)
        {
            // 실시간으로 HP, MP 업데이트
            hp.value = player.cur_Player_Hp / player.max_Player_Hp;
            mp.value = player.cur_Player_Mp / player.max_Player_Hp;

            if (player.cur_Player_Hp > 0)
            {
                dead_Check.SetActive(false);
            }
            else
            {
                dead_Check.SetActive(true);
            }

            // 배치 상태 업데이트
            deploy_Check.SetActive(!isDeploy);
        }
    }
}
