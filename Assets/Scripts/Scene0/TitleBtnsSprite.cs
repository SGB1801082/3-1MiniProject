using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleBtnsSprite : MonoBehaviour, IPointerEnterHandler, IPointerUpHandler, IPointerExitHandler
{
    public MainMenuMgr mainMenuMgr;

    [SerializeField] private Image img;
    [SerializeField] private Button my;
    private bool isPointerOver = false; // 마우스가 버튼 위에 있는지 여부를 저장하는 변수
    public List<AudioClip> audioSources = new();
    private void Update()
    {
        // 마우스가 버튼 위에 있지 않고, 마우스가 눌린 상태가 아닌 경우
        /*if (!isPointerOver && Input.GetMouseButton(0))
        {
            OnPointerExit(null); // OnPointerExit 호출
        }*/

        if (Input.GetMouseButton(0))
        {
            if (isPointerOver == true)
            {
                img.sprite = mainMenuMgr.TitleBtnSprites[2];// 클릭
            }
            else
            {
                img.sprite = mainMenuMgr.TitleBtnSprites[0];//기본
            }
        }
        else
        {
            Debug.Log("Return Update");
            return;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOver = true;
        img.sprite = mainMenuMgr.TitleBtnSprites[1];//호버
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Up");
        img.sprite = mainMenuMgr.TitleBtnSprites[0];
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit");
        isPointerOver = false;
        img.sprite = mainMenuMgr.TitleBtnSprites[0];
    }
}
