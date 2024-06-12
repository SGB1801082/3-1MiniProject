using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    Ally[] player;
    public TMP_Text item_Cnt_Text;
    [SerializeField] int item_Cnt;

    void Start()
    {
        item_Cnt = 5;
        item_Cnt_Text.text = item_Cnt.ToString();
    }

    public void Postion()
    {
        /*List<GameObject> obj = BattleManager.Instance.party_List;

        player = new Ally[obj.Length];

        for (int i = 0; i < obj.Length; i++)
        {
            player[i] = obj[i].GetComponent<Ally>();
        }*/

        /*if (item_Cnt != 0)
        {
            foreach (Ally player_Obj in player)
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

            if (BattleManager.Instance.dialogue.isQuest)
            {
                BattleManager.Instance.EndTutorial(BattleManager.Instance.dialogue.cnt);
            }

            Debug.Log("아군 전체 각 최대 체력 3 만큼 회복");
        }
        else
        {
            Debug.Log("포션이 부족합니다");
            return;
        }*/

        Debug.Log("수정 예정");
        GameMgr.playerData[0].cur_Player_Hp += 0;

        if (BattleManager.Instance.dialogue.isQuest)
        {
            BattleManager.Instance.EndTutorial(BattleManager.Instance.dialogue.cnt);
        }
    }
}
