using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Topo : Core
{
    [SerializeField] private State startState;
    public Vector3 startPosition { get; private set; }

    [SerializeField] private State diggingState;
    [SerializeField] private State parabolicMoveState;

    private void Awake()
    {
        startPosition = gameObject.transform.localPosition;
    }
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
        if (state.isComplete)
        {
            if (state == diggingState)
            {
                Set(parabolicMoveState);
            }
            if (state == parabolicMoveState)
            {
                Set(diggingState);
            }
        }
    }
    private void FixedUpdate()
    {
        state.FixedDo();
    }
}
