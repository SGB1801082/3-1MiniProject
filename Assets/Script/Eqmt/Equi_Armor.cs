using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equi_Armor : Equiment
{
    public Equi_Armor(string iconName) : base(iconName)
    {

    }

    public override Sprite GetIcon()
    {
        return RescourceMgr.GetArmor(strIconName);
    }
}
