using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public Stat maxHealth;
    public Stat_MajorGroup major;
    public Stat_OffenseGroup offense;
    public Stat_DefenceGroup defence;

    public float GetMaxHealth()
    {
        float baseHP = maxHealth.GetValue();
        float bonusHp = major.vitality.GetValue() * 5;

        return baseHP + bonusHp;
    }
}
