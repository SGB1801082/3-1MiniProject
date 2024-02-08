using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

//상속 클래스는 1개밖에안되지만 추상클래스는 몇개든 붙일 수 있다.
public class Item_Player : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private CanvasGroup _group;
    [SerializeField] private string playerName;
    [SerializeField] private TextMeshProUGUI textName;

    private void Awake()
    {
        textName.text = playerName;
        OnUnselect();
    }
    //화면이 켜지는 순간 클릭이 안 된 상태로 초기화 해주어야한다.
    public void OnUnselect() //아이콘을 그레이처리하려면 여기다넣으면되겠지 
    {
        _group.alpha = 0.1f;
    }

    public string GetName() => playerName;

    public void Onselect() //아이콘을 그레이처리하려면 여기다넣으면되겠지 
    {
        _group.alpha = 1f;
    }

    public void OnPointerClick(PointerEventData eventData) //추상클래스
    {
        //Onselect(); //클릭
        PanelSelectPlayer.single.ONFocusePlayerItem(this);
    }
}
