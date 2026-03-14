using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class WallState : State
{
    [SerializeField] private AnimationClip moveAnimation;
    [SerializeField] private AnimationClip idleAnimation;
    [SerializeField] private PlayerController _input;
    [SerializeField] private float moveSpeed;
    private Vector2 moveInput;

    public GameEvent onWallDetection;

    [SerializeField] private SpriteRenderer playerSprite;
    private Vector3 originalPlayerSpriteScale;

    public override void Enter()
    {
        animator.Play(moveAnimation.name);

        onWallDetection.Raise(this, true);        

        //originalPlayerSpriteScale = playerSprite.localScale;
        //if (_input.groundSensor.groundNormal.x > 0)
        //{
        //    playerSprite.localScale = new Vector3(-1, 1, 1);
        //}
        //else
        //{
        //    playerSprite.localScale = new Vector3(1, 1, 1);
        //}
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
    public override void Exit()
    {
        onWallDetection.Raise(this, false);

        animator.Play(idleAnimation.name);       

        //playerSprite.localScale = originalPlayerSpriteScale;
    }
}
