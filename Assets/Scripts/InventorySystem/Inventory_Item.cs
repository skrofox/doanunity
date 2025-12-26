using System;
using System.Text;
using UnityEngine;

[Serializable]
public class Inventory_Item
{
    private string itemId;

    public ItemDataSO itemData;
    public int stackSize = 1;

    public ItemModifier[] modifiers { get; private set; }
    public ItemEffectDataSO itemEffect;

    public Inventory_Item(ItemDataSO itemData)
    {
        this.itemData = itemData;
        itemEffect = itemData.itemEffect;
        modifiers = EquipmentData()?.modifiers;

        itemId = itemData.itemName + " - " + Guid.NewGuid();
    }

    public void AddModifiers(Entity_Stats playerStats)
    {
        foreach (var mod in modifiers)
        {
            Stat statToModify = playerStats.GetStatByType(mod.statType);
            statToModify.AddModifier(mod.value, itemId);
        }
    }

    public void RemoveModifiers(Entity_Stats playerStats)
    {
        foreach (var mod in modifiers)
        {
            Stat statToModify = playerStats.GetStatByType(mod.statType);
            statToModify.RemoveModifier(itemId);
        }
    }

    public void AddItemEffect(Player player) => itemEffect?.Subscribe(player);
    public void RemoveItemEffect() => itemEffect?.Unsubscribe();

    private EquipmentDataSO EquipmentData()
    {
        if (itemData is EquipmentDataSO equipment)
            return equipment;

        return null;
    }

    public bool CanAddStack() => stackSize < itemData.maxStackSize;
    public void AddStack() => stackSize++;
    public void RemoveStack() => stackSize--;

    public string GetItemInfo()
    {
        if (itemData.itemType == ItemType.Material)
            return "Dùng để chế tạo.";

        if (itemData.itemType == ItemType.Consumable)
            return itemData.itemEffect.effectDescription;

        StringBuilder sb = new StringBuilder();

        sb.AppendLine("");

        foreach (var mod in modifiers)
        {
            string modType = GetStatNameByType(mod.statType);
            string modValue = IsPercentageStat(mod.statType) ? mod.value.ToString() + "%" : mod.value.ToString();
            sb.AppendLine("+ " + modValue + " " + modType);
        }

        if (itemEffect != null)
        {
            sb.AppendLine("");
            sb.AppendLine("Kĩ năng đặc biệt: ");
            sb.AppendLine(itemEffect.effectDescription);
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
