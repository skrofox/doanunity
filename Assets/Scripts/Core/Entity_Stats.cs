using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public Stat maxHealth;
    public Stat vitality; //moi diem giup ban tang 5 mau toi da


    public float GetMaxHealth()
    {
        float baseHP = maxHealth.GetValue();
        float bonusHp = vitality.GetValue() * 5;

        return baseHP + bonusHp;
    }
}
