using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor;
using UnityEngine;

public class PlayerController : Core
{
    [Header("States")]    
    [SerializeField] private IdleState idleState;   
    [SerializeField] private MoveState moveState;
    [SerializeField] private WallState wallState;  
    [SerializeField] private JumpState jumpState;

    [Header("Internal Variables")]
    public bool onJump;

    [Header("Inputs")]
    [SerializeField] private PlayerController playerInputs;   
    public float xInput { get; private set; }
    public float yInput { get; private set; }
    public bool startJumpInput { get; private set; }
    public bool jumpInput { get; private set; }   
    public bool lookingRight { get; private set; }

    [Header("Ground Sensor")]
    public GroundSensor groundSensor;   

    private void Awake()
    {
        lookingRight = false;
    }
    private void Start()
    {       
        SetupInstances();             
        Set(idleState);
    }
    private void Update()
    {
        InitializeInputs();      
        SelectState();             
        FlipSprite();   
       
        state.Do();
    }
    private void FixedUpdate()
    {         
        state.FixedDo();
    }
    private void SelectState()
    {
        if(onJump)
        {
            Set(jumpState);
            return;
        }
        if (groundSensor.grounded)
        {
            if (startJumpInput)
            {
                Set(jumpState);
                return;
            }
            Set(wallState);            
            return;
        }
        if (xInput!=0 || yInput !=0)
        {
            Set(moveState);
            return;
        }
        Set(idleState);
    }
    void InitializeInputs()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput=Input.GetAxisRaw("Vertical");
        startJumpInput = Input.GetButtonDown("Jump");
        jumpInput = Input.GetButton("Jump");
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

}
