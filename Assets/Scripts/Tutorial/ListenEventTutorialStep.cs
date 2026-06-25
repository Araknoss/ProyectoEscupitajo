using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenEventTutorialStep : TutorialStep
{
    private Player player;      
    private bool completed = false;
    public void HandleOnEventListened(Component sender, object data)
    {
        if (data != null)
        {
            if(data is bool)
            {
                completed = (bool)data;
            }             
            
            if(data is Trick)
            {
                completed = true;
            }
        }
    }
    public override bool IsCompleted()
    {
        return completed;
    }
}
