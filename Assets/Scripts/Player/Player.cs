using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim { get; private set; }
    public Rigidbody2D rb;

    public PlayerInputSet input { get; private set; }
    private StateMachine stateMachine;

    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }

    public Vector2 moveInput { get; private set; }



    //
    private bool _isFacingRight = true;
    private int _facingDir = 1; // 1 is right, 2 is left

    [Header("Movement Details")]
    public float moveSpeed;
    public float jumpForce;
    [Range(0, 1)]
    public float inAirMoveMultipler = 0.8f;

    [Header("Collision Detection")]
    [SerializeField] private float _groundedCheckDistance;
    [SerializeField] private float _wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    public bool groundDetected { get; private set; }
    public bool wallDetected { get; private set; }

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine();
        input = new PlayerInputSet();

        idleState = new Player_IdleState(this, stateMachine, "idle");
        moveState = new Player_MoveState(this, stateMachine, "move");
        jumpState = new Player_JumpState(this, stateMachine, "jumpFall");
        fallState = new Player_FallState(this, stateMachine, "jumpFall");

    }

    private void OnEnable()
    {
        input.Enable();

        input.Player.Movement.performed += context => moveInput = context.ReadValue<Vector2>();
        input.Player.Movement.canceled += context => moveInput = Vector2.zero;


    }


    private void OnDisable()
    {
        input.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateMachine.Initialize(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        HandleCollisionDetection();
        stateMachine.UpdateActiveState();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }

    private void HandleFlip(float xVelocity)
    {
        if (xVelocity > 0 && !_isFacingRight)
        {
            Flip();
        }
        else if (xVelocity < 0 && _isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0, 180, 0);
        _facingDir *= -1;
    }

    private void HandleCollisionDetection()
    {
        groundDetected = Physics2D.Raycast(transform.position, Vector2.down, _groundedCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(transform.position, Vector2.right * _facingDir, _wallCheckDistance, whatIsGround);
    }

    void OnDrawGizmos()
    {
        //nhu nhau, same way
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.down * _groundedCheckDistance));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(_facingDir * _wallCheckDistance, 0));
    }

}
