using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Inputs")]
    private float xInput;
    private float yInput;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    private bool mirandoDerecha;

    [Header("Dash")]
    [SerializeField] private KeyCode dashKey;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashForce;
    private float timeOnDash;    
    private bool onDash;    

    [Header("Components")]
    [SerializeField] private Rigidbody2D body;  

    private void Awake()
    {
        mirandoDerecha = true;
    }
    private void Update()
    {
        CheckInputs();       
        Flip();
        Dash();       
    }

    private void FixedUpdate()
    {
        HandleXInput();
        HandleYInput();
    }

    private void CheckInputs()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
    }

    private void HandleXInput()
    {
        if (!onDash)
        {
            body.velocity = new Vector2(xInput * moveSpeed, body.velocity.y);
        }        
    }

    private void HandleYInput()
    {
        if (!onDash)
        {
            body.velocity = new Vector2(body.velocity.x, yInput * moveSpeed);
        }
    }

    private void Flip()
    {
        if (xInput > 0 && !mirandoDerecha)
        {
            mirandoDerecha = true;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (xInput < 0 && mirandoDerecha)
        {
            mirandoDerecha = false;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }        
    } 

    private void Dash()
    {
        if (Input.GetKeyDown(dashKey))
        {
            onDash = true;
            timeOnDash = 0;           
            body.velocity = Vector2.zero;
            if (mirandoDerecha)
            {
                body.AddForce(Vector2.right * dashForce, ForceMode2D.Impulse);
            }
            else
            {
                body.AddForce(Vector2.left * dashForce, ForceMode2D.Impulse);
            }

        }
        timeOnDash += Time.deltaTime;
        if (timeOnDash >= dashTime)
        {
            onDash = false;
        }
    }     
}
