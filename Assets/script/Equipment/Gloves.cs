using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gloves : Equipment
{
    public Gloves(string iconName) : base(iconName)
    {
    }

    public override Sprite GetIcon()
    {
        return ResourceMgr.GetGloves(strIconName);
    }
}
