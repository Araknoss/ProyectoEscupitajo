using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CollisionDetection : MonoBehaviour
{
    public GameEvent onPlayerDeath;
    //public GameEvent onWallDetection;

    private bool isInvulnerable = false;
    [SerializeField] private Toggle toggle;
    //private void OnCollisionEnter2D(Collision2D collision)
    //{        
    //    if (collision.gameObject.CompareTag("Wall"))
    //    {
    //        onWallDetection.Raise(this, true);
    //        Debug.Log("Player collided with Wall");
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Wall"))
    //    {
    //        onWallDetection.Raise(this, false);
    //        Debug.Log("Player exited collision with Wall");
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && !isInvulnerable)
        {
            onPlayerDeath.Raise(this, collision);
            Debug.Log("Player triggered with Obstacle");
        }        
    }

    public void SetInvulnerability()
    {
        isInvulnerable = toggle.isOn;
    }
}

