using UnityEngine;

public class PlayerJumpState : EntityState
{
    public PlayerJumpState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(rb.linearVelocity.x, player.jumpForce);
    }

    public override void Update()
    {
        base.Update();

        if (isFalling())
            entityStateMachine.ChangeState(player.fallState);

    }

    private bool isFalling()
    {
        return rb.linearVelocity.y < 0;
    }
}
