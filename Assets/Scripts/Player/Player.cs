using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerInputSet input;
    private StateMachine stateMachine;

    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    private void Awake()
    {
        stateMachine = new StateMachine();
        input = new PlayerInputSet();

        idleState = new Player_IdleState(this, stateMachine, "idle");
        moveState = new Player_MoveState(this, stateMachine, "move");

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateMachine.Initialize(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState.Update();
    }
}
