using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuloIdleState : State
{
    [SerializeField] private AnimationClip animationClip;
    [SerializeField] private Pooler _pooler;
   
    public override void Enter()
    {
        animator.Play(animationClip.name);
        StartCoroutine(FartOnTimeIntervalCo());
    }
    public override void Do()
    {
      
    }
    public override void Exit()
    {
        StopCoroutine(FartOnTimeIntervalCo());
    }    

    private void Fart()
    {
        _pooler.GetPooledObject();
    }
    IEnumerator FartOnTimeIntervalCo()
    {
        while(true) 
        {
            yield return new WaitForSeconds(3f);
            Fart();
        }
    }
}
