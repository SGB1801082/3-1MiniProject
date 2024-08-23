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

        //아이템이 생성될때 바로 표시 초기화
        itemPrice = item.itemPrice;
        textItemPrice.text = itemPrice.ToString();
        
        DrawBuyShop(item);
    }

    public void DrawBuyShop(Item _item)//상점 '구매'아이템 표시 초기화
    {
        if (_item.itemType == Item.ItemType.Consumables || _item.itemType == Item.ItemType.Ect)
        {
            // 소모품일 경우 표시
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
            // 장비아이템일 경우 표시
            if (_item.itemPower > 0)
            {
                textItemPower.text = _item.itemPower.ToString();
            }
        }
    }
    void DrawSellShop()//상점 '판매'아이템 표시 초기화
    {

    }
}
