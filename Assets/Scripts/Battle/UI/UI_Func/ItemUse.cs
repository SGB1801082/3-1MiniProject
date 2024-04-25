using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    Player[] player;
    public TMP_Text item_Cnt_Text;
    int item_Cnt;

    void OnEnable()
    {
        item_Cnt = 5;
        item_Cnt_Text.text = item_Cnt.ToString();

        GameObject[] obj = GameObject.FindGameObjectsWithTag("Player");
        player = new Player[obj.Length];
        
        for (int i = 0; i < obj.Length; i++) 
        {
            player[i] = obj[i].GetComponent<Player>();
        }
    }

    private void Update()
    {
        if (BattleManager.Instance._curphase == BattleManager.BattlePhase.Battle)
        {
            item_Cnt_Text.text = item_Cnt.ToString();
        }
        
    }

    public void Postion()
    {
        if (item_Cnt != 0)
        {
            foreach (Player player_Obj in player)
            {
                player_Obj.cur_Hp += 3f;
            }
            item_Cnt--;
            Debug.Log("아군 전체 각 최대 체력 5 만큼 회복");
        }
        else
        {
            Debug.Log("포션이 부족합니다");
            return;
        }
        
    }
}
