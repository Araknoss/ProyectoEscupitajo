using Rewired;
using UnityEngine;

public class PopupInputTutorialStep : TutorialStep
{
    [Header("Popup Events")]
    [SerializeField] private GameEvent openPopupEvent;
    [SerializeField] private GameEvent closePopupEvent;
    [SerializeField] private GameEvent onGamePause;
    [SerializeField] private GameEvent onGameResume;

    [Header("Input")]
    [SerializeField] private string actionName = "UISubmit";
    [SerializeField] private string alternativeActionName;

    [Header("Wait Time")]
    [SerializeField] private float waitTime = 2f;

    private Player player;

    private float timer;
    private bool waitElapsed;

    private void Awake()
    {
        player = ReInput.players.GetPlayer(0);
    }

    public override void EnterStep()
    {
        base.EnterStep();

        timer = 0f;
        waitElapsed = false;

        openPopupEvent?.Raise(this, null);
        onGamePause?.Raise(this, null);

    }

    public override void ExitStep()
    {
        closePopupEvent?.Raise(this, null);
        onGameResume?.Raise(this, null);

        base.ExitStep();
    }

    public override bool IsCompleted()
    {
        bool primaryPressed = player.GetButtonDown(actionName);
        bool alternativePressed = !string.IsNullOrEmpty(alternativeActionName) && player.GetButtonDown(alternativeActionName);
        bool anyPressed = primaryPressed || alternativePressed;

        if (!waitElapsed)
        {
            // -------- SKIP WAIT TIME --------
            // Si se pulsa el botón mientras aún se está esperando,
            // esto solo completa el tiempo de espera, sin completar el paso todavía.

            if (anyPressed)
            {
                waitElapsed = true;
                return false;
            }

            timer += Time.unscaledDeltaTime;

            if (timer < waitTime)
                return false;

            waitElapsed = true;
            return false;
        }

        return anyPressed;
    }

}