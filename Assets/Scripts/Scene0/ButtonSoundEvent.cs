using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSoundEvent : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public virtual void OnPointerEnter(PointerEventData eventData)// Hover Event
    {
        AudioManager.single.PlaySfxClipChange(1);
        Debug.Log(gameObject.name + "is Hover");
    }
    public virtual void OnPointerDown(PointerEventData eventData)// Click Event
    {
        AudioManager.single.PlaySfxClipChange(0);
        Debug.Log(gameObject.name + "is Click");
    }

}
