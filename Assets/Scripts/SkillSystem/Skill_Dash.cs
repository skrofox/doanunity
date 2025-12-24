using UnityEngine;

public class Skill_Dash : Skill_Base
{
    
    public void OnStartEffect()
    {
        if (UnLocked(SkillUpgradeType.Dash_CloneOnStart) || UnLocked(SkillUpgradeType.Dash_CloneOnStartAndArrival))
            CreateClone();

        if (UnLocked(SkillUpgradeType.Dash_ShardOnStart) || UnLocked(SkillUpgradeType.Dash_ShardOnStartAndArrival))
            CreateShard();
    }

    public void OnEndEffect()
    {
        if (UnLocked(SkillUpgradeType.Dash_CloneOnStartAndArrival))
            CreateClone();

        if (UnLocked(SkillUpgradeType.Dash_ShardOnStartAndArrival))
            CreateShard();
    }

    private void CreateShard()
    {
        skillManager.shard.CreateRawShard();
    }

    private void CreateClone()
    {
        Debug.Log("Create Clone");
    }
}
