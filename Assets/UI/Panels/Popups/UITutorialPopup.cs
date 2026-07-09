using MoreMountains.Feedbacks;
using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UITutorialPopup : UIPopup
{
    //public override bool CanGoBack => true;
    //[Header("Buttons")]
    //[SerializeField] private Button backButton;
    //[SerializeField] private Button quitButton;

    //[Header("Default Selection")]
    //[SerializeField] private GameObject defaultSelected;

    [Header("Game Events")]
    [SerializeField] private GameEvent quitEvent;
    [SerializeField] private GameEvent backEvent;

    [SerializeField] private MMF_Player popupIntroFeedback;
    [SerializeField] private MMF_Player popupOutroFeedback;

    protected override void Awake()
    {
        //backButton.onClick.AddListener(OnBackPressed);
        //quitButton.onClick.AddListener(OnQuitPressed);

    }

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
        popupOutroFeedback?.PlayFeedbacks();
    }

    public override void HandleInput(Player player)
    {
        
    }
    
}
