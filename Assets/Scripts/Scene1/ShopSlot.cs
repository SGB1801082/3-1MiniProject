using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ShopState
{
    BUY,
    SELL,
    SOLDOUT
}
public class ShopSlot : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private ShopState state;
    public int slotPirce = 0;
    public int shopIndex = 0;

    public TextMeshProUGUI textItemPrice;
    public TextMeshProUGUI textItemStack;
    public TextMeshProUGUI textItemModifyStack;
    public Image imgSlodOut;
    
    public Image slotIcon;
    
    public void Init(Item _item, ShopState _state)
    {
        item = _item;
        state = _state;

        //Debug.Log($"Item Price: {item.itemPrice}");
        Debug.Log("���ݻ����� �������� ����: " + _item.itemStack);

        slotPirce = item.itemPrice;
        if (state == ShopState.SELL)
        {
            slotPirce = (int)(slotPirce * 0.7);
        }
        slotIcon.sprite = item.itemImage;
        textItemPrice.text = slotPirce.ToString();
        DrawBuyShop(item);
    }

    public void DrawBuyShop(Item _item)//���� '����'������ ǥ�� �ʱ�ȭ
    {
        if (_item.itemType == Item.ItemType.Consumables || _item.itemType == Item.ItemType.Ect)
        {
            // �Ҹ�ǰ�� ��� ǥ��
            if (_item.itemStack > 0)
            {
                textItemStack.text = _item.itemStack.ToString();
                textItemStack.gameObject.SetActive(true);
            }
            else
            {
                textItemStack.gameObject.SetActive(false);
                if (state != ShopState.SOLDOUT)
                {
                    state = ShopState.SOLDOUT;
                    imgSlodOut.gameObject.SetActive(true);
                }
                return;
            }
        }
        else
        {
            // ���������� ��� ǥ��
            if (_item.modifyStack > 0)
            {
                textItemModifyStack.text = "+"+_item.modifyStack.ToString();
                textItemModifyStack.gameObject.SetActive(true);
            }
            else
                textItemModifyStack.gameObject.SetActive(false);
        }
    }
    void DrawSellShop()//���� '�Ǹ�'������ ǥ�� �ʱ�ȭ
    {

    }
}
