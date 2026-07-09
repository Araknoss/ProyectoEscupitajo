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
        if (!waitElapsed)
        {
            timer += Time.unscaledDeltaTime;

            if (timer < waitTime)
                return false;

            waitElapsed = true;
        }

        return player.GetButtonDown(actionName);
    }
}