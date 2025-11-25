using Unity.VisualScripting;
using UnityEngine;

public class PlayerGroundedState : EntityState
{
    public PlayerGroundedState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (rb.linearVelocity.y < 0)
            entityStateMachine.ChangeState(player.fallState);

        if (input.Player.Jump.WasPressedThisFrame())
            entityStateMachine.ChangeState(player.jumpState);
    }
}
