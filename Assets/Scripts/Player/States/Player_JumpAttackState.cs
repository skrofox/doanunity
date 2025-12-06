using UnityEngine;

public class Player_JumpAttackState : PlayerState
{
    private bool touchedGround;
    private Vector2 jumpAttackVelocity;
    public Player_JumpAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        jumpAttackVelocity = player.jumpAttackVelocity;

        touchedGround = false;

        player.SetVelocity(jumpAttackVelocity.x * player.facingDir, jumpAttackVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (player.groundDetected && !touchedGround)
        {
            touchedGround = true;
            anim.SetTrigger("jumpAttackTrigger");
            player.SetVelocity(0, rb.linearVelocity.y);
        }
        if (triggerCalled && player.groundDetected)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
