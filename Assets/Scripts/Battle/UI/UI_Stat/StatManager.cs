using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    public BaseEntity player;
    public Slider hp;
    public Slider mp;
    public Image player_Icon;
    public GameObject entry_Check;
    public GameObject dead_Check;

    public void InitStat(BaseEntity player, Sprite icon)
    {
        this.player = player;
        this.player_Icon.sprite = icon;
    }

    private void Update()
    {
        if (BattleManager.Instance._curphase == BattleManager.BattlePhase.Battle)
        {
            UpdateStatus();
        }
    }

    private void UpdateStatus()
    {
        if (player != null && player.cur_Hp >= 0) 
        {
            entry_Check.SetActive(false);
            hp.value = player.cur_Hp / player.max_Hp;
            mp.value = player.cur_Mp / player.max_Mp;
            dead_Check.SetActive(false);
        }

        if (player != null && player.cur_Hp <= 0 && player._curstate == BaseEntity.State.Death)
        {
            dead_Check.SetActive(true);
        }
    }

}
