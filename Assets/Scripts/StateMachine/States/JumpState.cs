using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class JumpState : State
{
    [SerializeField] private AnimationClip moveAnimation;
    [SerializeField] private PlayerController _input;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpTime;
    private Vector2 moveInput;
    private Vector2 jumpDirection;
    private bool onJump;
    public override void Enter()
    {
        animator.Play(moveAnimation.name);
        jumpDirection = groundSensor.GroundNormal();
        _input.onJump = true;
    }
    public override void Do()
    {
        if(time > jumpTime)
        {
            _input.onJump = false;
        }
    }

    public override void FixedDo() 
    {        
        body.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);        
    }
    public override void Exit() { }
}
