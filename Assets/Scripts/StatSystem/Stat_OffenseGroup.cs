using System;
using UnityEngine;

[Serializable]
public class Stat_OffenseGroup
{
    public Stat attackSpeed;

    //sat thuong vat ly
    public Stat damage;
    public Stat critPower;
    public Stat critChance;
    public Stat armorReduction;

    //elemental damage // sat thuong nguyen to
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightningDamage;
}
