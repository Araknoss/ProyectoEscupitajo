using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeGameEvent : MonoBehaviour
{
    [SerializeField] private GameEvent gameEvent;
    [SerializeField] private int data =1;
    public void InvokeEvent()
    {
        if (gameEvent != null)
        {
            gameEvent.Raise(this, data);
        }
    }
}
