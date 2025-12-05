using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DetectionState : State
{
    [SerializeField] private AnimationClip animationClip;
    [SerializeField] private AnimationClip detectedClip;

    [SerializeField] private BoxCollider2D detectionCollider;
    [SerializeField] private LayerMask detectionLayer;

    [Header("States")]
    [SerializeField] private ChargedAttackState chargedAttackState;
    public override void Enter()
    {
        animator.Play(animationClip.name);
    }
    public override void Do()
    {        
        if (Detected())
        {
           Set(chargedAttackState);

        }
    }    
    public override void Exit() { }

    private bool Detected()
    {
        return detectionCollider.IsTouchingLayers(detectionLayer);
    }
}
