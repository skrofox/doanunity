using UnityEngine;

public class Player_GroundedState : EntityState
{
    public Player_GroundedState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        // if(input.Player.Jump.WasCompletedThisFrame()) //thả nút nhảy => nhảy
        if (input.Player.Jump.WasPressedThisFrame()) //nhấn nút nhảy => nhảy
        {
            stateMachine.ChangeState(player.jumpState);
        }

        if (rb.linearVelocity.y < 0)
        {
            stateMachine.ChangeState(player.fallState);
        }
    }
}
