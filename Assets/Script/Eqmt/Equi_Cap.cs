using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equi_Cap : Equiment
{
    public Equi_Cap(string iconName) : base(iconName)
    {
    }

    public override Sprite GetIcon()
    {
        return RescourceMgr.GetCap(strIconName);
    }
}
