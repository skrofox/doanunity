using UnityEngine;

public class Enemy_BattleState : EnemyState
{
    private Transform player; //player vector position
    private float lastTimeWasInBattle;
    public Enemy_BattleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (player == null)
        {
            player = enemy.playerDetected().transform;
        }

        if (ShouldRetreat())
        {
            rb.linearVelocity = new Vector2(enemy.retreatVelocity.x * -DirectionToPlayer(), enemy.retreatVelocity.y);
            enemy.HandleFlip(DirectionToPlayer());
        }
    }

    public override void Update()
    {
        base.Update();

        if(enemy.playerDetected() == true)
            UpdateBattleTimer();

        if (BattleTimeIsOver())
            stateMachine.ChangeState(enemy.idleState);

        if (WithinAttackRange() && enemy.playerDetected())
        {
            stateMachine.ChangeState(enemy.attackState);
        } else
        {
            enemy.SetVelocity(enemy.battleMoveSpeed * DirectionToPlayer(), rb.linearVelocity.y);
        }
    }

    private void UpdateBattleTimer() => lastTimeWasInBattle = Time.time;
    private bool BattleTimeIsOver() => Time.time > lastTimeWasInBattle + enemy.battleTimeDuration;

    private bool WithinAttackRange()
    {
        //khoang cach den nguoi choi < khoang cach tan cong cua enemy => tra ve true, nguoc lai tra ve false
        return DistanceToPlayer() < enemy.attackDistance;
    }

    private bool ShouldRetreat() => DistanceToPlayer() < enemy.minRetreatDistance;

    //khoang cach den nguoi choi
    private float DistanceToPlayer()
    {
        if(player == null)
        {
            return float.MaxValue;
        }

        //tra ve khoang cach (gia tri) tuyet doi, 
        return Mathf.Abs(player.position.x - enemy.transform.position.x);
    }

    private int DirectionToPlayer()
    {
        if(player == null)
        {
            return 0;
        }
        return player.position.x > enemy.transform.position.x ? 1 : -1;
    }

}
