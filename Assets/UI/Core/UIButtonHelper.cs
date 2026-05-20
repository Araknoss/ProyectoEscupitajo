using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Helper para que los botones se seleccionen al pasar el ratón
/// </summary>
public class UIButtonHelper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Selecciona este botón cuando el ratón entra
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Aquí puedes dejar el botón seleccionado o deseleccionarlo
        // Por ahora, lo dejamos seleccionado
    }
}
