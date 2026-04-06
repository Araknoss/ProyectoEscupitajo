using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButtonAutoSelect : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnSelect(BaseEventData eventData)
    {
        // Aquí puedes enganchar sonido, animación o indicador visual.
    }
}