using UnityEngine;

public class PlayerWallSlideState : EntityState
{
    public PlayerWallSlideState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    public override void Update()
    {
        base.Update();
        HandleSlide();

        if (!player.isWallDetected)
            entityStateMachine.ChangeState(player.fallState);

        if (player.isGroundDetected)
        {
            entityStateMachine.ChangeState(player.idleState);
            player.Flip();
        }

    }

    private void HandleSlide()
    {
        if (player.moveInput.y < 0)
            player.SetVelocity(player.moveInput.x, rb.linearVelocity.y);
        else
            player.SetVelocity(player.moveInput.x, rb.linearVelocity.y * player.wallSlideDamper);
    }
}
