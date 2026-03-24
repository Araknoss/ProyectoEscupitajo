using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonHoverIndicator : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private RectTransform indicator;
    [SerializeField] private Vector2 offset = new Vector2(-40f, 0f);

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (indicator == null) return;

        // Activar el indicador
        indicator.gameObject.SetActive(true);

        // Calcular posición lateral del botón
        Vector3 targetPosition = rectTransform.TransformPoint(
            new Vector3(-rectTransform.rect.width * 0.5f, 0f, 0f) + (Vector3)offset
        );

        indicator.position = targetPosition;
    }
}
