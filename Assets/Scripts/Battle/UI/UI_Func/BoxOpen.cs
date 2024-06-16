using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxOpen : MonoBehaviour
{
    public Animator ani;
    public GameObject mimic;
    bool isSetTutorial;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        if (!isSetTutorial)
        {
            ani.SetTrigger("isOpen");
        }
    }


    public void OnAniDone()
    {
        if (gameObject.CompareTag("Mimic"))
        {
            if (BattleManager.Instance.dialogue.isTutorial)
            {
                BattleManager.Instance.tutorial.EndTutorial(18);
            }

            GameObject mimics = Instantiate(mimic, gameObject.transform.parent);
            mimics.transform.position = gameObject.transform.position;
            gameObject.SetActive(false);

         
            BattleManager.Instance.room.currentRoom.gameObject.tag = "Battle";
            BattleManager.Instance.CheckRoom();

            if (BattleManager.Instance.ui.out_Portal.activeSelf)
            {
                BattleManager.Instance.ui.out_Portal.GetComponent<FadeEffect>().fadein = true;
            }

        }
        else
        {
            BattleManager.Instance.ui.OpenPopup(BattleManager.Instance.ui.reward_Popup);
            RewardPopupInit reward = BattleManager.Instance.ui.reward_Popup.GetComponent<RewardPopupInit>();
            reward.Init("»óÀÚ", true);
            if (BattleManager.Instance.dialogue.isTutorial)
                BattleManager.Instance.ui.ui_Tutorial_Box.SetActive(false);

            int gold = Random.Range(10, 101);

            GameObject gold_Obj = Instantiate(BattleManager.Instance.ui.reward_Prefab, reward.inner_Main);
            gold_Obj.GetComponent<RewardInit>().Init(BattleManager.Instance.ui.reward_Icons[0], gold + " Gold");

            BattleManager.Instance.total_Gold += gold;
            GameMgr.playerData[0].player_Gold += gold;

            gameObject.SetActive(false);
            return;
        }
    }


    public void SetTutorial(bool setTutorial)
    {
        isSetTutorial = setTutorial;
    }
 
}
