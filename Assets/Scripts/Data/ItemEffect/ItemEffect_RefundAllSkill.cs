using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/Item Effect/Refund All Skills", fileName = "Item effect data - Refund all skill")]
public class ItemEffect_RefundAllSkill : ItemEffectDataSO
{
    public override void ExecuteEffect()
    {
        //UI_SkillTree skillTree = FindFirstObjectByType<UI_SkillTree>(FindObjectsInactive.Include);
        //skillTree.RefundAllSkills();
        UI ui = FindFirstObjectByType<UI>();
        ui.skillTree.RefundAllSkills();
    }
}
