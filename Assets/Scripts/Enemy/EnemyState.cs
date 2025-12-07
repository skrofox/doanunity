using UnityEngine;

public class EnemyState : EntityState
{
    protected Enemy enemy;

    public EnemyState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.enemy = enemy;
        rb = enemy.rb;
        anim = enemy.anim;
    }


    public override void UpdateAnimationParameters()
    {
        base.UpdateAnimationParameters();

        float battleAnimSpeedMultiplier = enemy.battleMoveSpeed / enemy.moveSpeed;
        //skeleton di chuyen, animations nhanh hon, khong truot tren mat dat
        anim.SetFloat("battleAnimSpeedMultiplier", battleAnimSpeedMultiplier);
        //toc do di chuyen tang, animation speed tang.
        anim.SetFloat("moveAnimSpeedMultiplier", enemy.moveAnimSpeedMultiplier);
        anim.SetFloat("xVelocity", rb.linearVelocity.y);
    }
}
