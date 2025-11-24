using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine entityStateMachine;
    protected string stateName;

    public EntityState(Player player, StateMachine stateMachine, string stateName)
    {
        this.player = player;
        this.entityStateMachine = stateMachine;
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
