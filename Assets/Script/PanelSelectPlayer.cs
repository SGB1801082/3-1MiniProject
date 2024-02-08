using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelSelectPlayer : MonoBehaviour
{
    // 얘가 중앙통제실이라서 item_P 한테 받은 정보로 item_P를 통제하고 최종적으로 결과를 GMgr 한테 넘겨준다.
    //싱글톤
    public static PanelSelectPlayer single { get; private set; }
    //캐릭터 오브젝트
    [SerializeField] private List<Item_Player> listeItem;
    //시작하기 버튼 오브젝트
    [SerializeField] private Button btnStart;

    // 안내 메세지
    [SerializeField] private TextMeshProUGUI textAlert;
    private void Awake()
    {
        single = this;

        ONFocusePlayerItem(null);

        /*
        //캐릭터 아이템 전체 선택 비활성화
        foreach (var item in listeItem)
        {
            item.OnUnselect();
        }
        //시작하기 버튼 비활성화
        btnStart.gameObject.SetActive(false); //시작하면 꺼짐
        //안내 메세지 활성화
        textAlert.gameObject.SetActive(true);//시작하면 켜짐
        */
    }

    private Item_Player focusePlayerItem = null;
    public void ONFocusePlayerItem(Item_Player clicked)
    {
        this.focusePlayerItem = clicked;
        foreach(var item in listeItem)
        {
            if (item == clicked)
                item.Onselect();
            else 
                item.OnUnselect();
        }

        // tip - btn On && text off
        btnStart.gameObject.SetActive(this.focusePlayerItem != null);
        textAlert.gameObject.SetActive(this.focusePlayerItem == null);
    }

    public void OnStartGame()// 이거 실행하면 게임매니저한테 데이터보내야됨
    {
        // 버그 방지
        if (focusePlayerItem == null) return;
        //게임 매니저한테 데이터 보내는 거
        GameManager.single.OnSelectPlayer(focusePlayerItem.GetName());
    }
}
