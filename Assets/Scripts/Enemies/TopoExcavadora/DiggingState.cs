using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DiggingState : State
{
    [SerializeField] private AnimationClip diggingAnimation;

    [SerializeField] private float maxDiggingTime = 6f;
    [SerializeField] private float minDiggingTime = 3f;
    [SerializeField] private float reactionTreshold = 0.5f;
    private float reactionTime;
    private float diggingTime;

    public override void Enter()
    {
        if (diggingAnimation != null)
        {  
            animator.Play(diggingAnimation.name); 
        }

        diggingTime = Random.Range(minDiggingTime, maxDiggingTime);
        reactionTime= diggingTime - reactionTreshold;
    }
    public override void Do()
    {
        if (time >= reactionTime)
        {
            core.GetComponent<SpriteRenderer>().color = Color.red;
            //Meter cambio de animacion
        }
        if(time >= maxDiggingTime)
        {
            isComplete = true;
            
            Debug.Log("Digging complete on: " +diggingTime);
        }
    }

    public override void FixedDo()
    {

    }
    public override void Exit() 
    {

    }
}
