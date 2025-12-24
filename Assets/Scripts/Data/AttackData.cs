using System;
using UnityEngine;

[Serializable]
public class AttackData
{
    public float physicalDamage;
    public float elemtentalDamage;
    public bool isCrit;
    public ElementType element;

    public ElementalEffectData effectData;

    public AttackData(Entity_Stats entityStats, DamageScaleData scaleData)
    {
        physicalDamage = entityStats.GetPhysicalDamage(out isCrit, scaleData.physical);
        elemtentalDamage = entityStats.GetElementalDamage(out element, scaleData.elemental);

        effectData = new ElementalEffectData(entityStats, scaleData);
    }
}
