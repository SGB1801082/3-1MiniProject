using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnAddUserNameSoundEvent : ButtonSoundEvent
{
    public bool ishover = false;
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (ishover)
        {
            base.OnPointerEnter(eventData);
        }
        
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
    }

}
