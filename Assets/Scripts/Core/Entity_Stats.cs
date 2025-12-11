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

    public float GetEvasion()
    {
        float baseEvasion = defence.evasion.GetValue();
        float bonusEvasion = major.agility.GetValue() * .5f; // moi diem nhanh nhen (agility) cho 0.5% evasion(ne don).

        float totalEvasion = baseEvasion + bonusEvasion;

        float evasionCap = 30f; //gioi han evasion la 30%

        float finalEvasion = Mathf.Clamp(totalEvasion, 0, evasionCap);

        return finalEvasion;
    }
}
