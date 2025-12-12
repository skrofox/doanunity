using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    private Entity_VFX vfx;
    private Entity_Stats stats;

    [Header("Taget Detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1;
    [SerializeField] private LayerMask WhatIsTarget;

    [Header("Status effect details")]
    [SerializeField] private float defaultDuration = 3;
    [SerializeField] private float chillSlowMultiplier = .2f;

    private void Awake()
    {
        vfx = GetComponent<Entity_VFX>();
        stats = GetComponent<Entity_Stats>();
    }

    public void PerformAttack()
    {
        GetDetectedColliders();

        foreach (var target in GetDetectedColliders())
        {
            IDamagable damegable = target.GetComponent<IDamagable>();

            if (damegable == null)
                continue; //bo qua muc tiu hien tai, di den mt tiep theo

            float elementalDamage = stats.GetElementalDamage(out ElementType element);
            float damage = stats.GetPhysicalDamage(out bool isCrit);
            bool targetGotHit = damegable.TakeDamage(damage, elementalDamage, element, transform);

            if (element != ElementType.None)
                ApplyStatusEffect(target.transform, element);

            if (targetGotHit)
            {
                vfx.UpdateOnHitColor(element);
                vfx.CreateOnHitVFX(target.transform, isCrit);
            }
        }
    }

    public void ApplyStatusEffect(Transform target, ElementType element, float scaleFactor = 1)
    {
        Entity_StatusHandler statusHandler = target.GetComponent<Entity_StatusHandler>();
        if (statusHandler == null)
            return;

        if (element == ElementType.Ice && statusHandler.CanBeApplied(ElementType.Ice))
            statusHandler.ApplyChilledEffect(defaultDuration, chillSlowMultiplier);

        if (element == ElementType.Fire && statusHandler.CanBeApplied(ElementType.Fire))
        {
            float fireDamage = stats.offense.fireDamage.GetValue() * scaleFactor;
            statusHandler.ApplyBurnEffect(defaultDuration, fireDamage);
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
