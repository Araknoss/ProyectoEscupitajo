using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
   public bool isCompleted { get; protected set; }
   protected float startTime;
   public float time => Time.time - startTime; //Returns current time when we use it, is like an implemented function

    protected Core core;
    protected Rigidbody2D body => core.body;
    protected Animator animator => core.animator;   
    protected GroundSensor groundSensor => core.groundSensor;

    public StateMachine machine;
    protected StateMachine parent;
    public State state => machine.state;
   
   public virtual void Enter() { }
   public virtual void Do() { }   
   public virtual void FixedDo() { }
   public virtual void Exit() { }


    protected void Set(State newState, bool forceReset = false)
    {
        machine.Set(newState, forceReset);
    }
    public void SetCore(Core _core, StateMachine _machine)
    {
        //machine = new StateMachine();
        machine = _machine;
        core = _core;
    }    
    public void Initialize(StateMachine _parent)
    {
        parent = _parent;
        isCompleted = false;
        startTime = Time.time;
    }

    //For hierarchical states:

    public void DoBranch()
    {
        Do();
        state?.DoBranch();
    }
    public void FixedDoBranch()
    {
        FixedDo();
        state?.FixedDoBranch();
    }
}
