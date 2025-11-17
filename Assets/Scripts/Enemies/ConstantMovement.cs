using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMovement : MonoBehaviour
{
    [SerializeField] private Vector2 movementSpeed;
    [SerializeField] private bool toRight;
    [SerializeField] private Rigidbody2D rb;
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        rb.velocity = toRight ? new Vector2(1,1) * movementSpeed : new Vector2(-1,1) * movementSpeed;
    }
}
