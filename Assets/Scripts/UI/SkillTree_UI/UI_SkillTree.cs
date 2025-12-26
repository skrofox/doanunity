using TMPro;
using UnityEngine;

public class UI_SkillTree : MonoBehaviour
{
    [SerializeField] private int skillPoints;
    [SerializeField] private TextMeshProUGUI skillPointText;
    [SerializeField] private UI_TreeConnectHandler[] parentNodes;
    private UI_TreeNode[] allTreeNodes;

    public Player_SkillManager skillManager { get; private set; }


    private void Start()
    {
        UpdateAllConnections();
        UpdateSkillPointsUI();
    }

    private void UpdateSkillPointsUI()
    {
        skillPointText.text = skillPoints.ToString();
    }

    public void UnlockDefaultSkills()
    {
        allTreeNodes = GetComponentsInChildren<UI_TreeNode>(true);
        skillManager = FindAnyObjectByType<Player_SkillManager>();

        foreach (var node in allTreeNodes)
        {
            node.UnlockDefaultSkills();
        }
    }

    [ContextMenu("Reset Skill Tree")]
    public void RefundAllSkills()
    {
        UI_TreeNode[] skillNodes = GetComponentsInChildren<UI_TreeNode>();

        foreach (var node in skillNodes)
        {
            node.Refund();
        }
    }

    public bool EnoughSkillPoints(int cost) => skillPoints >= cost;
    public void RemoveSkillPoint(int cost)
    {
        skillPoints -= cost;
        UpdateSkillPointsUI();
    }
    public void AddSkillPoints(int points)
    {
        skillPoints += points;
        UpdateSkillPointsUI();
    }

    [ContextMenu("Update All Connections")]
    public void UpdateAllConnections()
    {
        foreach (var node in parentNodes)
        {
            node.UpdateAllConnections();
        }
    }
}
