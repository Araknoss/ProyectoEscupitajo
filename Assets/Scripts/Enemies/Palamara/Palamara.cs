using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Palamara : Core
{
    [SerializeField] private State startState;

    [Header("States")]   
    [SerializeField] private DetectionState detectionState;
    [SerializeField] private ChargedAttackState chargedAttackState;

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
            if(state==chargedAttackState)
            {
                actualState = detectionState;
                Debug.Log("Enemy attack end");
                gameObject.SetActive(false);
            }
        }
    }    
    private void FixedUpdate()
    {
        state.FixedDo();
    }
}
