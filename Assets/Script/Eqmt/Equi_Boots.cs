using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equi_Boots : Equiment
{
    public Equi_Boots(string iconName) : base(iconName)
    {

    }

    public override Sprite GetIcon()
    {
        return RescourceMgr.GetBoots(strIconName);
    }
}
