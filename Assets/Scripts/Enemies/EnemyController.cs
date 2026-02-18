using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyController : Core
{
    [SerializeField] private State startState;    

    private void OnEnable()
    {
        SetupInstances();
        Set(startState);
    }

    private void Start()
    {
        Set(startState);
    }
    private void Update()
    {
        state.Do();
    }    
    private void FixedUpdate()
    {
        state.FixedDo();
    }
}
