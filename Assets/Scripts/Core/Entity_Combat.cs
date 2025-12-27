using System;
using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    public event Action<float> OnDoingPhysicalDamage;

    private Entity_SFX sfx;
    private Entity_VFX vfx;
    private Entity_Stats stats;

    public DamageScaleData basicAttackScale;

    [Header("Taget Detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1;
    [SerializeField] private LayerMask WhatIsTarget;

    //[Header("Status effect details")]
    //[SerializeField] private float defaultDuration = 3;
    //[SerializeField] private float chillSlowMultiplier = .2f;
    //[SerializeField] private float electrifyChargeBuildUp = .4f;
    //[Space]
    //[SerializeField] private float fireScale = .8f;
    //[SerializeField] private float lightningScale = 1.5f;

    private void Awake()
    {
        vfx = GetComponent<Entity_VFX>();
        sfx = GetComponent<Entity_SFX>();
        stats = GetComponent<Entity_Stats>();
    }

    public void PerformAttack()
    {
        bool targetGotHit = false;
        GetDetectedColliders();

        foreach (var target in GetDetectedColliders())
        {
            IDamagable damegable = target.GetComponent<IDamagable>();

            if (damegable == null)
                continue; //bo qua muc tiu hien tai, di den mt tiep theo

            AttackData attackData = stats.GetAttackData(basicAttackScale);
            Entity_StatusHandler statusHandle = target.GetComponent<Entity_StatusHandler>();

            float physicalDamage = attackData.physicalDamage;
            float elementalDamage = attackData.elemtentalDamage;
            ElementType element = attackData.element;

            targetGotHit = damegable.TakeDamage(physicalDamage, elementalDamage, element, transform);

            if (element != ElementType.None)
                statusHandle?.ApplyStatusEffect(element, attackData.effectData);

            if (targetGotHit)
            {
                OnDoingPhysicalDamage?.Invoke(physicalDamage);
                vfx.CreateOnHitVFX(target.transform, attackData.isCrit, element);
                sfx?.PlayAttackHit();
            }
            //else
            //{
            //    sfx?.PlayAttackMiss();
            //}
        }

        if(targetGotHit == false)
        {
            sfx?.PlayAttackMiss();
        }
    }

    protected Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, WhatIsTarget);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }
}
