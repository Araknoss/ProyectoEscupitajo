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
    private bool canDetect = false;

    [Header("States")]
    [SerializeField] private ChargedAttackState chargedAttackState;
    public override void Enter()
    {
        animator.Play(animationClip.name);
        detectionCollider.enabled = false;
    }
    public override void Do()
    {
        if (time >= 0.2f)
        {
            detectionCollider.enabled = true;
        }
        if (Detected())
        {
           Set(chargedAttackState);
        }
    }    
    public override void Exit()
    {
        detectionCollider.enabled = false;
    }

    private bool Detected()
    {
        return detectionCollider.IsTouchingLayers(detectionLayer);
    }
}
