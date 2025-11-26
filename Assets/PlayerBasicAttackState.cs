using UnityEngine;

public class PlayerBasicAttackState : EntityState
{
    private float attackVelocityTimer;

    private const int FirstComboIndex = 1;
    private int comboLimit = 3;

    private int comboIndex = FirstComboIndex;
    private float lastTimeAttacked;

    public PlayerBasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        if(comboLimit != player.attackVelocity.Length)
        {
            Debug.LogWarning("Adjusted comboLimit to attackVelocity.Length!");
            comboLimit = player.attackVelocity.Length;
        }
    }
    public override void Enter()
    {
        base.Enter();
        ResetComboIndexIfNeeded();

        anim.SetInteger("basicAttackIndex", comboIndex);
        ApplyAttackVelocity();
    }

    

    public override void Update()
    {
        base.Update();
        HandlePlayerVelocity();

        if (isTriggerCalled)
            entityStateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();

        comboIndex++;
        //remember time last attack happened
        lastTimeAttacked = Time.time;
    }
    private void HandlePlayerVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;

        if(attackVelocityTimer < 0 ) 
            player.SetVelocity(0,rb.linearVelocity.y);
    }

    public void ApplyAttackVelocity()
    {
        Vector2 attackVelocity = player.attackVelocity[comboIndex - 1];
        attackVelocityTimer = player.attackVelocityDuration;
        player.SetVelocity(attackVelocity.x * player.FacingRightMultiplier(), attackVelocity.y);
    }

    private void ResetComboIndexIfNeeded()
    {
        //if last attack was a while ago, reset combo to startindex
        if (Time.time > lastTimeAttacked + player.comboResetTime)
            comboIndex = FirstComboIndex;

        if (comboIndex > comboLimit)
            comboIndex = FirstComboIndex;
    }

}
