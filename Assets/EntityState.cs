using UnityEngine;

public class EntityState
{
    protected StateMachine stateMachine;
    protected string stateName;

    public EntityState(StateMachine stateMachine, string stateName)
    {
        this.stateMachine = stateMachine;
       this.stateName = stateName;
    }

    public virtual void Enter()
    {
        // everytime state changes, Enter() wil be called
        Debug.Log("Entered " + stateName);
    }

    public virtual void Update()
    {
        // running logic of the state
        Debug.Log("Updated " + stateName);

    }

    public virtual void Exit()
    {
        // called before changing state for cleaning up
        Debug.Log("Exiting " + stateName);

    }
}
