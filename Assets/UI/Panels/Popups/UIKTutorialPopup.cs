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

    protected override void Awake()
    {
        //backButton.onClick.AddListener(OnBackPressed);
        //quitButton.onClick.AddListener(OnQuitPressed);

    }

    protected override void OnShow()
    {
        SelectDefaultButton();
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
    }

    public override void HandleInput(Player player)
    {
        if (player.GetButtonDown(inputActionId))
        {
            OnBackPressed();
        }
    }
}
