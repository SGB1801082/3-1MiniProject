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
    public bool tutorialChek = false;

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
        if (item != null )
        {
            switch (item.itemType)
            {
                /*case Item.ItemType.Equipment_Arrmor:
                    break;
                case Item.ItemType.Equipment_Boots:
                    break;
                case Item.ItemType.Equipment_Helmet:
                    break;
                case Item.ItemType.Equipment_Weapon:
                    break;*/
                case Item.ItemType.Ect:
                    break;
                case Item.ItemType.Consumables:
                    bool isUse = item.Use();
                    if (isUse)
                    {
                        if (item.itemName.Equals("새내기 포션") &&tutorialChek == true)
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
