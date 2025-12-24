using UnityEngine;

public class SkillObject_Base : MonoBehaviour
{
    [SerializeField] protected LayerMask WhatIsEnemy;
    [SerializeField] protected Transform targetCheck;
    [SerializeField] protected float checkRadius = 1;

    protected Entity_Stats playerStats;
    protected DamageScaleData damageScaleData;
    protected ElementType usedElement;

    protected void DamageEnemiesInRadius(Transform t, float radius)
    {
        foreach (var target in EnemiesAround(t, radius))
        {
            IDamagable damagable = target.GetComponent<IDamagable>();

            if (damagable == null)
                continue;

            AttackData attackData = playerStats.GetAttackData(damageScaleData);
            Entity_StatusHandler statusHandler = target.GetComponent<Entity_StatusHandler>();

            float physDamage = attackData.physicalDamage;
            float elemDamage = attackData.elemtentalDamage;
            ElementType element = attackData.element;


            damagable.TakeDamage(physDamage, elemDamage, element, transform);
        
            if (element != ElementType.None)
                statusHandler?.ApplyStatusEffect(element, attackData.effectData);

            usedElement = element;
        }
    }

    protected Transform FindClosestTarget()
    {
        Transform target = null;
        float closestDistance = Mathf.Infinity;

        foreach (var enemy in EnemiesAround(transform, 10))
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance < closestDistance)
            {
                target = enemy.transform;
            }
        }

        return target;
    }

    protected Collider2D[] EnemiesAround(Transform t, float radius)
    {
        return Physics2D.OverlapCircleAll(t.position, radius, WhatIsEnemy);
    }

    protected virtual void OnDrawGizmos()
    {
        if (targetCheck == null)
            targetCheck = transform;

        Gizmos.DrawWireSphere(targetCheck.position, checkRadius);
    }
}
