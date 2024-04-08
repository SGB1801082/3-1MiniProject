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

    //04-01 Add
    [Header("GameUiMgr")]
    public GameUiMgr guiMgr;

    [Header("Tooltip")]
    public Tooltip tooltip;

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
        if (guiMgr.activeInventory == true)
        {
            if (item != null)
            {
                if (item.itemImage != null)
                {
                    tooltip.gameObject.SetActive(true);
                    tooltip.SetupTooltip(item.itemName, item.itemTitle, item.itemDesc, item.itemImage);
                }
                else
                    tooltip.gameObject.SetActive(false);
            }
        }
        else
        {
            tooltip.gameObject.SetActive(false);
        }
        

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.gameObject.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (item != null)
        {

            switch (item.itemType)
            {
                case Item.ItemType.Equipment_Arrmor:
                    break;
                case Item.ItemType.Equipment_Boots:
                    break;
                case Item.ItemType.Equipment_Helmet:
                    break;
                case Item.ItemType.Equipment_Weapon:
                    break;
                case Item.ItemType.Consumables:
                    bool isUse = item.Use();
                    if (isUse)
                    {
                        Inventory.single.RemoveItem(slotnum);
                    }
                    break;
                case Item.ItemType.Ect:
                    break;
                default:
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
