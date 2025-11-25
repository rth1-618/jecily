using UnityEngine;

public class PlayerFallState : EntityState
{
    public PlayerFallState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        // detect ground, if ground transistion to idle state
    }
}
