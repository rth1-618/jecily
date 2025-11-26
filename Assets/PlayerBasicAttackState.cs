using UnityEngine;

public class PlayerBasicAttackState : EntityState
{
    private float attackVelocityTimer;
    public PlayerBasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        GenerateAttackVelocity();
    }
    public override void Update()
    {
        base.Update();
        HandlePlayerVelocity();

        if (isTriggerCalled)
            entityStateMachine.ChangeState(player.idleState);
    }
    private void HandlePlayerVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;

        if(attackVelocityTimer < 0 ) 
            player.SetVelocity(0,rb.linearVelocity.y);
    }

    public void GenerateAttackVelocity()
    {
        attackVelocityTimer = player.attackVelocityDuration;
        player.SetVelocity(player.attackVelocity.x * player.FacingRightMultiplier(), player.attackVelocity.y);
    }

}
