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

    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerDashState dashState { get; private set; }



    [Header("Movement Details")]
    public float moveSpeed = 8f;
    public float jumpForce = 5f;
    [Range(0, 1)]
    public float inAirDamper = 0.7f;
    [Range(0, 1)]
    public float wallSlideDamper = 0.3f;
    public bool isFacingRight = true;
    public Vector2 moveInput {  get; private set; }
    public Vector2 wallJumpForce;

    [Space]
    public float dashDuration = 0.25f;
    public float dashSpeed = 20;

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
        wallJumpState = new PlayerWallJumpState(this, playerStateMachine, "jumpFall");
        dashState = new PlayerDashState(this, playerStateMachine, "dash");
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
        isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * FacingRightMultiplier(), wallCheckDistance, whatIsGround);
    }

    public int FacingRightMultiplier()
    {
        return isFacingRight ? 1 : -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance,0));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(wallCheckDistance * FacingRightMultiplier(), 0));
    }
}
