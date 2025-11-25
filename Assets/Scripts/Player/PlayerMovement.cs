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
    [SerializeField] private bool lookingRight;

    [Header("Dash")]
    [SerializeField] private KeyCode dashKey;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashForce;
    private float timeOnDash;
    private bool onDash;
    private bool isInWall;

    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D body;    

    private void Update()
    {
        CheckInputs();
        Flip();
        WallDash();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void CheckInputs()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
    }

    // Reemplaza HandleXInput + HandleYInput: normaliza el vector de entrada
    // para que la velocidad total no aumente al moverse en diagonal.
    private void HandleMovement()
    {
        if (onDash) return;

        Vector2 input = new Vector2(xInput, yInput);
        
        if (input.sqrMagnitude > 1f)
        {
            input = input.normalized;
        }

        body.velocity = input * moveSpeed;
    }

    private void Flip()
    {
        if (onDash) return;
        
        if (xInput > 0 && !lookingRight)
        {
            lookingRight = true;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (xInput < 0 && lookingRight)
        {
            lookingRight = false;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    private void WallDash()
    {
        if (Input.GetKeyDown(dashKey) && isInWall)
        {
            onDash = true;
            timeOnDash = 0;
            body.velocity = Vector2.zero;
            animator.SetTrigger("Dash");
            if (!lookingRight)
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

    public void SetIsInWall(Component sender, object data)
    {
        if (data is bool)
        {
            isInWall = (bool)data;
        }
    }

    public void SetPlayerSpeed(Component sender, object data)
    {        
        moveSpeed = (float)data;        
    }
}
