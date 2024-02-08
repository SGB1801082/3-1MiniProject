using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boots : Equipment
{
    public Boots(string iconName) : base(iconName)
    {
    }

    public override Sprite GetIcon()
    {
        return ResourceMgr.GetBoots(strIconName);
    }
}
