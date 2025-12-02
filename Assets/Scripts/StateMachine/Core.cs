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
    }

    public void SetupInstances()
    {
        machine = new StateMachine();
        State[] allChildStates = GetComponentsInChildren<State>();
        foreach(State state in allChildStates)
        {
            state.SetCore(this,machine);            
        }
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (Application.isPlaying && state!=null)
        {
            List<State> activeStates = machine.GetActiveStateBranch();
            UnityEditor.Handles.Label(transform.position, "Active States: " +string.Join(" > ",activeStates));
        }
#endif
    }
}
