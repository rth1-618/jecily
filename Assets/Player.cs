using UnityEngine;

public class Player : MonoBehaviour
{
    public StateMachine playerStateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }

    private void Awake()
    {
        playerStateMachine = new StateMachine();

        idleState = new PlayerIdleState(this, playerStateMachine, "idle");
        moveState = new PlayerMoveState(this, playerStateMachine, "move");
    }

    public void Start()
    {
        playerStateMachine.Initialize(idleState);
    }

    public void Update()
    {
        playerStateMachine.currentState.Update();
    }
}
