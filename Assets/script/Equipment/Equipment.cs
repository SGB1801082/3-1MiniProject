using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Equipment
{
    protected string strIconName;
    public Equipment(string iconName)
    {
        this.strIconName = iconName;
    }
    public abstract Sprite GetIcon();
}
