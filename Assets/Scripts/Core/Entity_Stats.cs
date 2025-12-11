using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public Stat maxHealth;
    public Stat_MajorGroup major;
    public Stat_OffenseGroup offense;
    public Stat_DefenceGroup defence;

    public float GetPhysicalDamage(out bool isCrit)
    {
        float baseDamage = offense.damage.GetValue();
        float bonusDamage = major.strength.GetValue();

        float totalBaseDamage = baseDamage + bonusDamage;

        float baseCritChance = offense.critChange.GetValue();
        float bonusCritChance = major.agility.GetValue() * 0.3f; //Bonus crit chance from Agility: +0.3% per AGI
        float critChance = baseCritChance + bonusCritChance;

        float baseCritPower = offense.critPower.GetValue();
        float bonusCritPower = major.strength.GetValue() * 0.5f; //Bonus crit chance from Strength: +0.5% per STR
        float critPower = (baseCritPower + bonusCritPower) / 100;//Total crit power as multiplier (e.g 150 / 100 = 1.5f - multiplier);

        isCrit = Random.Range(0, 100) < critChance;
        float finalDamage = isCrit ? totalBaseDamage * critPower : totalBaseDamage;

        return finalDamage;
    }

    public float GetArmorMitigation(float armorReduction)
    {
        float baseArmor = defence.armor.GetValue();
        float bonusArmor = major.vitality.GetValue(); //Bonus armor from Vitality: +1 per VIT
        float totalArmor = baseArmor + bonusArmor;

        //float reductionMultiplier = Mathf.Clamp(1 - armorReduction, 0, 1);
        float reductionMultiplier = Mathf.Clamp01(1 - armorReduction);
        float effectiveArmor = totalArmor * reductionMultiplier;

        float mitigation = effectiveArmor / (effectiveArmor + 100);
        float mitigationCap = .50f; //max mitigation will be capped at 50%

        float finalMitigation = Mathf.Clamp(mitigation, 0, mitigationCap);

        return finalMitigation;
    }

    public float GetArmorReduction()
    {
        //Total Armor Reduction as multiplier (e.g 30 / 100 = 0.3f - multiplier);
        float finalReduction = offense.armorReduction.GetValue() / 100;

        return finalReduction;
    }

    public float GetEvasion()
    {
        float baseEvasion = defence.evasion.GetValue();
        float bonusEvasion = major.agility.GetValue() * .5f; // moi diem nhanh nhen (agility) cho 0.5% evasion(ne don).

        float totalEvasion = baseEvasion + bonusEvasion;

        float evasionCap = 30f; //gioi han evasion la 30%

        float finalEvasion = Mathf.Clamp(totalEvasion, 0, evasionCap);

        return finalEvasion;
    }

    public float GetMaxHealth()
    {
        float baseMaxHealth = maxHealth.GetValue();
        float bonusMaxHealth = major.vitality.GetValue() * 5;
        float finalMaxHealth = baseMaxHealth + bonusMaxHealth;

        return finalMaxHealth;
    }
}
