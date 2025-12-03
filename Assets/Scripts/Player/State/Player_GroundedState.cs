using UnityEngine;

public class Player_GroundedState : EntityState
{
    public Player_GroundedState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        if (rb.linearVelocity.y < 0 && player.groundDetected == false)
        {
            stateMachine.ChangeState(player.fallState);
        }

        // if(input.Player.Jump.WasCompletedThisFrame()) //thả nút nhảy => nhảy
        if (input.Player.Jump.WasPerformedThisFrame()) //nhấn nút nhảy => nhảy
        {
            stateMachine.ChangeState(player.jumpState);
        }
        if (input.Player.Attack.WasPerformedThisFrame())
        {
            stateMachine.ChangeState(player.basicAttackState);
        }


    }
}
