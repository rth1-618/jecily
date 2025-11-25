using UnityEngine;

public class Player : MonoBehaviour
{

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public PlayerInputSet input { get; private set; }
    public StateMachine playerStateMachine { get; private set; }


    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerFallState fallState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }



    [Header("Movement Details")]
    public float moveSpeed = 8f;
    public float jumpForce = 5f;
    [Range(0, 1)]
    public float inAirDamper = 0.7f;
    [Range(0, 1)]
    public float wallSlideDamper = 0.3f;
    private bool isFacingRight = true;
    public Vector2 moveInput {  get; private set; }

    [Header("Collision Detect")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    public bool isGroundDetected { get; private set; }
    public bool isWallDetected { get; private set; }

    private void Awake()
    {
        //GetComponents BEFORE setting states for them to be accessible inside State
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();


        playerStateMachine = new StateMachine();

        input = new PlayerInputSet();

        idleState = new PlayerIdleState(this, playerStateMachine, "idle");
        moveState = new PlayerMoveState(this, playerStateMachine, "move");
        jumpState = new PlayerJumpState(this, playerStateMachine, "jumpFall");
        fallState = new PlayerFallState(this, playerStateMachine, "jumpFall");
        wallSlideState = new PlayerWallSlideState(this, playerStateMachine, "wallSlide");
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
        HandleCollisionDetection();
        playerStateMachine.UpdateActiveState();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip();
    }

    private bool isMovingRight()
    {
        if (rb.linearVelocity.x == 0)
            return isFacingRight;
        return rb.linearVelocity.x > 0;
    }
    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
    }

    public void HandleFlip()
    { 
        if (isMovingRight() && !isFacingRight)
            Flip();
        else if (!isMovingRight() && isFacingRight)
            Flip();
    }

    private void HandleCollisionDetection()
    {
        isGroundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * (isFacingRight ? 1 : -1), wallCheckDistance, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance,0));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(wallCheckDistance * (isFacingRight ? 1 : -1), 0));
    }
}
