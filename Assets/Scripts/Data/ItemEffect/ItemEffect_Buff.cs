using System;
using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/Item Effect/Buff Effect", fileName = "Item effect data - Buff")]
public class ItemEffect_Buff : ItemEffectDataSO
{
    [SerializeField] private BuffEffectData[] buffsToApply;
    [SerializeField] private float duration;
    [SerializeField] private string source = Guid.NewGuid().ToString();

    private Player_Stats playerStats;

    public override bool CanBeUsed()
    {
        if (playerStats == null)
            playerStats = FindFirstObjectByType<Player_Stats>();

        //return playerStats.CanApplyBuffOf(source);
        if (playerStats.CanApplyBuffOf(source))
        {
            return true;
        }
        else
        {
            Debug.Log("Hieu ung giong nhau, khong applied dc");
            return false;
        }
    }

    public override void ExecuteEffect()
    {
        playerStats.ApplyBuff(buffsToApply, duration, source);
    }
}
