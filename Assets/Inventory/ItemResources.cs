using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemResources : MonoBehaviour
{
    public static ItemResources instance;
    public List<Item> itemRS = new List<Item>();

    private void Awake()
    {
        instance = this;
    }

    //FieldItem Prefab copy code
    /*
    public GameObject fieldItemPrefab;
    public Vector3[] pos;

    private void Start()
    {
        for(int i = 0; i < 6; i++)
        {
            GameObject go = Instantiate(fieldItemPrefab, pos[i], Quaternion.identity);
            go.GetComponent<FieldItems>().SetItem(itemRS[Random.Range(0, 3)]);
        }
    }
    */
}
