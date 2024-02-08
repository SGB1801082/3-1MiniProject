using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenMgr : MonoBehaviour
{
    public List<Item> poolItem;
    public string[] arrArmorName;
    public string[] arrBootsName;
    public string[] arrGlovesName;
    public string[] arrHelmetName;
    private List<Equipment> ListEquips = new List<Equipment>();
    
    private void Awake()
    {
        //Reset Item pool
        foreach (var item in poolItem)
        {
            item.gameObject.SetActive(false);
        }
        
    }
    private void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            ListEquips.Add(new Armor(arrArmorName[Random.Range(0, arrArmorName.Length)]));
        }
        for (int i = 0; i < 2; i++)
        {
            ListEquips.Add(new Boots(arrBootsName[Random.Range(0, arrBootsName.Length)]));
        }
        for (int i = 0; i < 2; i++)
        {
            ListEquips.Add(new Gloves(arrGlovesName[Random.Range(0, arrGlovesName.Length)]));
        }
        for (int i = 0; i < 2; i++)
        {
            ListEquips.Add(new Helmet(arrHelmetName[Random.Range(0, arrHelmetName.Length)]));
        }
        for (int i = 0; i < ListEquips.Count; i++)
        {
            CreateItem().Init(ListEquips[i]);
        }
    }

    public void ResetBtn()
    {
        ListEquips.Clear();
        for (int i = 0; i < poolItem.Count; i++)
        {
            poolItem[i].gameObject.SetActive(false);
        }

        int loop = Random.Range(1, 6);
        for (int i = 0; i < loop; i++)
        {
            ListEquips.Add(new Armor(arrArmorName[Random.Range(0, arrArmorName.Length)]));
        }
        for (int i = 0; i < 2; i++)
        {
            ListEquips.Add(new Boots(arrBootsName[Random.Range(0, arrBootsName.Length)]));
        }
        for (int i = 0; i < 2; i++)
        {
            ListEquips.Add(new Gloves(arrGlovesName[Random.Range(0, arrGlovesName.Length)]));
        }
        for (int i = 0; i < 2; i++)
        {
            ListEquips.Add(new Helmet(arrHelmetName[Random.Range(0, arrHelmetName.Length)]));
        }
        for (int i = 0; i < ListEquips.Count; i++)
        {
            CreateItem().Init(ListEquips[i]);
        }
    }
    private Item CreateItem()
    {
        //사용하고 있지 않은 아이템이 있을 경우 그대로 반환
        foreach (Item item in poolItem)
        {
            if(!item.gameObject.activeSelf)
                return item;
        }

        //모든 오브젝트가 사용 중일 경우 새로운 오브젝트 생성
        GameObject go = Instantiate(poolItem[0].gameObject, poolItem[0].transform.parent);
        Transform tr = go.transform;

        tr.localScale = Vector3.one;
        tr.localPosition = Vector3.zero;

        Item newItem = go.GetComponent<Item>();
        poolItem.Add(newItem);

        go.SetActive(true);
        return go.GetComponent<Item>();
    }

}
