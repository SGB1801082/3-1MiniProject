using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler/*, IBeginDragHandler, IDragHandler, IEndDragHandler*/
{
    public int slotnum;
    public Item item;
    public Image itemIcon;
    public bool wearChek = false;
    public bool usability = false;

    //private bool isDraging;

    public void UpdateSloutUI()
    {
        itemIcon.sprite = item.itemImage;
        itemIcon.gameObject.SetActive(true);
    }
    public void RemoveSlot()
    {
        item = null;
        itemIcon.gameObject.SetActive(false);
    }
    public void CallGUISlot()
    {
        if (item.itemType != Item.ItemType.Consumables && item.itemType != Item.ItemType.Ect)
        {
            Debug.Log("Slot.cs - CallGUISlot(): "+item.itemType);// 정상작동함
/*
            GameUiMgr.single.nowSlot = this;
            GameUiMgr.single.dragIcon = GameUiMgr.single.nowSlot.itemIcon;*/
            //GameUiMgr.single.GetMoseItem(this);
            
        }
    }
    /*public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null && item.isDraggable == true)
        {
            GameUiMgr.single.OnBeginDrag(eventData);
            GameUiMgr.single.dragIndex = item.itemIndex;
            isDraging = true;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        GameUiMgr.single.OnDrag(eventData);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        GameUiMgr.single.OnEndDrag(eventData);
        isDraging = false;
    }*/

    //ToolTip & Item Use
    public void OnPointerEnter(PointerEventData eventData)//Move In ItemSlot
    {
        if (GameUiMgr.single.activeInventory == true /*&& isDraging == false*/)
        {
            if (item != null)
            {
                //GameUiMgr.single.dragIndex = item.itemIndex;
                if (item.itemImage != null)
                {
                    GameUiMgr.single.tooltip.SetActive(true);
                    GameUiMgr.single.SetupTooltip(item.itemName, item.itemTitle, item.itemDesc, item.itemImage);
                }
                else
                    GameUiMgr.single.tooltip.SetActive(false);
            }
        }
        else
        {
            GameUiMgr.single.tooltip.SetActive(false);
        }
        

    }
    public void OnPointerExit(PointerEventData eventData)//Move Out ItemSlot
    {
        GameUiMgr.single.tooltip.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)//Click ItemSlot
    {
        AudioManager.single.PlaySfxClipChange(0);
        Debug.Log("Run SFX sound index: 0");

        if (this.usability == true  && this.wearChek == false)//인벤토리 좌측의 장착아이템 목록이면서, 장비를 장착 중이 아닐때 상호작용불가능하게함.
        {
            return;
        }

        Debug.Log("Run Methode: Clicked ItemSlot");
        //int||float 아이템효과 담을 변수 선언
        if (this.wearChek == true)// 장착중인 장비와 상호작용
        {
            GameUiMgr.single.nowSlot = this;
            GameUiMgr.single.textEquipPanel.text = "장비를 해제 하시겠 습니까?";//OK버튼 클릭했을때 다른효과가 나와야하는데 생각조금 더 해봐야함
            GameUiMgr.single.addEquipPanel.gameObject.SetActive(true);

            Debug.Log("Run Methode: Take Off Item, Item Code: " + this.item.itemCode);
            

            //해당아이템의 효과수치 받아올 메서드(this.item); 를 실행
            /*switch (item.itemType)
            {
                //ex 모자 = 체력, 무기 = 공격력, 신발 = 공속, 갑옷 = 범위 인데 받아온 효과변수의 수치만큼 GameMgr.PlayerData[0]. 모자면 hp, 무기면 atk, 신발은 spd, 갑옷은 range에서 -- 하고, 해당 itme을 remove한 뒤 해당 targetSlot의 item을 비우고 itemType를 재 지정해 준 후 아이템을 인벤토리에 add
                *//*case Item.ItemType.Equipment_Arrmor:
                    break;
                case Item.ItemType.Equipment_Boots:
                    break;
                case Item.ItemType.Equipment_Helmet:
                    break;
                case Item.ItemType.Equipment_Weapon:
                    break;*//*
                default:
                    
                    break;
            }*/
            return;
        }


        if (item != null )//인벤토리의 아이템과 상호작용
        {
            switch (item.itemType)
            {
                case Item.ItemType.Ect:
                    break;
                case Item.ItemType.Consumables:
                    bool isUse = item.Use();
                    if (isUse)
                    {
                        if (item.itemName.Equals("새내기 포션") && wearChek == true)
                        {
                            GameUiMgr.single.questMgr.receptionist[0].SetActive(false);
                            GameUiMgr.single.questMgr.receptionist[1].SetActive(true);
                        }
                        Inventory.single.RemoveItem(slotnum);
                        GameUiMgr.single.tooltip.SetActive(false);
                    }
                    break;
                default:
                    GameUiMgr.single.nowSlot = this;
                    GameUiMgr.single.textEquipPanel.text = "장비를 장착 하시겠습니까?";
                    GameUiMgr.single.addEquipPanel.gameObject.SetActive(true);


                    //클릭하면 팝업창 출력되고. 팝업창에서 확인 클릭하면 실행되어야함

                    //isDraging = true;
                    GameUiMgr.single.tooltip.SetActive(false);
                    break;
            }

        }

        /*if (item.itemType == Item.ItemType.Equipment_Arrmor)
        {
            *//*bool isUse = item.Use();
            if (isUse)
            {
                Inventory.single.RemoveItem(slotnum);
            }*//*
            Debug.Log("This is Arrmor");
        }*/

    }

}
