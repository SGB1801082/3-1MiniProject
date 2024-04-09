using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelSelectPlayer : MonoBehaviour
{
    public static PanelSelectPlayer single { get; private set; }

    // 캐릭터 오브젝트
    [SerializeField] private List<Item_Player> listItems;

    // 시작하기 버튼 오브젝트
    [SerializeField] private Button btnStart;

    private void Awake()
    {
        Reset();
    }
    private void Update()
    {
        ChekBtnNull();
    }

    public void Reset()
    {
        OnFocusePlayerItem(null);
        btnStart.interactable = false;
    }

    private Item_Player focusedPlayerItem = null;
    public void OnFocusePlayerItem(Item_Player clicked)
    {
        this.focusedPlayerItem = clicked;

        foreach (var item in listItems)
        {
            if (item == clicked)
                item.SelectPanelAlpha(1f);// item.OnSelect();
            else
                item.SelectPanelAlpha(0.1f);// item.OnUnselect();
        }

    }

    public void OnStartGame()
    {
        // 버그 방지
        if (this.focusedPlayerItem == null)
            return;

        if (string.IsNullOrEmpty(this.focusedPlayerItem.GetName()))
        {
            btnStart.interactable = false;
            return;
        }
        //GameMgr.single.OnSelectPlayer(focusedPlayerItem.GetName(), focusedPlayerItem.GetJob());

    }

    private void ChekBtnNull()
    {
        if (string.IsNullOrEmpty(focusedPlayerItem.GetName()))
        {
            btnStart.interactable = false;
        } 
        else
        {
            btnStart.interactable = true;
        }
    }

}
