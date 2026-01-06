using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    [SerializeField] private AnimationClip idleAnimation;
    public override void Enter()
    {
        if(idleAnimation != null)
            animator.Play(idleAnimation.name);
    }
    public override void Do()
    {
        
    }

    public override void FixedDo() 
    {
        body.velocity = Vector2.zero;
    }
    public override void Exit() { }
}
