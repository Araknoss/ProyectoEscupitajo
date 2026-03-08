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
    [SerializeField] private WallChargeState wallChargeState;
    [SerializeField] private JumpState jumpState;

    [Header("Internal Variables")]
    public bool onJump;
    public bool onCharge;

    [Header("Inputs")]
    [SerializeField] private PlayerController playerInputs;   
    public float xInput { get; private set; }
    public float yInput { get; private set; }
    public bool startJumpInput { get; private set; }
    public bool jumpInput { get; private set; }   
    public bool lookingRight { get; private set; }

    [Header("Ground Sensor")]
    public GroundSensor groundSensor;

    [Header("Sprite Object")]
    public GameObject spriteObject;

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
        //if (actualState == wallChargeState)
        //{
        //    if(actualState.isComplete)
        //        Set(jumpState);
        //}
        if (onCharge)
        {
            Set(wallChargeState);
            return;
        }
        if(onJump)
        {
            Set(jumpState);
            return;
        }
        if (groundSensor.grounded)
        {
            if (startJumpInput)
            {
                //Set(jumpState);
                Set(wallChargeState);
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
        if (xInput > 0 && !lookingRight)
        {
            lookingRight = true;
            spriteObject.transform.localScale = new Vector3(-spriteObject.transform.localScale.x, spriteObject.transform.localScale.y, spriteObject.transform.localScale.z);
        }
        else if (xInput < 0 && lookingRight)
        {
            lookingRight = false;
            spriteObject.transform.localScale = new Vector3(-spriteObject.transform.localScale.x, spriteObject.transform.localScale.y, spriteObject.transform.localScale.z);
        }
    }

}
