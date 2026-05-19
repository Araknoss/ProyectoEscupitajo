using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUnlockScreen : UIScreen
{
    [Header("Bindings")]
    [SerializeField] private TextMeshProUGUI trickNameText;
    [SerializeField] private Image trickImage;

    [Header("Feedback")]
    [SerializeField] private MMF_Player unlockFeedback;

    protected override void OnShow()
    {      
        base.OnShow();
        unlockFeedback?.PlayFeedbacks();
    }

    //protected override void OnHide()
    //{
    //    base.OnHide();
    //}   

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
}
