using UnityEngine;

public class Enemy : Entity
{
    public Enemy_IdleState idleState;
    public Enemy_MoveState moveState;
    public Enemy_AttackState attackState;
    public Enemy_BattleState battleState;

    [Header("Battle Details")]
    public float battleMoveSpeed = 2.8f;//nhanh gap 2
    public float attackDistance = 2;//khoang cach tan cong
    public float battleTimeDuration = 5;//thoi gian trong trang thai battle khi khong phat hien nguoi choi nua
    public float minRetreatDistance = 5;//Nguoi choi dung qua gan thi enemy lui lai 1 chut
    public Vector2 retreatVelocity;

    [Header("Movement Details")]
    public float idleTime = 1.5f;
    public float moveSpeed = 1.4f;
    [Range(0, 2)]
    public float moveAnimSpeedMultiplier = 1;

    [Header("Player Detection")]
    [SerializeField] private LayerMask WhatIsPlayer;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private float playerCheckDistance = 10f;


    protected override void Update()
    {
        base.Update();

    }

    public RaycastHit2D playerDetected()
    {
        RaycastHit2D hit = Physics2D.Raycast(playerCheck.position, Vector2.right * facingDir, playerCheckDistance, WhatIsPlayer | whatIsGround);

        if (hit.collider == null || hit.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
            return default;
            //return default is.
            //hit.collider = null;
            //hit.point = 0,0;
            //hit.distance = 0;

        return hit;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(playerCheck.position.x + (facingDir * playerCheckDistance), playerCheck.position.y));
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, new Vector3(playerCheck.position.x + (facingDir * minRetreatDistance), playerCheck.position.y));
        
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, new Vector3(playerCheck.position.x + (facingDir * attackDistance), playerCheck.position.y));


    }

}
