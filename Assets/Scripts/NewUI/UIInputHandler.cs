using UnityEngine;
using Rewired;

/// <summary>
/// Abstrae el input de Rewired para la UI.
/// Todos los sistemas de UI consultan aquí, nunca Rewired directamente.
/// </summary>
public class UIInputHandler : MonoBehaviour
{
    // Nombres de acciones en Rewired (configúralos en el Rewired Editor)
    private const string UI_SUBMIT = "UI_Submit";
    private const string UI_CANCEL = "UI_Cancel";
    private const string UI_NAV_UP = "UI_Navigate_Up";
    private const string UI_NAV_DOWN = "UI_Navigate_Down";
    private const string UI_NAV_LEFT = "UI_Navigate_Left";
    private const string UI_NAV_RIGHT = "UI_Navigate_Right";

    private Player _player;

    // Eventos que el NavigationManager escucha
    public System.Action OnSubmit;
    public System.Action OnCancel;
    public System.Action<Vector2Int> OnNavigate; // (-1/0/1, -1/0/1)

    private void Awake()
    {
        // Jugador 0; ajusta si tienes multijugador local
        _player = ReInput.players.GetPlayer(0);
    }

    private void Update()
    {
        if (_player == null) return;

        // Submit / Cancel
        if (_player.GetButtonDown(UI_SUBMIT)) OnSubmit?.Invoke();
        if (_player.GetButtonDown(UI_CANCEL)) OnCancel?.Invoke();

        // Navegación direccional (GetButtonDown evita repetición continua)
        var dir = Vector2Int.zero;
        if (_player.GetButtonDown(UI_NAV_UP)) dir.y = 1;
        if (_player.GetButtonDown(UI_NAV_DOWN)) dir.y = -1;
        if (_player.GetButtonDown(UI_NAV_LEFT)) dir.x = -1;
        if (_player.GetButtonDown(UI_NAV_RIGHT)) dir.x = 1;

        if (dir != Vector2Int.zero) OnNavigate?.Invoke(dir);
    }
}
