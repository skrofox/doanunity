using System.Collections;
using UnityEngine;

public class Entity_StatusHandler : MonoBehaviour
{
    private Entity entity;
    private Entity_VFX entityVfx;
    private Entity_Stats entityStats;
    private Entity_Health entityHealth;
    private ElementType currentEffect = ElementType.None;

    private void Awake()
    {
        entityHealth = GetComponent<Entity_Health>();
        entityStats = GetComponent<Entity_Stats>();
        entity = GetComponent<Entity>();
        entityVfx = GetComponent<Entity_VFX>();
    }

    public void ApplyBurnEffect(float duration, float fireDamage)
    {
        float fireResistance = entityStats.GetElementalResistance(ElementType.Fire);
        float finalDamage = fireDamage * (1 - fireResistance);

        StartCoroutine(BurnEffectCo(duration, finalDamage));
    }

    private IEnumerator BurnEffectCo(float duration, float totalDamage)
    {
        currentEffect = ElementType.Fire;
        entityVfx.PlayOnStatusVfx(duration, ElementType.Fire);

        int tickerPerSecond = 2;
        int tickCount = Mathf.RoundToInt(tickerPerSecond * duration);

        float damagePerTick = totalDamage / tickCount;
        float tickInterval = 1f / tickerPerSecond;

        for (int i = 0; i < tickCount; i++)
        {
            entityHealth.ReduceHp(damagePerTick);
            yield return new WaitForSeconds(tickInterval);
        }

        currentEffect = ElementType.None;
    }

    public void ApplyChilledEffect(float duration, float slowMultiplier)
    {
        float iceRes = entityStats.GetElementalResistance(ElementType.Ice);
        float finalDuration = duration * (1- iceRes);

        StartCoroutine(ChilledEffectCo(finalDuration, slowMultiplier));
    }

    private IEnumerator ChilledEffectCo(float duration, float slowMultiplier)
    {
        entity.SlowDownEntity(duration, slowMultiplier);
        currentEffect = ElementType.Ice;
        entityVfx.PlayOnStatusVfx(duration, ElementType.Ice);

        yield return new WaitForSeconds(duration);

        currentEffect = ElementType.None;
    }

    public bool CanBeApplied(ElementType element)
    {
        return currentEffect == ElementType.None;
    }
}
