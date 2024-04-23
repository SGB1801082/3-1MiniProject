using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    [SerializeField] private BaseEntity player;
    [SerializeField] private Slider hp;
    [SerializeField] private Slider mp;
    [SerializeField] private Image player_Icon;
    [SerializeField] private GameObject entry_Check;
    [SerializeField] private GameObject dead_Check;

    private void OnEnable()
    {
        if (BattleManager.Instance._curphase == BattleManager.BattlePhase.Battle)
        {
            switch (name)
            {
                case "P1":
                    player = BattleManager.Instance.deloy_Player_List[0].GetComponent<BaseEntity>();
                    break;
                case "P2":
                    if (BattleManager.Instance.deloy_Player_List.Count >= 2)
                        player = BattleManager.Instance.deloy_Player_List[1].GetComponent<BaseEntity>();
                    else
                        return;
                    break;
                case "P3":
                    if (BattleManager.Instance.deloy_Player_List.Count >= 3)
                        player = BattleManager.Instance.deloy_Player_List[2].GetComponent<BaseEntity>();
                    else
                        return;
                    break;
                case "P4":
                    if (BattleManager.Instance.deloy_Player_List.Count >= 4)
                        player = BattleManager.Instance.deloy_Player_List[3].GetComponent<BaseEntity>();
                    else
                        return;
                    break;
            }
        }
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
