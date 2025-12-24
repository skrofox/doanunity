using UnityEngine;

public class ElementalEffectData
{
    public float ChillDuration;
    public float chillSlowMultiplier;

    public float burnDuration;
    public float totalburnDamage;

    public float shockDuration;
    public float shockDamage;
    public float shockCharge;

    public ElementalEffectData(Entity_Stats entityStats, DamageScaleData damageScale)
    {
        ChillDuration = damageScale.chillDuration;
        chillSlowMultiplier = damageScale.chillSlowMultiplier;

        burnDuration = damageScale.burnDuration;
        totalburnDamage = entityStats.offense.fireDamage.GetValue() * damageScale.burnDamageScale;

        shockDuration = damageScale.shockDuration;
        shockDamage = entityStats.offense.lightningDamage.GetValue() * damageScale.shockDamageScale;
        shockCharge = damageScale.shockCharge;

    }
}