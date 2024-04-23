using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatManager : MonoBehaviour
{
    public BaseEntity enemy;
    public Slider hp;
    //public Slider mp;

    private void Start()
    {
        enemy = GetComponent<BaseEntity>();
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
        if (enemy != null && enemy.cur_Hp >= 0)
        {
            hp.value = enemy.cur_Hp / enemy.max_Hp;
            //mp.value = player.cur_Mp / player.max_Mp;
        }

        if (enemy.cur_Hp <= 0 && enemy._curstate == BaseEntity.State.Death)
        {
            hp.gameObject.SetActive(false);
        }
    }


}
