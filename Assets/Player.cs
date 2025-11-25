using UnityEngine;

public class Player : MonoBehaviour
{

    public Animator anim { get; private set; }

    private PlayerInputSet input;
    public StateMachine playerStateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }

    public Vector2 moveInput {  get; private set; }

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        playerStateMachine = new StateMachine();

        input = new PlayerInputSet();

        idleState = new PlayerIdleState(this, playerStateMachine, "idle");
        moveState = new PlayerMoveState(this, playerStateMachine, "move");
    }

    private void OnEnable()
    {
        input.Enable();

        //input.Player.Movement.started quick 
        //input.Player.Movement.performed continous
        //input.Player.Movement.canceled when key released to cancel input

        input.Player.Movement.performed += context => moveInput = context.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => moveInput = Vector2.zero;

    }

    private void OnDisable()
    {
        input.Disable();
    }

    public void Start()
    {
        playerStateMachine.Initialize(idleState);
    }

    public void Update()
    {
        playerStateMachine.UpdateActiveState();
    }
}
