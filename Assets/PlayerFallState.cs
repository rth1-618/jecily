using UnityEngine;

public class PlayerFallState : PlayerAiredState
{
    public PlayerFallState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        // detect ground, if ground transistion to idle state
        if (player.isGroundDetected)
            entityStateMachine.ChangeState(player.idleState);
    }
}
