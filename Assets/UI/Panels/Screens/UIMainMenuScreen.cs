using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMainMenuScreen : UIScreen
{
    public override bool CanGoBack => false;
    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button quitButton;

    //[Header("Default Selection")]
    //[SerializeField] private GameObject defaultSelected;

    [Header("Game Events")]
    [SerializeField] private GameEvent playEvent;
    [SerializeField] private GameEvent openSettingsEvent;
    [SerializeField] private GameEvent openShopEvent;
    [SerializeField] private GameEvent openQuitEvent;

    protected override void Awake()
    {
        //base.Awake();

        playButton.onClick.AddListener(OnPlayPressed);
        settingsButton.onClick.AddListener(OnSettingsPressed);
        shopButton.onClick.AddListener(OnShopPressed);
        quitButton.onClick.AddListener(OnQuitPressed);
    }

    protected override void OnShow()
    {
        SelectDefaultButton();
    }

    private void OnPlayPressed()
    {
        playEvent?.Raise(this, null);
    }

    private void OnSettingsPressed()
    {
        openSettingsEvent?.Raise(this, null);
    }

    private void OnShopPressed()
    {
        openShopEvent?.Raise(this, null);
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

}
