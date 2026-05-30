using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameplayScreen : UIScreen
{
    public override bool CanGoBack => false;
    [SerializeField] private GameEvent resumeEvent;
    protected void OnShow(object data)
    {
        resumeEvent.Raise(this, null);
    }
}
