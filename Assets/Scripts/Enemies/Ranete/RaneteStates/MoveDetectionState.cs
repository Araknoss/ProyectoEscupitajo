using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MoveDetectionState : State
{
    [SerializeField] private AnimationClip animationClip;
    [SerializeField] private float movementSpeed;
    [SerializeField] private Vector3 direction;

    [SerializeField] private CircleCollider2D detectionCollider;
    [SerializeField] private LayerMask detectionLayer;

    [Header("States")]
    [SerializeField] private ExplodeState explodeState;
    public override void Enter()
    {
        animator.Play(animationClip.name);      
    }
    public override void Do()
    {
        core.gameObject.transform.position += movementSpeed * Time.deltaTime * direction;
        if (CloseEnough()) 
        {
            Set(explodeState);
        }
    }
    public override void FixedDo()
    {
        
    }
    public override void Exit() { }

    private bool CloseEnough()
    {
        return detectionCollider.IsTouchingLayers(detectionLayer);        
    }
}
