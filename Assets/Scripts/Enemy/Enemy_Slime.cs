using UnityEngine;

public class Enemy_Slime : Enemy, ICouterable
{
    public bool CanBeCounter { get => canBeStunned; }
    public Enemy_SlimeDeadState slimeDeadState { get; set; }

    [Header("Slime Specifics")]
    [SerializeField] private GameObject slimeToCreatePrefab;
    [SerializeField] private int amountSlimesToCreate = 2;
    [SerializeField] private Vector2 newSlimeVelocity;

    [SerializeField] private bool hasRecoveryAnimation = true;

    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_IdleState(this, stateMachine, "idle");
        moveState = new Enemy_MoveState(this, stateMachine, "move");
        attackState = new Enemy_AttackState(this, stateMachine, "attack");
        battleState = new Enemy_BattleState(this, stateMachine, "battle");
        stunnedState = new Enemy_StunnedState(this, stateMachine, "stunned");
        slimeDeadState = new Enemy_SlimeDeadState(this, stateMachine, "idle");

        anim.SetBool("hasStunnedRecovery", hasRecoveryAnimation);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    public override void EntityDeath()
    {
        stateMachine.ChangeState(slimeDeadState);
    }

    public void HandleCouter()
    {
        if (!CanBeCounter)
            return;

        stateMachine.ChangeState(stunnedState);
    }

    public void CreateSlimeOnDeath()
    {
        if (slimeToCreatePrefab == null)
            return;

        for (int i = 0; i < amountSlimesToCreate; i++)
        {
            GameObject newSlime = Instantiate(slimeToCreatePrefab, transform.position, Quaternion.identity);
            Enemy_Slime slimeScript = newSlime.GetComponent<Enemy_Slime>();

            slimeScript.stats.AdjustStatsSetup(stats.resources, stats.offense, stats.defence, .6f, 1.2f);
            slimeScript.ApplyRespawnVelocity();
            slimeScript.StartBattleStateCheck(player);
        }

    }

    public void ApplyRespawnVelocity()
    {
        Vector2 velocity = new Vector2 (stunnedVeclocity.x * Random.Range(-1f,1f), stunnedVeclocity.y * Random.Range(1f,2f));

        SetVelocity(velocity.x, velocity.y);
    }

    public void StartBattleStateCheck(Transform player)
    {
        TryEnterBattleState(player); 
        InvokeRepeating(nameof(ReEnterBattleState), 0, .3f);
    }

    private void ReEnterBattleState()
    {
        if (stateMachine.currentState == battleState || stateMachine.currentState == attackState)
        {
            CancelInvoke(nameof(ReEnterBattleState));
            return;
        }

        stateMachine.ChangeState(battleState);
    }
}
