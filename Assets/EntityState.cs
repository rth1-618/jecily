using UnityEngine;

public abstract class EntityState
{

    protected Player player;
    protected StateMachine entityStateMachine;
    protected string animBoolName;

    protected Animator anim;

    public EntityState(Player player, StateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.entityStateMachine = stateMachine;
        this.animBoolName = animBoolName;
        anim = player.anim;
    }

    public virtual void Enter()
    {
        // everytime state changes, Enter() wil be called
        anim.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
        // running logic of the state
        Debug.Log("Updated " + animBoolName);

    }

    public virtual void Exit()
    {
        // called before changing state for cleaning up
        anim.SetBool(animBoolName, false);

    }
}
