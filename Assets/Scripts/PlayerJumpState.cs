public class PlayerJumpState : PlayerAiredState
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

        //only transfer to fall state if not in jumpAttackState
        if (isFalling() && entityStateMachine.currentState != player.jumpAttackState)
            entityStateMachine.ChangeState(player.fallState);

    }

    private bool isFalling()
    {
        return rb.linearVelocity.y < 0;
    }
}
