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
    public int itemPrice;

    public TextMeshProUGUI textItemPrice;
    public TextMeshProUGUI textItemStack;
    public TextMeshProUGUI textItemPower;
    public Image imgSlodOut;
    
    public void Init(Item _item, ShopState _state)
    {
        item = _item;
        state = _state;

        //�������� �����ɶ� �ٷ� ǥ�� �ʱ�ȭ
        itemPrice = item.itemPrice;
        textItemPrice.text = itemPrice.ToString();
        
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
            }
            else
            {
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
            if (_item.itemPower > 0)
            {
                textItemPower.text = _item.itemPower.ToString();
            }
        }
    }
    void DrawSellShop()//���� '�Ǹ�'������ ǥ�� �ʱ�ȭ
    {

    }
}
