using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using FMODUnity;

public class WallState : State
{
    [SerializeField] private AnimationClip wallSlideAnimation;
    //[SerializeField] private AnimationClip idleAnimation;
    [SerializeField] private PlayerController _input;    
    [SerializeField] private float moveSpeed;
    private Vector2 moveInput;

    public GameEvent onWallDetection;

    [SerializeField] private SpriteRenderer playerSprite;
    private Vector3 originalPlayerSpriteScale;

    [SerializeField] private StudioEventEmitter wallSlideSoundEmitter;

    public override void Enter()
    {
        _input.onWall = true;

        animator.SetBool("OnWall", true);       
        animator.Play(wallSlideAnimation.name);

        onWallDetection.Raise(this, true);        
       
        if(_input.groundSensor.groundNormal.x > 0)
        {
            playerSprite.transform.localScale = new Vector3(1, 1, 1);
            _input.lookingRight = false;
        }
        if(_input.groundSensor.groundNormal.x < 0)
        {
            playerSprite.transform.localScale = new Vector3(-1, 1, 1);      
            _input.lookingRight = true;
        }

        wallSlideSoundEmitter.Play();
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
        _input.onWall = false;

        onWallDetection.Raise(this, false);

        animator.SetBool("OnWall", false);

        wallSlideSoundEmitter.Stop();
        //animator.Play(idleAnimation.name);       

        //playerSprite.localScale = originalPlayerSpriteScale;
    }
}
