using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Core : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D body;    
    public GroundSensor groundSensor;

    protected StateMachine machine;
    protected State state => machine.state;
    public State actualState;

    protected void Set(State newState, bool forceReset = false)
    {
        machine.Set(newState, forceReset);
        actualState = newState;
    }
    public void SetupStates()
    {
        machine = new StateMachine();
        
        State[] allChildStates = GetComponentsInChildren<State>();
        foreach(State state in allChildStates)
        {
            state.SetCore(this,machine);
            Debug.Log(state);
        }
    }
}
