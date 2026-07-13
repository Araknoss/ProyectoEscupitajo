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
    [SerializeField] private GameEvent pauseEvent;

    private void Awake()
    {
        player = ReInput.players.GetPlayer(0);
    }

    private void Update()
    {
        if (player == null)
            return;

        // -------- POPUP PRIORITY --------

        if (uiManager.CurrentPopup != null)
        {
            uiManager.CurrentPopup.HandleInput(player);
            return;
        }

        // -------- GLOBAL INPUT --------

        if (player.GetButtonDown("UICancel"))
        {
            backEvent.Raise(this, null);
            Debug.Log("Back button pressed");
            return;
        }

        if (player.GetButtonDown("Pause"))
        {
            if (GameStateManager.Instance != null && !GameStateManager.Instance.CanPause())
            {
                Debug.Log("Pause bloqueado: tutorial o carga en curso");
                return;
            }

            Debug.Log("Pause pressed");
            pauseEvent.Raise(this, null);
            return;
        }

        // -------- LOCAL SCREEN INPUT --------        

        if (uiManager.CurrentScreen != null)
        {
            uiManager.CurrentScreen.HandleInput(player);
            return;
        }

        // -------- GAMEPLAY --------

        //if (player.GetButtonDown("Pause"))
        //{
        //    pauseEvent.Raise(this, null);
        //}
    }
}
