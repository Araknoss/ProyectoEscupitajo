using UnityEngine;

/// <summary>
/// Clase base para cualquier pantalla o panel de UI.
/// </summary>
public abstract class UIPanel : MonoBehaviour
{
    [Header("Panel Config")]
    public UISelectable DefaultSelectable; // Primer elemento con foco al abrir
    public bool CanClose = true;           // Si Back/Cancel puede cerrarlo

    [HideInInspector] public UISelectable LastFocused;

    private bool _inputEnabled = true;

    public virtual void Open()
    {
        gameObject.SetActive(true);
        SetInputEnabled(true);
    }

    public virtual void Close()
    {
        SetInputEnabled(false);
        gameObject.SetActive(false);
    }

    public void SetInputEnabled(bool enabled)
    {
        _inputEnabled = enabled;
    }
}
