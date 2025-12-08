using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class MoveState : State
{
    [SerializeField] private AnimationClip animationClip;
    [SerializeField] private PlayerController _input;
    [SerializeField] private float moveSpeed;
    private Vector2 moveInput;
    
    public override void Enter()
    {
        animator.Play(animationClip.name);
    }
    public override void Do()
    {
        moveInput= new Vector2(_input.xInput, _input.yInput);

        if (moveInput.sqrMagnitude > 1f)
        {
            moveInput = moveInput.normalized;
        }          
    }

    public override void FixedDo() 
    {
        body.velocity = moveInput * moveSpeed;
    }
    public override void Exit() { }
}
