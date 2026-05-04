using UnityEngine;
using UnityEngine.EventSystems;

public class PersistentUIButtonSelection : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    [Header("Options")]
    [SerializeField] private bool selectOnHover = true;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!selectOnHover) return;

        if(EventSystem.current.currentSelectedGameObject == gameObject) return;
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnSelect(BaseEventData eventData)
    {
        // Aquí puedes reproducir sonido, lanzar feedback, etc.
        Debug.Log("Selected: " + gameObject.name);
    }
}
