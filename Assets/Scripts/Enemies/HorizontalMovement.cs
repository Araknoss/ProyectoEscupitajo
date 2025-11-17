using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private bool toRight;
    [SerializeField] private Rigidbody2D rb;
    private void FixedUpdate()
    {
        ConstantMovement();
    }
    private void ConstantMovement()
    {
        rb.velocity = toRight ? Vector2.right * movementSpeed : Vector2.left * movementSpeed;
    }
}
