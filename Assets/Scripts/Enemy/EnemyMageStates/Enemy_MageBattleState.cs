using UnityEngine;

public class Enemy_MageBattleState : Enemy_BattleState
{
    private Enemy_Mage enemyMage;
    private float lastTimeUseRetreat = float.NegativeInfinity;

    public Enemy_MageBattleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        enemyMage = enemy as Enemy_Mage;
    }

    public override void Enter()
    {
        base.Enter();

        if (ShouldRetreat())
        {
            if (CanUseRetreatAbility())
                Retreat();
            else
                ShortTreat();
        }
    }

    private void Retreat()
    {
        lastTimeUseRetreat = Time.time;
        stateMachine.ChangeState(enemyMage.mageRetreatState);
    }

    private bool CanUseRetreatAbility() => Time.time > lastTimeUseRetreat + enemyMage.retreatCooldown;
}
