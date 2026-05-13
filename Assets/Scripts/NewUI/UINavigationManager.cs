using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gestiona el foco y la navegación entre UISelectables.
/// Solo un panel puede estar activo a la vez (stack-based).
/// </summary>
public class UINavigationManager : MonoBehaviour
{
    public static UINavigationManager Instance { get; private set; }

    [SerializeField] private UIInputHandler _inputHandler;

    private readonly Stack<UIPanel> _panelStack = new Stack<UIPanel>();
    private UISelectable _focused;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        _inputHandler.OnSubmit += HandleSubmit;
        _inputHandler.OnCancel += HandleCancel;
        _inputHandler.OnNavigate += HandleNavigate;
    }

    private void OnDisable()
    {
        _inputHandler.OnSubmit -= HandleSubmit;
        _inputHandler.OnCancel -= HandleCancel;
        _inputHandler.OnNavigate -= HandleNavigate;
    }

    // ?? Panel stack ??????????????????????????????????????????????????????????

    /// <summary>Abre un panel y lo pone en el top del stack.</summary>
    public void PushPanel(UIPanel panel)
    {
        if (_panelStack.Count > 0)
            _panelStack.Peek().SetInputEnabled(false);

        _panelStack.Push(panel);
        panel.Open();
        SetFocus(panel.DefaultSelectable);
    }

    /// <summary>Cierra el panel actual y vuelve al anterior.</summary>
    public void PopPanel()
    {
        if (_panelStack.Count == 0) return;

        var current = _panelStack.Pop();
        current.Close();

        if (_panelStack.Count > 0)
        {
            var previous = _panelStack.Peek();
            previous.SetInputEnabled(true);
            SetFocus(previous.LastFocused);
        }
        else
        {
            _focused = null;
        }
    }

    // ?? Foco ?????????????????????????????????????????????????????????????????

    public void SetFocus(UISelectable target)
    {
        if (target == null || !target.IsInteractable) return;

        _focused?.OnFocusLost();
        _focused = target;
        _focused.OnFocusGained();

        // Guarda el último elemento enfocado en el panel activo
        if (_panelStack.Count > 0)
            _panelStack.Peek().LastFocused = _focused;
    }

    // ?? Handlers de input ????????????????????????????????????????????????????

    private void HandleSubmit()
    {
        _focused?.OnSubmit();
    }

    private void HandleCancel()
    {
        if (_panelStack.Count > 0 && _panelStack.Peek().CanClose)
            PopPanel();
    }

    private void HandleNavigate(Vector2Int dir)
    {
        if (_focused == null) return;

        UISelectable next = dir switch
        {
            { y: 1 } => _focused.NavUp,
            { y: -1 } => _focused.NavDown,
            { x: -1 } => _focused.NavLeft,
            { x: 1 } => _focused.NavRight,
            _ => null
        };

        if (next != null && next.IsInteractable)
            SetFocus(next);
    }
}
