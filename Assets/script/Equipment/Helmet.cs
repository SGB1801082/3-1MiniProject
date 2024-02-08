using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helmet : Equipment
{
    public Helmet(string iconName) : base(iconName)
    {
    }

    public override Sprite GetIcon()
    {
        return ResourceMgr.GetHelmet(strIconName);
    }
}
