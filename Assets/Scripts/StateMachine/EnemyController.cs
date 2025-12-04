using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyController : Core
{
    [SerializeField] private State startState;

    [Header("States")]    
    [SerializeField] private EnemyMoveState constantMoveState;
    [SerializeField] private MoveDetectionState moveDetectionState;
    [SerializeField] private ExplodeState explodeState;

    private void OnEnable()
    {
        SetupInstances();
        Set(startState);
    }
    private void Update()
    {
        state.Do();

        if (state.isComplete)
        {
            if(state==explodeState)
            {
                actualState=moveDetectionState;
                Debug.Log("Enemy exploded");
                gameObject.SetActive(false);
                
            }
        }
    }    
    private void FixedUpdate()
    {
        state.FixedDo();
    }
}
