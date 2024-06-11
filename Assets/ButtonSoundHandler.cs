using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSoundHandler : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioClip hoverClip;
    public AudioClip clickClip;
    [Range(0f, 1f)] public float hoverVolume = 1.0f;
    [Range(0f, 1f)] public float clickVolume = 1.0f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.instance.PlaySound(hoverClip, hoverVolume);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.instance.PlaySound(clickClip, clickVolume);
    }
}
