using UnityEngine;

public class Player_FallState : Player_AiredState
{
    public Player_FallState(Player player, StateMachine stateMachine, string animBoolName) : base (player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        //neu player detecting the ground bellow, if yes. go to the idle state
        if (player.groundDetected)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
