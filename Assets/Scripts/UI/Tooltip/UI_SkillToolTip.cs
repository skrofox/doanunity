using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

public class UI_SkillToolTip : UI_ToolTip
{
    private UI ui;
    private UI_SkillTree skillTree;

    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillDescription;
    [SerializeField] private TextMeshProUGUI skillCooldown;
    [SerializeField] private TextMeshProUGUI skillRequirements;

    [Space]
    [SerializeField] private string metConditionHex;
    [SerializeField] private string notMetConditionHex;
    [SerializeField] private string importantInfoHex;
    [SerializeField] private Color exampleColor;
    [SerializeField] private string lockedSkillText = "Bạn không thể học 2 kĩ năng có thuộc tính riêng biệt.";

    private Coroutine textEffectCo;

    protected override void Awake()
    {
        base.Awake();
        ui = GetComponentInParent<UI>();
        skillTree = ui.GetComponentInChildren<UI_SkillTree>(true);
    }

    public override void ShowToolTip(bool show, RectTransform targetRect)
    {
        base.ShowToolTip(show, targetRect);

    }

    public void ShowToolTip(bool show, RectTransform targetRect, SkillDataSO skillData, UI_TreeNode node)// Skill_DataSO skillData, UI_TreeNode[] conflitNodes, )
    {
        base.ShowToolTip(show, targetRect);

        if (!show) return;

        skillName.text = skillData.displayName;
        skillDescription.text = skillData.description;
        skillCooldown.text = "Thời gian hồi chiêu:" + skillData.upgradeData.cooldown + " giây.";

        if (node == null)
        {
            skillRequirements.text = "";
            return;
        }


        string skillLockedText = $"<color={importantInfoHex}> {lockedSkillText}</color>";
        string requirements = node.isLooked ? skillLockedText : GetRequirements(node.skillData.cost, node.neededNodes, node.conflictNodes);

        //skillRequirements.text = "Requirements: \n" + " -" + node.skillData.cost + " skill point.";
        skillRequirements.text = requirements;
    }

    public void LockedSkillEffect()
    {
        if (textEffectCo != null)
            StopCoroutine(textEffectCo);

        textEffectCo = StartCoroutine(TextBlinkEffectCo(skillRequirements, .15f, 3));
    }

    private IEnumerator TextBlinkEffectCo(TextMeshProUGUI text, float blinkInterval, int blinkCount)
    {
        for (int i = 0; i < blinkCount; i++)
        {
            text.text = GetColoredText(notMetConditionHex, lockedSkillText);
            yield return new WaitForSeconds(blinkInterval);

            text.text = GetColoredText(importantInfoHex, lockedSkillText);
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    private string GetRequirements(int skillCost, UI_TreeNode[] neededNodes, UI_TreeNode[] conflictNodes)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("Cần thiết:");

        string costColor = skillTree.EnoughSkillPoints(skillCost) ? metConditionHex : notMetConditionHex;

        sb.AppendLine($"<color={costColor}>- {skillCost} điểm kĩ năng(s) </color>");

        foreach (var node in neededNodes)
        {
            string nodeColor = node.isUnlocked ? metConditionHex : notMetConditionHex;
            sb.AppendLine($"<color={nodeColor}>- {node.skillData.displayName}.</color>");
        }

        if (conflictNodes.Length <= 0)
            return sb.ToString();

        sb.AppendLine();
        sb.AppendLine($"<color={importantInfoHex}>Không học được: </color>");

        foreach (var node in conflictNodes)
        {
            if (node == null) continue;

            sb.AppendLine($"<color={importantInfoHex}>- {node.skillData.displayName}.</color>");
        }

        return sb.ToString();

    }

}
