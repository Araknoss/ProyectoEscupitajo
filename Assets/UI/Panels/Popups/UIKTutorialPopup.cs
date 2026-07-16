using MoreMountains.Feedbacks;
using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIKTutorialPopup : UIPopup
{
    [Header("Game Events")]
    [SerializeField] private GameEvent quitEvent;
    [SerializeField] private GameEvent backEvent;

    [SerializeField] private int inputActionId;
    [SerializeField] private int alternativeInputActionId = -1;

    [SerializeField] private MMF_Player popupIntroFeedback;
    [SerializeField] private MMF_Player popupOutroFeedback;

    protected override void OnShow()
    {
        SelectDefaultButton();
        if (popupIntroFeedback != null)
        {
            popupIntroFeedback.PlayFeedbacks();
        }
    }

    private void OnQuitPressed()
    {
        quitEvent?.Raise(this, null);
    }

    private void OnBackPressed()
    {
        backEvent?.Raise(this, null);
    }

    private void SelectDefaultButton()
    {
        if (defaultSelected == null)
            return;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(defaultSelected);
    }

    protected override void OnHide()
    {
        //resumeEvent.Raise(this, null);
        if (popupIntroFeedback != null)
        {
            popupOutroFeedback.PlayFeedbacks();
        }
    }

    public override void HandleInput(Player player)
    {
        bool primaryPressed = player.GetButtonDown(inputActionId);
        bool alternativePressed = alternativeInputActionId >= 0 && player.GetButtonDown(alternativeInputActionId);

        if (!primaryPressed && !alternativePressed)
            return;

        // -------- SKIP INTRO ANIMATION --------
        // Si la animación de entrada aún se está reproduciendo,
        // cualquiera de estos inputs solo la completa, sin cerrar el popup todavía.

        if (popupIntroFeedback != null && popupIntroFeedback.HasFeedbackStillPlaying())
        {
            popupIntroFeedback.SkipToTheEnd();
            return;
        }

        OnBackPressed();
    }
}
