using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_StatSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Player_Stats playerStat;
    private RectTransform rect;
    private UI ui;

    [SerializeField] private StatType statSlotType;
    [SerializeField] private TextMeshProUGUI statName;
    [SerializeField] private TextMeshProUGUI statValue;

    private void OnValidate()
    {
        gameObject.name = "UI_Stat - " + GetStatNameByType(statSlotType);
        statName.text = GetStatNameByType(statSlotType);
    }

    private void Awake()
    {
        ui = GetComponentInParent<UI>();
        rect = GetComponent<RectTransform>();
        playerStat = FindFirstObjectByType<Player_Stats>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.statToolTip.ShowToolTip(true, rect, statSlotType);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.statToolTip.ShowToolTip(false, null);
    }

    public void UpdateStatValue()
    {
        Stat statToUpdate = playerStat.GetStatByType(statSlotType);

        if (statToUpdate == null && statSlotType != StatType.ElementalDamage)
        {
            Debug.Log($"Ban khong co {statSlotType} gan trong player!");
            return;
        }

        float value = 0;

        switch (statSlotType)
        {
            case StatType.Strength:
                value = playerStat.major.strength.GetValue();
                break;
            case StatType.Agility:
                value = playerStat.major.agility.GetValue();
                break;
            case StatType.Intelligence:
                value = playerStat.major.intelligence.GetValue();
                break;
            case StatType.Vitality:
                value = playerStat.major.vitality.GetValue();
                break;

            //offence stats
            case StatType.Damage:
                value = playerStat.GetBaseDamage();
                break;
            case StatType.CritChance:
                value = playerStat.GetCritChance();
                break;
            case StatType.CritPower:
                value = playerStat.GetCritPower();
                break;
            case StatType.ArmorReduction:
                value = playerStat.GetArmorReduction() * 100;
                break;
            case StatType.AttackSpeed:
                value = playerStat.offense.attackSpeed.GetValue() * 100;
                break;

            //Defence
            case StatType.MaxHealth:
                value = playerStat.GetMaxHealth();
                break;
            case StatType.HealthRegen:
                value = playerStat.resources.healthRegen.GetValue();
                break;
            case StatType.Evasion:
                value = playerStat.GetEvasion();
                break;
            case StatType.Armor:
                value = playerStat.GetBaseArmor();
                break;

            //Elemental
            case StatType.IceDamage:
                value = playerStat.offense.iceDamage.GetValue();
                break;
            case StatType.FireDamage:
                value = playerStat.offense.fireDamage.GetValue();
                break;
            case StatType.LightningDamage:
                value = playerStat.offense.lightningDamage.GetValue();
                break;
            case StatType.ElementalDamage:
                value = playerStat.GetElementalDamage(out ElementType element, 1);
                break;

            //Elemental resistance stats
            case StatType.IceResistance:
                value = playerStat.GetElementalResistance(ElementType.Ice) * 100;
                break;
            case StatType.FireResistance:
                value = playerStat.GetElementalResistance(ElementType.Fire) * 100; ;
                break;
            case StatType.LightningResistance:
                value = playerStat.GetElementalResistance(ElementType.Lightning) * 100;
                break;
        }
        statValue.text = IsPercentageStat(statSlotType) ? value + "%" : value.ToString();
    }
    private bool IsPercentageStat(StatType type)
    {
        switch (type)
        {
            case StatType.CritChance:
            case StatType.CritPower:
            case StatType.ArmorReduction:
            case StatType.IceResistance:
            case StatType.FireResistance:
            case StatType.LightningResistance:
            case StatType.AttackSpeed:
            case StatType.Evasion:
                return true;
            default:
                return false;
        }
    }

    private string GetStatNameByType(StatType type)
    {
        switch (type)
        {
            case StatType.MaxHealth: return "Máu tối đa.";
            case StatType.HealthRegen: return "Hồi máu mỗi giây.";
            case StatType.Armor: return "Giáp.";
            case StatType.Evasion: return "Né đòn.";

            case StatType.Strength: return "Sức mạnh.";
            case StatType.Agility: return "Nhanh nhẹn.";
            case StatType.Intelligence: return "Thông minh.";
            case StatType.Vitality: return "Sức sống.";

            case StatType.AttackSpeed: return "Tốc độ tấn công.";
            case StatType.Damage: return "Sát thương.";
            case StatType.CritChance: return "Tỉ lệ chí mạng.";
            case StatType.CritPower: return "Sát thương chí mạng.";
            case StatType.ArmorReduction: return "Giảm giáp.";

            case StatType.FireDamage: return "Sát thương lửa.";
            case StatType.IceDamage: return "Sát thương băng.";
            case StatType.LightningDamage: return "Sát thương điện.";
            case StatType.ElementalDamage: return "Sát thương nguyên tố.";

            case StatType.IceResistance: return "Kháng băng.";
            case StatType.FireResistance: return "Kháng lửa.";
            case StatType.LightningResistance: return "Kháng điện.";
            default: return "Không rõ.";
        }
    }
}
