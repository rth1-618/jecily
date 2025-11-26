using UnityEngine;

public class PlayerGroundedState : EntityState
{
    public PlayerGroundedState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (rb.linearVelocity.y < 0 && !player.isGroundDetected)
            entityStateMachine.ChangeState(player.fallState);

        if (input.Player.Jump.WasPressedThisFrame())
            entityStateMachine.ChangeState(player.jumpState);

        if (input.Player.Attack.WasPressedThisFrame())
            entityStateMachine.ChangeState(player.basicAttackState);
    }
}
