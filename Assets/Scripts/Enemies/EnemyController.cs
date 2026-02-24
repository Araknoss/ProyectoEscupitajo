using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class EnemyController : Core
{
    [SerializeField] private State startState;
    public Vector3 startPosition { get; private set; }

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
    }    
    private void FixedUpdate()
    {
        state.FixedDo();
    }
}
