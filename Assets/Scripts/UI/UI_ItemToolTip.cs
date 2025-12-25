using System.Text;
using TMPro;
using UnityEngine;

public class UI_ItemToolTip : UI_ToolTip
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemType;
    [SerializeField] private TextMeshProUGUI itemInfo;

    public void ShowToolTip(bool show, RectTransform targetRect, Inventory_Item itemToShow)
    {
        base.ShowToolTip(show, targetRect);

        itemName.text = itemToShow.itemData.itemName;
        itemType.text = itemToShow.itemData.itemType.ToString();
        itemInfo.text = GetItemInfo(itemToShow);
    }

    public string GetItemInfo(Inventory_Item item)
    {
        if (item.itemData.itemType == ItemType.Material)
            return "Dùng để chế tạo.";

        if (item.itemData.itemType == ItemType.Consumable)
            return item.itemData.itemEffect.effectDescription;

        StringBuilder sb = new StringBuilder();

        sb.AppendLine("");

        foreach (var mod in item.modifiers)
        {
            string modType = GetStatNameByType(mod.statType);
            string modValue = IsPercentageStat(mod.statType) ? mod.value.ToString() + "%" : mod.value.ToString();
            sb.AppendLine("+ " + modValue + " " + modType);
        }

        return sb.ToString();
    }

    private string GetStatNameByType(StatType type)
    {
        switch (type)
        {
            case StatType.MaxHealth: return "Máu tối đa.";
            case StatType.HealthRegen: return "Hồi máu mỗi giây.";
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
            case StatType.Armor: return "Giáp.";
            case StatType.Evasion: return "Né đòn.";
            case StatType.IceResistance: return "Kháng băng.";
            case StatType.FireResistance: return "Kháng lửa.";
            case StatType.LightningResistance: return "Kháng điện.";
            default: return "Không rõ.";
        }
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
}
