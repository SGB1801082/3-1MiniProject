using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Equipment
{
    public Armor(string iconName) : base(iconName)
    {
    }

    public override Sprite GetIcon()
    {
        return ResourceMgr.GetArmor(strIconName);
    }
}
