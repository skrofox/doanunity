using UnityEngine;

public class Enemy_ReaperBattleState : Enemy_BattleState
{
    private Enemy_Reaper enemyReaper;

    public Enemy_ReaperBattleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        enemyReaper = enemy as Enemy_Reaper;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemyReaper.maxBattleIdleTime;
    }

    public override void Update()
    {
        stateTimer -= Time.deltaTime;
        UpdateAnimationParameters();

        if (stateTimer < 0)
            stateMachine.ChangeState(enemyReaper.reaperTeleportState);

        // Ensure player reference is set - use enemy.player as fallback, or try to detect player
        if (player == null)
        {
            player = enemy.player;
            if (player == null)
            {
                RaycastHit2D hit = enemy.PlayerDetected();
                if (hit.collider != null)
                    player = hit.transform;
            }
        }

        if (enemy.PlayerDetected())
            UpdateTargetIfNeeded();

        if (WithinAttackRange() && enemy.PlayerDetected() && CanAttack())
        {
            lastTimeAttacked = Time.time;
            stateMachine.ChangeState(enemyReaper.reaperAttackState);
        }
        else
        {
            float xVeloicty = enemy.canChasePlayer ? enemy.GetBattleMoveSpeed() : 0.0001f;

            if (enemy.groundDetected == false)
                xVeloicty = 0.00001f;

            enemy.SetVelocity(xVeloicty * DirectionToPlayer(), rb.linearVelocity.y);
        }
    }

}
