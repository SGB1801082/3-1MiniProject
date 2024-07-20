using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void OnSlotCountChange(int val);//delegate Definition
    public OnSlotCountChange onSlotCountChange;// Definition make is Instance

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    public List<Item> items = new List<Item>();

    private int slotCnt;
    public int SlotCnt
    {
        get => slotCnt;
        set { 
        slotCnt = value;
            onSlotCountChange.Invoke(slotCnt);
        }
    }

    #region Singletone
    public static Inventory single;
    private void Awake()
    {
        if (single != null)
        {
            Destroy(single);
            return;
        }
        single = this;
    }
    #endregion

    private void Start()
    {
        slotCnt = 35;
    }

    //method
    public bool AddItem(Item _item)
    {
        if (items.Count < slotCnt)
        {
            _item.itemIndex = items.Count;
            items.Add(_item);
            Debug.Log(_item.itemIndex);

            if (onChangeItem != null)
                onChangeItem.Invoke();
            
            return true;
        }
        return false;
    }

    public void RemoveItem(int _index)
    {
        if (_index >= 0 && _index < items.Count)
        {
            items.RemoveAt(_index);

            // �������� ���ŵ� �� ���� �����۵��� itemIndex ����
            for (int i = 0; i < items.Count; i++)
            {
                items[i].itemIndex = i;
            }

            onChangeItem?.Invoke();
        }
        else
        {
            Debug.Log("Invalid index to remove item!");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FieldItem"))
        {
            FieldItems fieldItem = collision.GetComponent<FieldItems>();
            if (AddItem(fieldItem.GetItem()))
            {
                fieldItem.DestroyItem();
            }
            else
            {
                Debug.Log("Inventory Is Full");
            }
        }
    }
}
