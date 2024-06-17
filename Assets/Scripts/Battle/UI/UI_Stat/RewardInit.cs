using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardInit : MonoBehaviour
{
    public Image reward_Icon;
    public TMP_Text reward_Text;


    public void Init(Sprite icon, string text) 
    {
        reward_Icon.sprite = icon;
        this.reward_Text.text = text;
    }
}
