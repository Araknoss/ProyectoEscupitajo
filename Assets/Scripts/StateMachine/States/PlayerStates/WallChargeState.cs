using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChargeState : State
{
    [SerializeField] private PlayerController _input;
    [SerializeField] private GroundSensor groundSensor;
    //[SerializeField] private JumpState jumpState;
    [SerializeField] private AnimationClip chargeAnimation;
    [SerializeField] private GameEvent onWallCharge;
    [SerializeField] private GameEvent onWallChargeFailed;
    [SerializeField] private Trick onWallChargeTrick;
    [SerializeField] private Transform spriteTransform;
    [SerializeField] private SpriteRenderer playerSprite;

    [SerializeField] private float minBufferTime;
    private bool jumpInputBuffered;

    [SerializeField] private float moveSpeed;
    private Vector2 moveInput;

    [SerializeField] private float coyoteTime=0.1f;
    private float coyoteTimer;



    public override void Enter()
    {
        if (chargeAnimation != null)
            animator.Play(chargeAnimation.name);
        _input.onCharge = true;
        onWallCharge.Raise(this, true);

        jumpInputBuffered = false;

        coyoteTimer = 0;
    }
    public override void Do()
    {
        moveInput = new Vector2(0, _input.yInput);

        if (moveInput.sqrMagnitude > 1f)
        {
            moveInput = moveInput.normalized;
        }

        if (_input.releaseJumpInput)
        {
            if(time < minBufferTime)
            {
                jumpInputBuffered = true;
                return;
            }      
            Jump();
        }
        //if (time > onWallChargeTrick.listenInputTime)
        //{
        //    isComplete = true;
        //    _input.onCharge = false;
        //}
        if (!groundSensor.grounded)
        {
            coyoteTimer += Time.deltaTime;
            if(coyoteTimer >= coyoteTime)
            {
                isComplete = true;
                _input.onCharge = false;
                onWallChargeFailed.Raise(this, null);
            }            
        }
        if(time > minBufferTime && jumpInputBuffered) //Por si lo mantiene muy poco
        {
            Jump();
        }
    }

    public void Jump()
    {
        isComplete = true;
        _input.onCharge = false;
        _input.onJump = true;
    }

    public override void FixedDo()
    {
        body.velocity = moveInput * moveSpeed;
    }
    public override void Exit()
    {       
        coyoteTimer = 0;
        onWallCharge.Raise(this, false);
    }
}
