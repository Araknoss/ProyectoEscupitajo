using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeState : State
{
    [SerializeField] private AnimationClip animationClip;    
    
    public override void Enter()
    {
        animator.Play(animationClip.name);
    }
    public override void Do()
    {
        if (time > animationClip.length)
        {
            isComplete = true;           
        }
    }

    public override void FixedDo()
    {
       
    }
    public override void Exit() 
    { 
    
    }


}
