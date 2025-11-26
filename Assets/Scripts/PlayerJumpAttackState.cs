using UnityEngine;

public class PlayerJumpAttackState : EntityState
{
    private bool isGroundTouched;
    public PlayerJumpAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        isGroundTouched = false;
        player.SetVelocity(player.jumpAttackVelocity.x * player.FacingRightMultiplier(), player.jumpAttackVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (player.isGroundDetected && !isGroundTouched)
        {
            isGroundTouched = true;
            anim.SetTrigger("jumpAttackTrigger");
            player.SetVelocity(0,rb.linearVelocity.y);
        }

        if(isTriggerCalled && player.isGroundDetected)
            entityStateMachine.ChangeState(player.idleState);
    }
}
