using System;
using System.Collections;
using UnityEngine;

public class Skill_Shard : Skill_Base
{
    private SkillObject_Shard currentSharp;
    private Entity_Health playerHealth;

    [SerializeField] private GameObject shardPrefab;
    [SerializeField] private float detonateTime = 2;

    [Header("Moving Shard Upgrade")]
    [SerializeField] private float shardSpeed = 7;

    [Header("Multicast Shard Upgrade")]
    [SerializeField] private int maxCharges = 3;
    [SerializeField] private int currentCharges;
    [SerializeField] private bool isReCharging;

    [Header("Teleport Shard Upgrade")]
    [SerializeField] private float shardExistDuration = 10f;

    [Header("Health Rewind Shard Upgrade")]
    [SerializeField] private float saveHealthPercent;

    protected override void Awake()
    {
        base.Awake();
        currentCharges = maxCharges;
        playerHealth = GetComponentInParent<Entity_Health>();
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

        if (UnLocked(SkillUpgradeType.Shard_Teleport))
            HandleShardTeleport();

        if (UnLocked(SkillUpgradeType.Shard_TeleportHpRewind))
            HandleShardHealthRewind();
    }

    private void HandleShardHealthRewind()
    {
        if (currentSharp == null)
        {
            CreateShard();
            saveHealthPercent = playerHealth.GetHealthPercent();
        }
        else
        {
            SwapPlayerAndSharp();
            playerHealth.SetHealthToPercent(saveHealthPercent);
            SetSkillOnCooldown();
        }
    }

    private void HandleShardTeleport()
    {
        if (currentSharp == null)
        {
            CreateShard();
        }
        else
        {
            SwapPlayerAndSharp();
            SetSkillOnCooldown();
        }
    }

    private void SwapPlayerAndSharp()
    {
        Vector3 shardPosition = currentSharp.transform.position;
        Vector3 playerPosition = player.transform.position;

        currentSharp.transform.position = playerPosition;
        currentSharp.Explode();

        player.TeleportPlayer(shardPosition);
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
        float detonateTime = GetDetonateTime();

        GameObject shard = Instantiate(shardPrefab, transform.position, Quaternion.identity);
        currentSharp = shard.GetComponent<SkillObject_Shard>();
        currentSharp.SetupShard(this);

        if (UnLocked(SkillUpgradeType.Shard_Teleport) || UnLocked(SkillUpgradeType.Shard_TeleportHpRewind))
            currentSharp.OnExplode += ForceColldown;
    }

    public float GetDetonateTime()
    {
        if (UnLocked(SkillUpgradeType.Shard_Teleport) || UnLocked(SkillUpgradeType.Shard_TeleportHpRewind))
        {
            return shardExistDuration;
        }
        return detonateTime;
    }

    private void ForceColldown()
    {
        if (OnCooldown() == false)
        {
            SetSkillOnCooldown();
            currentSharp.OnExplode -= ForceColldown;
        }
    }
}
