using DarkPixelRPGUI.Scripts.UI.Equipment;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ShopMgr : MonoBehaviour
{
    [SerializeField] List<ShopSlot> shopSlots;
    [SerializeField] List<ShopSlot> playerShopItems;

    [SerializeField] ShopSlot slotPrefab;
    public ShopState shopState;

    [Header("Tab")]
    [SerializeField] Button buyTab;
    [SerializeField] Button sellTab;

    [Header("Pannel")]
    public GameObject buyPannel;
    public GameObject sellPannel;

    public Transform tfBuy;
    public Transform tfSell;

    //[SerializeField] Button btnBuy;// TODO: Add SoundEv 

    private void Start()
    {
        shopState = ShopState.BUY;
        OpenTap(shopState);

        //�� �Ʒ��� for�� ���������� GameUIMgr�� �������ҰŰ��� ���⼭�����ϱ⿣����? ����� ����̸�����
        for (int i = 0; i < 18; i++)
        {
            // �������� ����
            Item newItem = ItemResources.instance.itemRS[Random.Range(0, 6)];

            // �������� �ν��Ͻ�ȭ�Ͽ� ShopSlot�� ����
            ShopSlot slot = Instantiate(slotPrefab, tfBuy);

            // ������ ���� �ʱ�ȭ
            slot.Init(newItem, ShopState.BUY);

            // ������ ������ ����Ʈ�� �߰�
            shopSlots.Add(slot);
        }
    }

    void SellItems()
    {
        // ������ playerShopItems ������ ��� ��Ȱ��ȭ
        if (playerShopItems.Count > 0)
        {
            foreach (ShopSlot _slot in playerShopItems)
            {
                _slot.gameObject.SetActive(false);
            }
        }

        // �κ��丮�� ��� �������� ������ ���Կ� �߰�
        for (int i = 0; i < Inventory.Single.items.Count; i++)
        {
            Item _item = ItemResources.instance.itemRS[Inventory.Single.items[i].itemCode];
            _item.itemStack = Inventory.Single.items[i].itemStack;

            // ��Ȱ��ȭ�� ������ �����ϰų�, ���ο� ������ ����
            ShopSlot sellSlot = PoolShopSlot(playerShopItems);
            if (sellSlot == null)
            {
                sellSlot = Instantiate(slotPrefab, tfSell);
            }
            // ���� �ʱ�ȭ
            sellSlot.Init(_item, ShopState.SELL);

            playerShopItems.Add(sellSlot);
        }
    }

    ShopSlot PoolShopSlot(List<ShopSlot> _s)
    {
        foreach (ShopSlot slot in _s)
        {
            if (!slot.gameObject.activeSelf)
            {
                slot.gameObject.SetActive(true);
                return slot;
            }
        }
        return null;
    }

    void OpenTap(ShopState _state)// isOpen == true '�Ǹ�'�� Ȱ��, isOpen == false '����'�� Ȱ��
    {
        if (_state == ShopState.BUY)
        {
            buyTab.interactable = false;
            sellTab.interactable = true;

            //TODO: Buy ImageObejct Active, Sell ImageObject unActive
            buyPannel.SetActive(true);
            sellPannel.SetActive(false);
        }
        else if (_state == ShopState.SELL)
        {
            buyTab.interactable = true;
            sellTab.interactable = false;

            buyPannel.SetActive(false);
            sellPannel.SetActive(true);

            SellItems();
        }
    }

    public List<ShopSlot> GetShopSlots()
    {
        return shopSlots;
    }
    public void SetShopSlots(ShopSlot _shopSlot)
    {
        shopSlots.Add(_shopSlot);
    }

    // Tab Open
    public void ClickBuyTab() { OpenTap(ShopState.BUY); }
    public void ClickSellTab() { OpenTap(ShopState.SELL); }
}
