using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public StatSetupDataSO defaultStatSetup;

    public Stat_ResourceGroup resources;
    public Stat_OffenseGroup offense;
    public Stat_DefenceGroup defence;
    public Stat_MajorGroup major;

    protected virtual void Awake()
    {

    }

    public void AdjustStatsSetup(Stat_ResourceGroup resourceGroup, Stat_OffenseGroup offenseGroup, Stat_DefenceGroup defenceGroup, float penalty, float increase)
    {
        //increased stats
        offense.damage.SetBaseValue(offenseGroup.damage.GetValue() * increase);
        offense.attackSpeed.SetBaseValue(offenseGroup.attackSpeed.GetValue() * increase);
        offense.critChance.SetBaseValue(offenseGroup.critChance.GetValue() * increase);
        offense.critPower.SetBaseValue(offenseGroup.critPower.GetValue() * increase);
        offense.fireDamage.SetBaseValue(offenseGroup.fireDamage.GetValue() * increase);
        offense.iceDamage.SetBaseValue(offenseGroup.iceDamage.GetValue() * increase);
        offense.lightningDamage.SetBaseValue(offenseGroup.lightningDamage.GetValue() * increase);

        defence.evasion.SetBaseValue(defenceGroup.evasion.GetValue() * increase);

        //penalty stats
        resources.maxHealth.SetBaseValue(resourceGroup.maxHealth.GetValue() * penalty);
        resources.healthRegen.SetBaseValue(resourceGroup.healthRegen.GetValue() * penalty);

        defence.armor.SetBaseValue(defenceGroup.armor.GetValue() * penalty);
        defence.fireRes.SetBaseValue(defenceGroup.fireRes.GetValue() * penalty);
        defence.iceRes.SetBaseValue(defenceGroup.iceRes.GetValue() * penalty);
        defence.lightningRes.SetBaseValue(defenceGroup.lightningRes.GetValue() * penalty);

    }

    public AttackData GetAttackData(DamageScaleData scaleData)
    {
        return new AttackData(this, scaleData);
    }

    public float GetElementalDamage(out ElementType element, float scaleFactor = 1)
    {
        float fireDamage = offense.fireDamage.GetValue();
        float iceDamage = offense.iceDamage.GetValue();
        float lightningDamage = offense.lightningDamage.GetValue();

        float bonusElementalDamage = major.intelligence.GetValue();// Bonus elemental damage from Intelligence +1 per INT

        //
        float highestDamage = fireDamage;
        element = ElementType.Fire;

        if (iceDamage > highestDamage)
        {
            element = ElementType.Ice;
            highestDamage = iceDamage;
        }

        if (lightningDamage > highestDamage)
        {
            element = ElementType.Lightning;
            highestDamage = lightningDamage;
        }
        //

        if (highestDamage <= 0)
        {
            element = ElementType.None;
            return 0;
        }

        float bonusFire = (fireDamage == highestDamage) ? 0 : fireDamage * 0.3f;
        float bonusIce = (iceDamage == highestDamage) ? 0 : iceDamage * 0.3f;
        float bonuslightning = (lightningDamage == highestDamage) ? 0 : lightningDamage * .3f;

        float weakerElementalDamage = bonusFire + bonusIce + bonuslightning;
        float finalDamage = highestDamage + weakerElementalDamage + bonusElementalDamage;

        return finalDamage * scaleFactor;
    }

    public float GetElementalResistance(ElementType element)
    {
        float baseResistance = 0;
        float bonusResistance = major.intelligence.GetValue() * .5f; //tri thong minh giam 0.5% sat thuong nguyen to

        switch (element)
        {
            case ElementType.Fire:
                baseResistance = defence.fireRes.GetValue();
                break;
            case ElementType.Ice:
                baseResistance = defence.iceRes.GetValue();
                break;
            case ElementType.Lightning:
                baseResistance = defence.lightningRes.GetValue();
                break;
                //default:
                //    baseResistance = 0;
                //    break;
        }

        float resistance = baseResistance + bonusResistance;
        float resistanCap = 75;//toi da 75%
        float finalResistance = Mathf.Clamp(resistance, 0, resistanCap) / 100;//convert value into 0 to 1 multiplier 

        return finalResistance;
    }

    public float GetPhysicalDamage(out bool isCrit, float scaleFactor = 1)
    {
        float baseDamage = GetBaseDamage();

        float critChance = GetCritChance();
        float critPower = GetCritPower() / 100;//Total crit power as multiplier (e.g 150 / 100 = 1.5f - multiplier);

        isCrit = Random.Range(0, 100) < critChance;
        float finalDamage = isCrit ? baseDamage * critPower : baseDamage;

        return finalDamage * scaleFactor;
    }

    public float GetBaseDamage() => offense.damage.GetValue() + major.strength.GetValue(); //bonus damage from strengh: +1 per STR
    public float GetCritChance() => offense.critChance.GetValue() + (major.agility.GetValue() * .3f); //Bonus crit chance from Agility: +0.3% per AGI
    public float GetCritPower() => offense.critPower.GetValue() + (major.strength.GetValue() * .5f); //Bonus crit chance from Strength: +0.5% per STR

    public float GetArmorMitigation(float armorReduction)
    {
        float totalArmor = GetBaseArmor();

        //float reductionMultiplier = Mathf.Clamp(1 - armorReduction, 0, 1);
        float reductionMultiplier = Mathf.Clamp01(1 - armorReduction);
        float effectiveArmor = totalArmor * reductionMultiplier;

        float mitigation = effectiveArmor / (effectiveArmor + 100);
        float mitigationCap = .60f; //max mitigation will be capped at 60%

        float finalMitigation = Mathf.Clamp(mitigation, 0, mitigationCap);

        return finalMitigation;
    }

    public float GetBaseArmor() => defence.armor.GetValue() + major.vitality.GetValue(); //Bonus armor from Vitality: +1 per VIT

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

        float evasionCap = 40f; //gioi han evasion la 40%

        float finalEvasion = Mathf.Clamp(totalEvasion, 0, evasionCap);

        return finalEvasion;
    }

    public float GetMaxHealth()
    {
        float baseMaxHealth = resources.maxHealth.GetValue();
        float bonusMaxHealth = major.vitality.GetValue() * 5;
        float finalMaxHealth = baseMaxHealth + bonusMaxHealth;

        return finalMaxHealth;
    }

    public Stat GetStatByType(StatType type)
    {
        switch (type)
        {
            case StatType.MaxHealth: return resources.maxHealth;
            case StatType.HealthRegen: return resources.healthRegen;

            case StatType.Strength: return major.strength;
            case StatType.Agility: return major.agility;
            case StatType.Vitality: return major.vitality;
            case StatType.Intelligence: return major.intelligence;

            case StatType.AttackSpeed: return offense.attackSpeed;
            case StatType.Damage: return offense.damage;
            case StatType.CritChance: return offense.critChance;
            case StatType.CritPower: return offense.critPower;
            case StatType.ArmorReduction: return offense.armorReduction;

            case StatType.FireDamage: return offense.fireDamage;
            case StatType.IceDamage: return offense.iceDamage;
            case StatType.LightningDamage: return offense.lightningDamage;

            case StatType.Armor: return defence.armor;
            case StatType.Evasion: return defence.evasion;

            case StatType.IceResistance: return defence.iceRes;
            case StatType.FireResistance: return defence.fireRes;
            case StatType.LightningResistance: return defence.lightningRes;

            default:
                //kieu du lieu thong ke chua dc kiem tra
                Debug.LogWarning($"Stat type {type} not implemented yet.");
                return null;
        }
    }

    [ContextMenu("Update Default Stat Setup")]
    public void ApplyDefaultStatSetup()
    {
        if (defaultStatSetup == null)
        {
            Debug.LogWarning("No default stat setup assigned");
            return;
        }

        resources.maxHealth.SetBaseValue(defaultStatSetup.maxHealth);
        resources.healthRegen.SetBaseValue(defaultStatSetup.healthRegen);

        major.strength.SetBaseValue(defaultStatSetup.strength);
        major.agility.SetBaseValue(defaultStatSetup.agility);
        major.intelligence.SetBaseValue(defaultStatSetup.intelligence);
        major.vitality.SetBaseValue(defaultStatSetup.vitality);

        
        offense.attackSpeed.SetBaseValue(defaultStatSetup.attackSpeed);
        offense.damage.SetBaseValue(defaultStatSetup.damage);
        offense.critChance.SetBaseValue(defaultStatSetup.critChance);
        offense.critPower.SetBaseValue(defaultStatSetup.critPower);
        offense.armorReduction.SetBaseValue(defaultStatSetup.armorReduction);
        
        offense.fireDamage.SetBaseValue(defaultStatSetup.fireDamage);
        offense.iceDamage.SetBaseValue(defaultStatSetup.iceDamage);
        offense.lightningDamage.SetBaseValue(defaultStatSetup.lightningDamage);


        defence.armor.SetBaseValue(defaultStatSetup.armor);
        defence.evasion.SetBaseValue(defaultStatSetup.evasion);

        defence.iceRes.SetBaseValue(defaultStatSetup.iceResistance);
        defence.fireRes.SetBaseValue(defaultStatSetup.fireResistance);
        defence.lightningRes.SetBaseValue(defaultStatSetup.lightningResistance);
    }
}
