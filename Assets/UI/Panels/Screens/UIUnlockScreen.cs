using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIUnlockScreen : UIScreen
{
    [Header("Buttons")]
    [SerializeField] private Button backButton;    

    [Header("Bindings")]
    [SerializeField] private TextMeshProUGUI trickNameText;
    [SerializeField] private Image trickImage;

    [Header("Feedback")]
    [SerializeField] private MMF_Player unlockFeedback;

    [Header("Default Selection")]
    [SerializeField] private GameObject defaultSelected;

    [Header("Game Events")]
    [SerializeField] private GameEvent backEvent;

    protected override void Awake()
    {
        //base.Awake();

        backButton.onClick.AddListener(OnBackPressed);
        
    }
    protected override void OnShow()
    {      
        base.OnShow();
        unlockFeedback?.PlayFeedbacks();
        SelectDefaultButton();
    }

    protected override void OnHide()
    {
        unlockFeedback?.StopFeedbacks();
        backButton.gameObject.SetActive(false);        
    }

    public void AssignUnlockTrickValues(Component sender, object data)
    {
        if(data is Trick)
        {
            Trick trick = (Trick)data;
            if (trick == null) return;
            if (trickNameText != null) trickNameText.text = trick.trickName;
            if (trickImage != null) trickImage.sprite = trick.sprite;
        }        
    }
    private void SelectDefaultButton()
    {
        if (defaultSelected == null)
            return;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(defaultSelected);
    }

    private void OnBackPressed()
    {
        backEvent.Raise(this, null);
    }
}
