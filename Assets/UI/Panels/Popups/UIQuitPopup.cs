using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIQuitPopup : UIPopup
{
    //public override bool CanGoBack => true;
    [Header("Buttons")]
    [SerializeField] private Button backButton;
    [SerializeField] private Button quitButton;

    //[Header("Default Selection")]
    //[SerializeField] private GameObject defaultSelected;

    [Header("Game Events")]
    [SerializeField] private GameEvent quitEvent;
    [SerializeField] private GameEvent backEvent;


    protected override void Awake()
    {
        //base.Awake();

        backButton.onClick.AddListener(OnBackPressed);
        quitButton.onClick.AddListener(OnQuitPressed);
        
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
}
