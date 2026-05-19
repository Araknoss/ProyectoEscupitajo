// UIInputRouter.cs

using Rewired;
using UnityEngine;

public class UIInputRouter : MonoBehaviour
{
    private Player player;

    [Header("References")]
    [SerializeField] private UIManager uiManager;

    [Header("GameEvents")]
    [SerializeField] private GameEvent backEvent;

    private void Awake()
    {
        player = ReInput.players.GetPlayer(0);
    }

    private void Update()
    {
        if (player == null)
            return;

        // -------- GLOBAL INPUT --------

        if (player.GetButtonDown("UICancel"))
        {
            backEvent.Raise(this, null);
        }

        // -------- LOCAL SCREEN INPUT --------

        if (uiManager.CurrentScreen != null)
        {
            uiManager.CurrentScreen.HandleInput(player);
        }
    }
}
