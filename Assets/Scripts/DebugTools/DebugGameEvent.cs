using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DebugGameEvent : MonoBehaviour
{
    [SerializeField] private KeyCode keyToRaise;
    [SerializeField] private GameEvent gameEvent;
    [SerializeField] private Trick trick;

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(keyToRaise) && trick !=null)
        {
            gameEvent.Raise(this, trick);
        }
#endif

    }
}
