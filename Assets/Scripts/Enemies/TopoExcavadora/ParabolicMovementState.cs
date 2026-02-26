using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ParabolicMovementStaste : State
{
    [SerializeField] private AnimationClip animationClip;
    
    public Transform parent1;
    public Vector2 originalPosition;

    [SerializeField] Vector3 direction;
    [SerializeField] private float movementSpeed;

    [SerializeField] private State nextState;

    public override void Enter()
    {
        core.actualState = this;
        if (animationClip != null)
        {
            animator.Play(animationClip.name);
        }            
        parent1 = core.gameObject.transform.parent;
        originalPosition = core.gameObject.transform.localPosition;
        core.gameObject.transform.SetParent(null);
    }
    public override void Do()
    {
        core.gameObject.transform.position += movementSpeed * Time.deltaTime * direction;
        if (time >= 2f)
        {
            isComplete = true;
            Set(nextState, true);
        }
    }
    public override void Exit()
    {
        core.gameObject.transform.SetParent(parent1);        
        core.gameObject.transform.localPosition = originalPosition;
    }  
}
