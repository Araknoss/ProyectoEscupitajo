using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DiggingState : State
{
    [SerializeField] private AnimationClip diggingAnimation;

    [SerializeField] private float maxDiggingTime = 1;
    [SerializeField] private float minDiggingTime = 0f;
    [SerializeField] private float reactionTreshold = 0.3f;
    private float reactionTime;
    private float diggingTime;
    private bool canDig;

    [SerializeField] private State nextState;

    public override void Enter()
    {
        core.actualState = this;
        if (diggingAnimation != null)
        {  
            animator.Play(diggingAnimation.name); 
        }        

        canDig = false;
    }
    public override void Do()
    {
        if (time >= reactionTime && canDig)
        {
            core.GetComponent<SpriteRenderer>().color = Color.red;
            //Meter cambio de animacion
        }
        if(time >= diggingTime && canDig)
        {
            isComplete = true;
            Set(nextState, true);
            
            Debug.Log("Digging complete on: " +diggingTime);
        }
    }

    public override void FixedDo()
    {

    }
    public override void Exit() 
    {
        canDig = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GameView"))
        {
            canDig = true;
            diggingTime = time + Random.Range(minDiggingTime, maxDiggingTime);
            reactionTime = diggingTime - reactionTreshold;
        }
    }
}
