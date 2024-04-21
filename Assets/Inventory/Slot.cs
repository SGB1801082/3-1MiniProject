using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    public int slotnum;
    public Item item;
    public Image itemIcon;

    private bool slotInChek;

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
            //Debug.Log(item.itemType);// 정상작동함

            GameUiMgr.single.nowSlot = this;
            GameUiMgr.single.dragIcon = GameUiMgr.single.nowSlot.itemIcon;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)//Move In ItemSlot
    {
        if (GameUiMgr.single.activeInventory == true)
        {
            if (item != null)
            {
                slotInChek = true;
                CallGUISlot();
                if (item.itemImage != null)
                {
                    GameUiMgr.single.tooltip.SetActive(true);
                    GameUiMgr.single.SetupTooltip(item.itemName, item.itemTitle, item.itemDesc, item.itemImage);
                }
                else
                    GameUiMgr.single.tooltip.SetActive(false);
            }
            else
            {
                slotInChek = false;
            }
        }
        else
        {
            GameUiMgr.single.nowSlot = null;
            GameUiMgr.single.tooltip.SetActive(false);
        }
        

    }
    public void OnPointerExit(PointerEventData eventData)//Move Out ItemSlot
    {
        GameUiMgr.single.tooltip.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)//Click ItemSlot
    {
        if (item != null)
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
                    break;
                case Item.ItemType.Ect:
                    break;*/
                case Item.ItemType.Consumables:
                    if (slotInChek)
                    {
                        bool isUse = item.Use();
                        if (isUse)
                        {
                            Inventory.single.RemoveItem(slotnum);
                            GameUiMgr.single.tooltip.SetActive(false);
                        }
                    } 
                    break;
                default:
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
