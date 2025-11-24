using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public GameEvent onPlayerDeath;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            onPlayerDeath.Raise(this, collision);
            Debug.Log("Player collided with Floor");
        }
    }
}
