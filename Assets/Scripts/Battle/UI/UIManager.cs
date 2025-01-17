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
    public GameObject next_Room_Popup;
    public GameObject item_Use_UI;
    public GameObject fade_Bg;

    [Header("Banner")]
    public GameObject def_Banner;
    public GameObject vic_Banner;
    public GameObject battle_Start_Banner;
    public GameObject battle_Ready_Banner;


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


    public IEnumerator Def_Banner()
    {
        CanvasGroup canvas = def_Banner.GetComponent<CanvasGroup>();

        yield return StartCoroutine(FadeTo(canvas, 0f, 1.0f, 1f));
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(FadeTo(canvas, 1f, 0f, 1f));
        CancelPopup(def_Banner);
    }


    private IEnumerator FadeTo(CanvasGroup group, float start, float targetAlpha, float duration)
    {
        float startAlpha = start;
        float timer = 0f;

        while (timer < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / duration);
            group.alpha = alpha;
            timer += Time.deltaTime;
            yield return null;
        }

        group.alpha = targetAlpha;
    }


    public IEnumerator StartBanner(GameObject banner)
    {
        Animator ani = banner.GetComponent<Animator>();
        AnimatorStateInfo aniInfo = ani.GetCurrentAnimatorStateInfo(0);

        yield return new WaitForSeconds(aniInfo.length);
        CancelPopup(banner);
    }
}
