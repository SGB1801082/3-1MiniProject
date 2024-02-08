using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] private Image imgIcon;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textUpgrade;

    [SerializeField] private GameObject opt;
    [SerializeField] private Image optIcon;
    [SerializeField] private TextMeshProUGUI optName;
    [SerializeField] private TextMeshProUGUI optUpgrade;

    private void Start()
    {
        int ranint = Random.Range(1, 10);
        //imgIcon.sprite = ResourceMgr.GetBoots("BattleguardArmor");
        //textName.text = "ITEM NAME";
        textUpgrade.text = "+" + ranint;
    }

    public void Init(Equipment equipment)
    {
        gameObject.SetActive(true);
        imgIcon.sprite = equipment.GetIcon();
        textName.text = imgIcon.sprite.name;
    }

    public void ItemClick()
    {
        opt.SetActive(true);
        optIcon.sprite = imgIcon.sprite;
        optName.text = textName.text;
        optUpgrade.text = "UPGRADE " + textUpgrade.text;
    }
}