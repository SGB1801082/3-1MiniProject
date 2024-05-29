using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    BaseEntity[] player;
    public TMP_Text item_Cnt_Text;
    int item_Cnt;

    void Start()
    {
        item_Cnt = 5;
        item_Cnt_Text.text = item_Cnt.ToString();

        GameObject[] obj = GameObject.FindGameObjectsWithTag("Player");
        player = new BaseEntity[obj.Length];
        
        for (int i = 0; i < obj.Length; i++) 
        {
            player[i] = obj[i].GetComponent<BaseEntity>();
        }
    }

    public void Postion()
    {
        if (item_Cnt != 0)
        {
            foreach (BaseEntity player_Obj in player)
            {
                if (player_Obj.max_Hp > (player_Obj.cur_Hp + 3f))
                {
                    player_Obj.cur_Hp += 3f;
                }
                else
                {
                    player_Obj.cur_Hp = player_Obj.max_Hp;
                }
                
            }
            item_Cnt--;
            item_Cnt_Text.text = item_Cnt.ToString();
            Debug.Log("아군 전체 각 최대 체력 3 만큼 회복");
        }
        else
        {
            Debug.Log("포션이 부족합니다");
            return;
        }
        
    }
}
