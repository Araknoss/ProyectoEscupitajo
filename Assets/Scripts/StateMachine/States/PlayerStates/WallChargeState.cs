using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChargeState : State
{
    [SerializeField] private PlayerController _input;
    //[SerializeField] private JumpState jumpState;
    [SerializeField] private AnimationClip chargeAnimation;
    [SerializeField] private GameEvent onWallCharge;
    [SerializeField] private Trick onWallChargeTrick;
    [SerializeField] private Transform spriteTransform;
    [SerializeField] private SpriteRenderer playerSprite;

    [SerializeField] private float minBufferTime;
    private bool jumpInputBuffered;
    
    public override void Enter()
    {
        if (chargeAnimation != null)
            animator.Play(chargeAnimation.name);
        _input.onCharge = true;
        onWallCharge.Raise(this, null);

        jumpInputBuffered = false;

        playerSprite.flipX = true;
    }
    public override void Do()
    {
        if(Input.GetButtonUp("Jump"))
        {
            if(time < minBufferTime)
            {
                jumpInputBuffered = true;
                return;
            }      
            Jump();
        }
        if (time > onWallChargeTrick.listenInputTime)
        {
            isComplete = true;
            _input.onCharge = false;
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
        body.velocity = Vector2.zero;
    }
    public override void Exit()
    {   
        playerSprite.flipX = false;
    }
}
