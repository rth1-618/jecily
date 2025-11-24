using UnityEngine;

public class Player : MonoBehaviour
{
    public StateMachine playerStateMachine { get; private set; }
    protected EntityState idleState;

    private void Awake()
    {
        playerStateMachine = new StateMachine();

        idleState = new EntityState(playerStateMachine, "Idle State");
    }

    void Start()
    {
        playerStateMachine.Initialize(idleState);
    }

    void Update()
    {
        playerStateMachine.currentState.Update();
    }
}
