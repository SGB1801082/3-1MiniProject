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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GameUiMgr.single.activeInventory == true)
        {
            if (item != null)
            {
                Call_GUI_Slot();
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
            GameUiMgr.single.nowSlot = null;
        }
        

    }

    public void Call_GUI_Slot()
    {
        if (this.item.itemType != Item.ItemType.Consumables)
        {
            GameUiMgr.single.nowSlot = this;
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameUiMgr.single.tooltip.SetActive(false);
        GameUiMgr.single.nowSlot = null;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (item != null)
        {
            if (item.itemType == Item.ItemType.Consumables)
            {
                bool isUse = item.Use();
                if (isUse)
                {
                    Inventory.single.RemoveItem(slotnum);
                    GameUiMgr.single.tooltip.SetActive(false);
                    GameUiMgr.single.nowSlot = null;
                }
            }

            /*switch (item.itemType)
            {
                case Item.ItemType.Equipment_Arrmor:
                    GameUiMgr.single.tooltip.SetActive(false);
                    break;
                case Item.ItemType.Equipment_Boots:
                    GameUiMgr.single.tooltip.SetActive(false);
                    break;
                case Item.ItemType.Equipment_Helmet:
                    GameUiMgr.single.tooltip.SetActive(false);
                    break;
                case Item.ItemType.Equipment_Weapon:
                    GameUiMgr.single.tooltip.SetActive(false);
                    break;
                case Item.ItemType.Consumables:
                    bool isUse = item.Use();
                    if (isUse)
                    {
                        Inventory.single.RemoveItem(slotnum);
                        GameUiMgr.single.tooltip.SetActive(false);
                    }
                    break;
                case Item.ItemType.Ect:
                    break;
                default:
                    GameUiMgr.single.tooltip.SetActive(false);
                    break;
            }*/
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
