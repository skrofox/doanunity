using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Skill Data", fileName = "Skill Data - ")]
public class Skill_DataSO : ScriptableObject
{
    [Header("Skill Description")]
    public Sprite icon;
    public string displayName;
    [TextArea]
    public string description;

    [Header("Unlock & Upgrade")]
    public int cost;
    public bool unlockedByDefault;
    public SkillType skillType;
    public UpgradeData upgradeData;
}

[System.Serializable]
public class UpgradeData
{
    public SkillUpgradeType upgradeType;
    public float cooldown;
    public DamageScaleData damageScaleData;
}