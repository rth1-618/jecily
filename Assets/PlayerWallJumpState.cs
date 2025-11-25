using UnityEngine;

public class PlayerWallJumpState : EntityState
{
    public PlayerWallJumpState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(player.wallJumpForce.x * -player.FacingRightMultiplier(), player.wallJumpForce.y);
    }
    public override void Update()
    {
        base.Update();

        if (rb.linearVelocity.y < 0)
            entityStateMachine.ChangeState(player.fallState);

        if (player.isWallDetected)
            entityStateMachine.ChangeState(player.wallSlideState);
    }
}
