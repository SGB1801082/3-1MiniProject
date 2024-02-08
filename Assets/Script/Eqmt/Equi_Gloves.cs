using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equi_Gloves : Equiment
{
    public Equi_Gloves(string iconName) : base(iconName)
    {
    }

    public override Sprite GetIcon()
    {
        return RescourceMgr.GetGloves(strIconName);
    }
}
