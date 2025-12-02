using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor;
using UnityEngine;

public class PlayerController : Core
{
    [Header("States")]    
    [SerializeField] private IdleState idleState;   

    [Header("State Variables")] //They can be modified from each state


    [Header("Inputs")]
    [SerializeField] private PlayerController playerInputs;
    public float xInput { get; private set; }
    public bool startJumpInput { get; private set; }
    public bool jumpInput { get; private set; }


    public float moveSpeed;
    public bool lookingRight { get; private set; }
    [SerializeField] private float jumpForce;


    private void Awake()
    {
        lookingRight = false;
    }
    private void Start()
    {       
        SetupStates();
        ResetBools();        
        Set(idleState);
    }
    private void Update()
    {
        InitializeInputs();
        HandleJumpInput();
        SelectState();             
        FlipSprite();       
        state.Do();
    }
    private void FixedUpdate()
    {
        CheckGround();       
        state.FixedDo();
    }
    private void SelectState()
    {
        Set(idleState);
    }
    void InitializeInputs()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        startJumpInput = Input.GetButtonDown("Jump");
        jumpInput = Input.GetButton("Jump");
    }
    private void HandleJumpInput()
    {
        if (startJumpInput && groundSensor.grounded)
        {
            body.AddForce(groundSensor.GroundNormal() * jumpForce, ForceMode2D.Impulse);
        }
    }    
    private void CheckGround()
    {
        if (groundSensor.grounded)
        {
            ResetBools();
        }
    }    

    private void FlipSprite()
    {        
        if (xInput < 0 && lookingRight)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            lookingRight = false;
        }
        if (xInput > 0 && !lookingRight)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            lookingRight = true;
        }
    }

    private void ResetBools()
    {
    }
}
