using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeGameEvent : MonoBehaviour
{
    [SerializeField] private GameEvent gameEvent;
    public void InvokeEvent()
    {
        if (gameEvent != null)
        {
            gameEvent.Raise(this, 1);
        }
    }
}
