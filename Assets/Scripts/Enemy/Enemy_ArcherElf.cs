using UnityEngine;

public class Enemy_ArcherElf : Enemy
{
    public bool CanBeCounter { get => canBeStunned; }
    public Enemy_ArcherElfBattleState archerElfBattleState { get; set; }

    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_IdleState(this, stateMachine, "idle");
        moveState = new Enemy_MoveState(this, stateMachine, "move");
        attackState = new Enemy_AttackState(this, stateMachine, "attack");
        deadState = new Enemy_DeadState(this, stateMachine, "idle");
        stunnedState = new Enemy_StunnedState(this, stateMachine, "stunned");
        

        archerElfBattleState = new Enemy_ArcherElfBattleState(this, stateMachine, "battle");
        battleState = archerElfBattleState;
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    public void HandleCouter()
    {
        if (!CanBeCounter)
            return;

        stateMachine.ChangeState(stunnedState);
    }
}
