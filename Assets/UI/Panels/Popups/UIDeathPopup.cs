using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDeathPopup : UIPopup
{

    public override bool CanGoBack => false;
    [Header("Buttons")]
    [SerializeField] private Button retryButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button quitButton;

    //[Header("Default Selection")]
    //[SerializeField] private GameObject defaultSelected;

    [Header("Game Events")]

    [SerializeField] private GameEvent quitEvent;
    [SerializeField] private GameEvent retryEvent;
    [SerializeField] private GameEvent mainMenuEvent;


    protected override void Awake()
    {
        //base.Awake();

        retryButton.onClick.AddListener(OnRetryPressed);
        menuButton.onClick.AddListener(OnMenuPressed);
        quitButton.onClick.AddListener(OnQuitPressed);

    }

    protected override void OnShow()
    {
        SelectDefaultButton();
    }

    private void OnRetryPressed()
    {
        retryEvent?.Raise(this, null);
    }
    private void OnQuitPressed()
    {
        quitEvent?.Raise(this, null);
    }

    private void OnMenuPressed()
    {
        mainMenuEvent?.Raise(this, null);
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
}
