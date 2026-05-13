using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Clase base para cualquier elemento de UI navegable.
/// </summary>
[RequireComponent(typeof(Selectable))]
public abstract class UISelectable : MonoBehaviour
{
    // Vecinos explícitos (se pueden asignar en Inspector o auto-detectar)
    [Header("Navigation")]
    public UISelectable NavUp;
    public UISelectable NavDown;
    public UISelectable NavLeft;
    public UISelectable NavRight;

    protected Selectable _selectable;

    public bool IsInteractable => _selectable != null && _selectable.interactable;

    protected virtual void Awake()
    {
        _selectable = GetComponent<Selectable>();
    }

    /// <summary>Llamado cuando este elemento recibe el foco.</summary>
    public virtual void OnFocusGained()
    {
        _selectable?.Select(); // Sincroniza con el sistema de Unity UI si lo usas
    }

    /// <summary>Llamado cuando este elemento pierde el foco.</summary>
    public virtual void OnFocusLost() { }

    /// <summary>Llamado al presionar Submit con este elemento enfocado.</summary>
    public abstract void OnSubmit();
}