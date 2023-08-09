using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Modifier;

public static class EquipmentComparer
{
    public class CompareStat
    {
        public string statID;
        public string statName;
        public string statMod;
        public string subfix;
        public float beforeValue;
        public float afterValue;

        public CompareStat(string ID, string statName, ModifierType modifierType, float beforeValue, float afterValue, bool UpgradeAble, bool PercentEffect)
        {
            statID = ID;
            this.statName = statName;
            statMod = (modifierType == ModifierType.Percentage) ? "%" : "";
            subfix = UpgradeAble ? "" : "U";
            subfix += PercentEffect ? "" : "P";
            this.beforeValue= beforeValue ;
            this.afterValue= afterValue ;           
        }

        public string StatDesc
        {
            get 
            {
                string before = (beforeValue > 0) ? $"+{beforeValue}" : $"{beforeValue}";
                string after = (afterValue > 0) ? $"+{afterValue}" : $"{afterValue}";
                return $"{statName} {before}{statMod} > {after}{statMod} {subfix}";                  
            }
        }
    }

    public static CompareStat[] GetCompareStatus(Equipment oldEquipment, Equipment newEquipment)
    {
        List<CompareStat> compareStats= new List<CompareStat>();
        if (oldEquipment != null)
        {
            for (int i = 0; i < oldEquipment.modifiers.Count; i++)
            {
                (string, string) IDName = GetStatIDAndName(oldEquipment.modifiers[i]);
                float amount = oldEquipment.modifiers[i].amount;
                if (oldEquipment.modifiers[i].isPowerEffect) amount *= oldEquipment.powerPercent / 100f;
                if (oldEquipment.modifiers[i].isEnchantEffect) amount *= (1 + (oldEquipment.enchantment * KeyValue.keyValues["EnchantPower"]));
                compareStats.Add(new CompareStat(IDName.Item1, IDName.Item2, oldEquipment.modifiers[i].type, amount, 0, oldEquipment.modifiers[i].isEnchantEffect, oldEquipment.modifiers[i].isPowerEffect));
            }
        }
        for(int j = 0; j < newEquipment.modifiers.Count; j++)
        {
            bool compared = false;
            (string, string) IDName = GetStatIDAndName(newEquipment.modifiers[j]);
            for (int k = 0; k < compareStats.Count; k++)
            {
                if (IDName.Item1 == compareStats[k].statID)
                {
                    float amount = newEquipment.modifiers[j].amount;
                    if (newEquipment.modifiers[j].isPowerEffect) amount *= newEquipment.powerPercent / 100f;
                    if (newEquipment.modifiers[j].isEnchantEffect) amount *= (1 + (newEquipment.enchantment * KeyValue.keyValues["EnchantPower"]));
                    compareStats[k].afterValue = amount;

                    compared= true;
                    break;
                }
            }

            if (!compared)
            {
                float amount = newEquipment.modifiers[j].amount;
                if (newEquipment.modifiers[j].isPowerEffect) amount *= newEquipment.powerPercent / 100f;
                if (newEquipment.modifiers[j].isEnchantEffect) amount *= (1 + (newEquipment.enchantment * KeyValue.keyValues["EnchantPower"]));
                compareStats.Add(new CompareStat(IDName.Item1, IDName.Item2, newEquipment.modifiers[j].type, 0, amount, newEquipment.modifiers[j].isEnchantEffect, newEquipment.modifiers[j].isPowerEffect));
            }
        }

        return compareStats.ToArray();
    }



    public static string[] GetCompareStatusDesc(CompareStat[] comparedStatArray)
    {
        List<string> compareStats = new List<string>();
        for(int i = 0; i < comparedStatArray.Length; i++) {          
            if(i % 6 == 0)
            {
                compareStats.Add("");
            }
            int slot = Mathf.FloorToInt(i / 6);
            compareStats[slot] += comparedStatArray[i].StatDesc;
            if(i % 6 != 5)
            {
                compareStats[slot] += "\n";
            }
        }
        return compareStats.ToArray();
    }

    public static string GetCompareStatusDescMerge(CompareStat[] comparedStatArray)
    {
        string[] statDescArray = GetCompareStatusDesc(comparedStatArray);
        string mergeDesc = "";
        for(int i = 0; i < statDescArray.Length; i++)
        {
            mergeDesc += statDescArray[i];
            if(i != statDescArray.Length - 1)
            {
                mergeDesc += "\n";
            }
        }
        return mergeDesc;
    }

    public static (string,string) GetStatIDAndName(EquipmentModifier modifier)
    {
        string statName = (modifier.modifierType == StatType.Main) ? modifier.mainType.ToString() : modifier.statType.ToString();
        string statMod = (modifier.type == ModifierType.Percentage) ? "P" : "F";
        string subfix = (modifier.isEnchantEffect) ? "" : "U";
        subfix += (modifier.isPowerEffect) ? "" : "P";
        return ($"{statName}{statMod}{subfix}", statName);
    }
}
