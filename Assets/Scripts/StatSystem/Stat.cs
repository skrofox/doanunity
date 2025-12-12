using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField] private float baseValue;
    [SerializeField] private List<StatModifier> modifiers = new List<StatModifier>();

    private bool needToBeReCalculated = true;
    private float finalValue;

    public float GetValue()
    {
        if (needToBeReCalculated)
        {
            finalValue = GetFinalValue();
            needToBeReCalculated = false;
        }

        return finalValue;
    }

    public void AddModifier(float value, string source)
    {
        StatModifier modToAdd = new StatModifier(value, source);
        modifiers.Add(modToAdd);
        needToBeReCalculated = true;
    }


    //removemodifier
    //thanh kiem pro vjp
    //+4 dame
    //+10 crit chance
    public void RemoveModifier(string source)
    {
        //xoa tat ca cac bo sung, neu dieu kien trong ngoac la dung
        modifiers.RemoveAll(modifiers => modifiers.source == source); //tuc la, modifiers.source == source => xoa
        needToBeReCalculated = true;

        //same way, su dung foreach thay vi viet nhu tren
        //foreach (var modifier in modifiers)
        //{

        //}
    }

    private float GetFinalValue()
    {
        float finalValue = baseValue;

        foreach (var modifier in modifiers)
        {
            finalValue += modifier.value;
        }

        return finalValue;
    }

    public void SetBaseValue(float value) => baseValue = value;
}

[Serializable]
public class StatModifier
{
    public float value; //
    public string source; // buff or item or etc

    public StatModifier(float value, string source)
    {
        this.value = value;
        this.source = source;
    }
}