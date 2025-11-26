using UnityEngine;

public class PlayerDashState : EntityState
{
    private float originalGravityScale;
    private int dashDirection;
    public PlayerDashState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashDuration;
        originalGravityScale = rb.gravityScale;
        rb.gravityScale = 0;
        dashDirection = player.moveInput.x != 0 ? (int)player.moveInput.x : player.FacingRightMultiplier();
    }

    public override void Update()
    {
        base.Update();

        cancelDashIfNeeded();
        player.SetVelocity(player.dashSpeed * dashDirection, 0);

        if (stateTimer < 0)
        {
            if(player.isGroundDetected)
                entityStateMachine.ChangeState(player.idleState);
            else
                entityStateMachine.ChangeState(player.fallState);
        }
    }
    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, 0);
        rb.gravityScale = originalGravityScale;
    }

    private void cancelDashIfNeeded()
    {
        if (player.isWallDetected)
        {
            if (player.isGroundDetected)
                entityStateMachine.ChangeState(player.idleState);
            if (player.isWallDetected)
                entityStateMachine.ChangeState(player.wallSlideState);
        }
    }
}
