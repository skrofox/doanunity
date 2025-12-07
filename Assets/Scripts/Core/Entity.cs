using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim { get; private set; }
    protected StateMachine stateMachine;


    [Header("Collision Detection")]
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] private float _groundedCheckDistance;
    [SerializeField] private float _wallCheckDistance;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform primatyWallCheck;
    [SerializeField] private Transform secondaryWallCheck;

    // Facing Direction
    private bool facingRight = true;
    public int facingDir { get; private set; } = 1; // 1 is right, -1 is left


    public bool groundDetected { get; private set; }
    public bool wallDetected { get; private set; }

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleCollisionDetection();
        stateMachine.UpdateActiveState();
    }


    public void CallAnimationTrigger()
    {
        stateMachine.currentState.CallAnimationTrigger();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }

    private void HandleFlip(float xVelocity)
    {
        if (xVelocity > 0 && !facingRight)
        {
            Flip();
        }
        else if (xVelocity < 0 && facingRight)
        {
            Flip();
        }
    }

    public void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
        facingDir *= -1;
    }

    private void HandleCollisionDetection()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, _groundedCheckDistance, whatIsGround);
        if (secondaryWallCheck != null)
        {
            wallDetected = Physics2D.Raycast(primatyWallCheck.position, Vector2.right * facingDir, _wallCheckDistance, whatIsGround)
                && Physics2D.Raycast(secondaryWallCheck.position, Vector2.right * facingDir, _wallCheckDistance, whatIsGround);
        }
        else
        {
            wallDetected = Physics2D.Raycast(primatyWallCheck.position, Vector2.right * facingDir, _wallCheckDistance, whatIsGround);
        }

    }

    protected virtual void OnDrawGizmos()
    {
        //nhu nhau, same way
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + (Vector3.down * _groundedCheckDistance));
        Gizmos.DrawLine(primatyWallCheck.position, primatyWallCheck.position + new Vector3(facingDir * _wallCheckDistance, 0));
        
        if (secondaryWallCheck != null)
            Gizmos.DrawLine(secondaryWallCheck.position, secondaryWallCheck.position + new Vector3(facingDir * _wallCheckDistance, 0));
    }
}
