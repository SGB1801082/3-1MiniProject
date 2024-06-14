using UnityEngine.EventSystems;

public class NameIOSound : ButtonSoundEvent
{
    private bool notEnter = false;
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (notEnter)
        {
            base.OnPointerEnter(eventData);
        }
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
    }
}
