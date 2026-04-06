using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

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
    public bool onWall;

    [Header("Inputs")]
    //[SerializeField] private PlayerController playerInputs;   
    [SerializeField] private int playerId;
    private Player rewiredPlayer;
    public float xInput { get; private set; }
    public float yInput { get; private set; }
    public bool startJumpInput { get; private set; }
    public bool jumpInput { get; private set; }
    public bool releaseJumpInput { get; private set; }
    public bool lookingRight;

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

        rewiredPlayer = ReInput.players.GetPlayer(playerId);
    }
    private void Update()
    {
        InitializeInputs();      
        SelectState();
        state.Do();               
        FlipSprite();   
       
        
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
        xInput = rewiredPlayer.GetAxisRaw("HorizontalMovement");
        yInput= rewiredPlayer.GetAxisRaw("VerticalMovement");
        startJumpInput = rewiredPlayer.GetButtonDown("KeepTrick");
        jumpInput = rewiredPlayer.GetButton("KeepTrick");
        releaseJumpInput = rewiredPlayer.GetButtonUp("KeepTrick");
    }         

    public void FlipSprite()
    {
        if (onJump || onCharge || onWall) return;        
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
