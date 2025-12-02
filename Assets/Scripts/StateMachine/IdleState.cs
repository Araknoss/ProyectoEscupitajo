using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    [SerializeField] private AnimationClip idleAnimation;
    public override void Enter()
    {
        animator.Play(idleAnimation.name);
    }
    public override void Do()
    {
        
    }

    public override void FixedDo() { }
    public override void Exit() { }
}
