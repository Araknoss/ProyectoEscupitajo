using UnityEngine;
using UnityEngine.EventSystems;
using FMODUnity;

public class UIButtonHighlightSound : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    [SerializeField] private bool playHoverSoundOnSelect = true;
    [SerializeField] private bool playPressSoundOnClick = true;
    public void OnSelect(BaseEventData eventData)
    {
        PlayHoverSound();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayHoverSound();
    }

    private void PlayHoverSound()
    {
        if (!playHoverSoundOnSelect) return;
        AudioManager.Instance.PlayHoverSound();
    }

    public void PlayButtonPressSound()
    {
        if (!playPressSoundOnClick) return;
        AudioManager.Instance.PlayButtonPressSound();
    }
}
