using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChargeState : State
{
    [SerializeField] private PlayerController _input;
    //[SerializeField] private JumpState jumpState;
    [SerializeField] private AnimationClip chargeAnimation;
    public override void Enter()
    {
        if (chargeAnimation != null)
            animator.Play(chargeAnimation.name);
        _input.onCharge = true;
    }
    public override void Do()
    {
        if(Input.GetButtonUp("Jump"))
        {
            isComplete = true;
            _input.onCharge = false;
        }
    }

    public override void FixedDo()
    {
        body.velocity = Vector2.zero;
    }
    public override void Exit()
    {   
       _input.onJump = true;
    }
}
