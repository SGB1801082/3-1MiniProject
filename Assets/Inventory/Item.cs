using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public enum ItemType//04-01 Add
    {
        Equipment_Arrmor,
        Equipment_Boots,
        Equipment_Helmet,
        Equipment_Weapon,
        Consumables,
        Ect
    }
    public ItemType itemType;

    // ������ ���� & ��ȭ��ġ stack ���� �߰�
    public int itemStack = 0;
    public int modifyStack = 0;
    
    public string itemName;
    public Sprite itemImage;
    public List<ItemEffect> efts;
    //04-01 Add
    public string itemTitle;
    public string itemDesc;

    public int itemIndex;
    //04-22
    public bool isDraggable;
    //05-08
    public int itemCode;
    //06
    public float itemPower;
    //08
    public int itemPrice;// �����۰���
    public bool Use()
    {
        bool isUsed = false;
        
        foreach (ItemEffect eft in efts)
        {
            isUsed = eft.ExcuteRole();
        }

        return isUsed;
    }
    //���� ���������� ����Ҷ� ���� �޼��带 ����� �������ϴµ�..?
}

