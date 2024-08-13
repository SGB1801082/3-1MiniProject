using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [Header("Display")]
    public TextMeshProUGUI itemStack;// �Ҹ�ǰ ���� ǥ��
    public TextMeshProUGUI modifyStack;// ��ȭ ��ġ ǥ��
    //private bool isDraging;

    public void UpdateSloutUI()
    {
        itemIcon.sprite = item.itemImage;
        if (item.itemType == Item.ItemType.Consumables || item.itemType == Item.ItemType.Ect)
        {
            itemStack.text = item.itemStack.ToString();
            itemStack.gameObject.SetActive(true);
        }
        else if (item.itemType == Item.ItemType.Equipment_Helmet || item.itemType == Item.ItemType.Equipment_Weapon || item.itemType == Item.ItemType.Equipment_Boots || item.itemType == Item.ItemType.Equipment_Arrmor)
        {
            if (item.modifyStack > 0)
            {
                modifyStack.text = item.modifyStack.ToString();
                modifyStack.gameObject.SetActive(true);
            }
        }

        itemIcon.gameObject.SetActive(true);
    }
    public void RemoveSlot()
    {
        item = null;
        itemIcon.gameObject.SetActive(false);

        itemStack.gameObject.SetActive(false);
        modifyStack.gameObject.SetActive(false);
    }
    public void CallGUISlot()
    {
        if (item.itemType != Item.ItemType.Consumables && item.itemType != Item.ItemType.Ect)
        {
            Debug.Log("Slot.cs - CallGUISlot(): "+item.itemType);// �����۵���
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

        if (this.usability == true  && this.wearChek == false)//�κ��丮 ������ ���������� ����̸鼭, ��� ���� ���� �ƴҶ� ��ȣ�ۿ�Ұ����ϰ���. = ���ĭ�� �������� �ǵ������ȵȴٰ�
        {
            return;
        }

        Debug.Log("Run Methode: Clicked ItemSlot");
        //int||float ������ȿ�� ���� ���� ����
        if (this.wearChek == true)// �������� ���� ��ȣ�ۿ�
        {
            if (!(item.itemType == Item.ItemType.Ect) && !(item.itemType == Item.ItemType.Consumables))
            {
                GameUiMgr.single.nowSlot = this;
                GameUiMgr.single.textEquipPanel.text = "��� ���� �Ͻðڽ��ϱ�?";//OK��ư Ŭ�������� �ٸ�ȿ���� ���;��ϴµ� �������� �� �غ�����
                GameUiMgr.single.addEquipPanel.gameObject.SetActive(true);

                Debug.Log("Run Methode: Take Off Item, Item Code: " + this.item.itemCode); 
            }
            else
            {
                return;
            }
            return;
        }


        if (item != null )//�κ��丮�� �����۰� ��ȣ�ۿ�
        {
            switch (item.itemType)
            {
                case Item.ItemType.Ect:
                    break;
                case Item.ItemType.Consumables:
                    bool isUse = item.Use();
                    if (isUse)
                    {
                        if (item.itemName.Equals("������ ����") && wearChek == true)
                        {
                            GameUiMgr.single.questMgr.receptionist[0].SetActive(false);
                            GameUiMgr.single.questMgr.receptionist[1].SetActive(true);
                        }
                        Inventory.Single.RemoveItem(item);
                        UpdateStack();
                        GameUiMgr.single.tooltip.SetActive(false);
                    }
                    break;
                default:
                    GameUiMgr.single.nowSlot = this;

                    GameUiMgr.single.textEquipPanel.text = "��� ���� �Ͻðڽ��ϱ�?";
                    Debug.Log("gggggggggg" + item.itemType);
                    GameUiMgr.single.addEquipPanel.gameObject.SetActive(true);
                    //Ŭ���ϸ� �˾�â ��µǰ�. �˾�â���� Ȯ�� Ŭ���ϸ� ����Ǿ����
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

    public void UpdateStack()
    {
        if (item.itemStack == 0)
        {
            GameUiMgr.single.RedrawSlotUI();
        }
        if (itemStack.gameObject.activeSelf)
        {
            itemStack.text = item.itemStack.ToString();
        }
        if (modifyStack.gameObject.activeSelf)
        {
            modifyStack.text = item.modifyStack.ToString();
        }
    }
}
