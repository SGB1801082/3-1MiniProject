using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubInfo : MonoBehaviour
{
    [SerializeField] private Image imgIcon;
    [SerializeField] private Image imgFrame;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textUpgrade;

    private void Start()
    {
        imgIcon.sprite = RescourceMgr.GetArmor("royaltunic");
        textName.text = "Test2";
        textUpgrade.text = "UPDATE+9999";
    }

}
