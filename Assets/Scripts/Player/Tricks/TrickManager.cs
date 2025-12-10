using System;
using System.Collections.Generic;
using UnityEngine;

public class TrickManager : MonoBehaviour
{
    [SerializeField] private List<Trick> tricksPerformed = new List<Trick>();

    [SerializeField] private KeyCode bodyKey= KeyCode.J;
    [Header("Variables")]
    [SerializeField] private float trickCooldownTime=0.2f;
    private float trickCooldownTimer;

    [Header("Tricks")]
    [SerializeField] private Trick horizontalFlip;
    [SerializeField] private Trick verticalFlip;

    [Header("Events")]
    public GameEvent onTrickPerformed;
    private void Update()
    {
        HandleInput();
        HandleTrickCooldown();
    }
    void HandleInput()
    {
        if (Input.GetKeyDown(bodyKey))
        {
            HandleBodyInput();
        }
    }
    void HandleTrickCooldown()
    {
        if (trickCooldownTimer > 0f)
        {
            trickCooldownTimer -= Time.deltaTime;
        }
    }

    void HandleBodyInput()
    {
        PerformTrick(horizontalFlip);
    }

    void PerformTrick(Trick trick)
    {
        if(trickCooldownTimer > 0f) return;
        onTrickPerformed.Raise(this, trick);
        tricksPerformed.Add(trick);
        trickCooldownTimer = trickCooldownTime;
    }
    
}
