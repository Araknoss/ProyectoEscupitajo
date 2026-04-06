using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDemoTrigger : MonoBehaviour
{
    [SerializeField] private GameEvent gameEvent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Trigger Entered");
            gameEvent.Raise(this, true);
        }
    }
}
