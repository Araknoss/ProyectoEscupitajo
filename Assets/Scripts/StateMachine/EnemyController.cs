using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class EnemyController : Core
{
    [Header("States")]
    [SerializeField] private State startState;
    [SerializeField] private EnemyMoveState constantMoveState;      

    private void Start()
    {
        SetupInstances();
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
