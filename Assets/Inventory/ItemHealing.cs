using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ItemEft/Consumable/Health")]
public class ItemHealing : ItemEffect
{
    public int healPoint = 0;
    public override bool ExcuteRole()
    {
        /*if (curPlayerHP <= maxPlayerHP*0.7)
        {
            curPlayerHP += maxPlayerHP * 0.3;
        }
        else
        {
            curPlayerHP = maxPlayerHP;
        }*/
        Debug.Log("PlayerHP "+healPoint);
        return true;
    }
}
