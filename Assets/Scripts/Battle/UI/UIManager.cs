using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Battle(UI)")]
    public GameObject player_Statbar;
    public GameObject mini_Map;
    public GameObject mini_Map_Big;
    public GameObject item_Bar;
    public GameObject party_List;
    public GameObject battleStart;
    public GameObject in_Portal;
    public GameObject out_Portal;
    public GameObject banner;
    public GameObject next_Room_Popup;
    public GameObject item_Use_UI;


    [Header("Battle_Popup")]
    public GameObject popup_Bg;
    public GameObject reward_Popup;
    public GameObject reward_Prefab;
    public GameObject vic_Popup;
    public GameObject def_Popup;
    public GameObject alert_Popup;


    [Header("Tutorial")]
    public GameObject item_Tutorial;
    public GameObject ui_Tutorial_Rest;
    public GameObject ui_Tutorial_Deploy;
    public GameObject ui_Tutorial_Box;


    [Header("Dialogue")]
    public GameObject dialogue_Box;
    public GameObject dialogue_Bg;


    [Header("Reward")]
    public Sprite[] reward_Icons;

    private void Start()
    {
        player_Statbar.SetActive(true);
        item_Bar.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!mini_Map_Big.activeSelf)
            {
                BattleManager.Instance.room.map_Big_Camera.gameObject.SetActive(true);
                mini_Map_Big.SetActive(true);
            }
            else
            {
                BattleManager.Instance.room.map_Big_Camera.gameObject.SetActive(false);
                mini_Map_Big.SetActive(false);
            }
            
        }
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

    public void OpenPopup(GameObject popup)
    {
        popup_Bg.SetActive(true);
        popup.SetActive(true);
    }

    public void CancelPopup(GameObject popup)
    {
        if (BattleManager.Instance.dialogue.isTutorial)
        {
            if (popup.name == "Reward_Popup")
            {
                if (popup.GetComponent<RewardPopupInit>().isBox)
                {
                    BattleManager.Instance.tutorial.EndTutorial(17);
                }
                else
                {
                    BattleManager.Instance.dialogue.ONOFF(true);
                    BattleManager.Instance.dialogue.NextDialogue();
                }
            }
        }

        popup.SetActive(false);
        popup_Bg.SetActive(false);
    }


    /*public IEnumerator Banner()
    {
        if (BattleManager.Instance.deploy_Enemy_List.Count == 0 && BattleManager.Instance.room.rooms.Length - 1 != BattleManager.Instance.room.room_Count)
        {
            OpenPopup(banner);
            banner.GetComponent<TitleInit>().Init("전투 종료");

            CanvasGroup canvasGroup = banner.GetComponent<CanvasGroup>();

            yield return StartCoroutine(FadeIn(canvasGroup, 1f)); // 페이드인, 1초 동안
            yield return new WaitForSeconds(2f); // 2초 동안 유지
            yield return StartCoroutine(FadeOut(canvasGroup, 1f)); // 페이드아웃, 1초 동안
        }

        yield return null;
    }


    private IEnumerator FadeIn(CanvasGroup canvasGroup, float duration)
    {
        float startAlpha = 0f;
        float endAlpha = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }

    private IEnumerator FadeOut(CanvasGroup canvasGroup, float duration)
    {
        float startAlpha = 1f;
        float endAlpha = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
        canvasGroup.gameObject.SetActive(false);
    }
*/

}
