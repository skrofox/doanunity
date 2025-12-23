using System;
using System.Collections;
using UnityEngine;

public class Skill_Shard : Skill_Base
{
    private SkillObject_Shard currentSharp;
    [SerializeField] private GameObject shardPrefab;
    [SerializeField] private float detonateTime = 2;

    [Header("Moving Shard Upgrade")]
    [SerializeField] private float shardSpeed = 7;

    [Header("Multicast Shard Upgrade")]
    [SerializeField] private int maxCharges = 3;
    [SerializeField] private int currentCharges;
    [SerializeField] private bool isReCharging;

    protected override void Awake()
    {
        base.Awake();
        currentCharges = maxCharges;
    }


    public override void TryUseSkill()
    {
        if (!CanUseSkill())
            return;

        if (UnLocked(SkillUpgradeType.Shard))
            HandleShardRegular();

        if (UnLocked(SkillUpgradeType.Shard_MoveToEnemy))
            HandleShardMoving();

        if (UnLocked(SkillUpgradeType.Shard_MultiCast))
            HandleShardMulticast();
    }

    private void HandleShardMulticast()
    {
        if (currentCharges <= 0)
            return;

        CreateShard();
        currentSharp.MoveTowardsClosestTarget(shardSpeed);
        currentCharges--;

        if (!isReCharging)
            StartCoroutine(ShardRechargeCo());
    }

    private IEnumerator ShardRechargeCo()
    {
        isReCharging = true;

        while (currentCharges < maxCharges)
        {
            yield return new WaitForSeconds(cooldown);
            currentCharges++;
        }

        isReCharging = false;
    }

    private void HandleShardMoving()
    {
        CreateShard();
        currentSharp.MoveTowardsClosestTarget(shardSpeed);
        SetSkillOnCooldown();
    }

    private void HandleShardRegular()
    {
        CreateShard();
        SetSkillOnCooldown();
    }

    public void CreateShard()
    {
        GameObject shard = Instantiate(shardPrefab, transform.position, Quaternion.identity);
        currentSharp = shard.GetComponent<SkillObject_Shard>();
        currentSharp.SetupShard(detonateTime);
    }
}
