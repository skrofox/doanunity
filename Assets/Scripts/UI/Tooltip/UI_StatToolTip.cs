using TMPro;
using UnityEngine;

public class UI_StatToolTip : UI_ToolTip
{
    private Player_Stats playerStats;
    private TextMeshProUGUI statToolTipText;

    protected override void Awake()
    {
        base.Awake();
        playerStats = FindFirstObjectByType<Player_Stats>();
        statToolTipText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ShowToolTip(bool show, RectTransform targetRect, StatType statType)
    {
        base.ShowToolTip(show, targetRect);
        statToolTipText.text = GetStatTextByType(statType);
    }

    public string GetStatTextByType(StatType type)
    {
        switch (type)
        {
            case StatType.Strength:
                return "Tăng sát thương vật lý thêm 1 điểm mỗi điểm và sức mạnh chí mạng thêm 0,5% mỗi điểm.";
            case StatType.Agility:
                return "Tăng tỉ lệ chí mạng thêm 0.3% mỗi điểm và né dòn thêm 0,5% mỗi điểm.";
            case StatType.Intelligence:
                return "Tăng kháng nguyên tố thêm 0.5% mỗi điểm và 1 sát thương nguyên tố mỗi điểm.";
            case StatType.Vitality:
                return "Tăng máu tối đa thêm 5 điểm mỗi điểm và tăng giáp thêm 1 điểm mỗi điểm.";

            case StatType.Damage:
                return "Tăng nhẹ sát thương vật lý.";
            case StatType.CritChance:
                return "Tăng nhẹ tỉ lệ chí mạng.";
            case StatType.CritPower:
                return "Tăng nhẹ sát thương chí mạng.";
            case StatType.ArmorReduction:
                return "Tăng nhẹ xuyên giáp.";
            case StatType.AttackSpeed:
                return "Tăng nhẹ tốc đánh.";

            case StatType.MaxHealth:
                return "Tăng máu tối đa.";
            case StatType.HealthRegen:
                return "Tăng tốc độ hồi máu mỗi giây.";
            case StatType.Armor:
                return "Giảm sát thương nhận vào, tối đa không quá 60%." +
                    "\nGiáp hiện tại: " + playerStats.GetArmorMitigation(0) * 100 + "%.";
            case StatType.Evasion:
                return "Tăng khả năng né đòn, tối đa 40%.";

            // Elemental Damage
            case StatType.IceDamage:
                return "Tăng sát thương nguyên tố băng.";
            case StatType.FireDamage:
                return "Tăng sát thương nguyên tốt lửa.";
            case StatType.LightningDamage:
                return "Tăng sát thương nguyên tốt điện.";
            case StatType.ElementalDamage:
                return
                    "Sát thương nguyên tố kết hợp cả ba nguyên tố." +
                    "\n Nguyên tố mạnh nhất gây ra hiệu ứng trạng thái tương ứng và sát thương tối đa. " +
                    "\n Hai nguyên tố còn lại đóng góp 50% sát thương.";

            // Elemental Resistances
            case StatType.IceResistance:
                return "Giảm sát thương bị gây ra bởi nguyên tố băng.";
            case StatType.FireResistance:
                return "Giảm sát thương bị gây ra bởi nguyên tố lửa.";
            case StatType.LightningResistance:
                return "Giảm sát thương bị gây ra bởi nguyên tố điện.";

            default:
                return "Thật bí ẩn.";
        }
    }
}
