using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleInit : MonoBehaviour
{
    public TMP_Text title;

    public void Init(string title)
    {
        this.title.text = title;
    }

}
