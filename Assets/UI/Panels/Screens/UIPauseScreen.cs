using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPauseScreen : UIScreen
{
    //public override bool CanGoBack => true;
    [Header("Buttons")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;

    [Header("Default Selection")]
    [SerializeField] private GameObject defaultSelected;

    [Header("Game Events")]    
    [SerializeField] private GameEvent resumeEvent;
    [SerializeField] private GameEvent openSettingsEvent;
    [SerializeField] private GameEvent mainMenuEvent;
    [SerializeField] private GameEvent openQuitEvent;

    protected override void Awake()
    {
        //base.Awake();

        resumeButton.onClick.AddListener(OnResumePressed);
        settingsButton.onClick.AddListener(OnSettingsPressed);
        mainMenuButton.onClick.AddListener(OnMainMenuPressed);
        quitButton.onClick.AddListener(OnQuitPressed);
    }

    protected override void OnShow()
    {
        SelectDefaultButton();        
    }

    private void OnResumePressed() 
    {       
        resumeEvent?.Raise(this, null);
    }

    private void OnSettingsPressed()
    {
        openSettingsEvent?.Raise(this, null);
    }

    private void OnMainMenuPressed()
    {
        mainMenuEvent?.Raise(this, null);
    }

    private void OnQuitPressed()
    {
        openQuitEvent?.Raise(this, null);
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
