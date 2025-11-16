using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D rb;
    private float horizontalInput;
    private float verticalInput;
    private void Update()
    {
        CheckInputs();
        HandleXInput();
        HandleYInput();
        ApplyFriction();
    }
    private void CheckInputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
    private void HandleXInput()
    {        
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    private void HandleYInput()
    {
        rb.velocity = new Vector2(rb.velocity.x, verticalInput * moveSpeed);
    }

    private void ApplyFriction()
    {
        if(horizontalInput == 0 && verticalInput == 0)
        {            
            rb.velocity=Vector2.zero;
        }
    }

}
