using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum SubStatType { HP,MP,PAtk,PDef,MAtk,MDef,Spd,Hit,Eva,Crate,Cdmg,Pen,Neu,Cres}
public enum ModifierType
{
    Flat, Pecentage
}

[System.Serializable]
public class Stat
{                                   
    public float baseValue;

    public float Value { get { return CalculateFinalValue(); } }

    public List<MainModifier> mainModifiers = new List<MainModifier>();        
    [System.Serializable]
    public class MainModifier
    {
        public MainStatType type;
        public float modifier;
    }

    public float MainValueModifier()
    {
        float value = 0;
        foreach (MainModifier mainStat in mainModifiers)
        {
           value += Character.instance.GetMainStat(mainStat.type).Value * mainStat.modifier * (1 + Character.instance.GetMainStat(mainStat.type).upgradeModifier);
        }
        return value;
    }
    public float CalculateFinalValue()
    {
        float value = baseValue + MainValueModifier();
        for(int i = 0;i < modifiers.Count;i++)
        {
            if (modifiers[i].type == ModifierType.Flat)
                value += modifiers[i].value;
            else
                value *= 1 + (modifiers[i].value / 100);
        }

        return value;
    }
 
   public List<Modifier> modifiers = new List<Modifier>();
   public void AddModifier(Modifier modifier)
    {
        if (modifier.value != 0)
            modifiers.Add(modifier);

        modifiers.Sort(CompareModifierOrder);
    }
    public void RemoveModifier(object source)
    {
        for(int i = 0;i < modifiers.Count;i++)
        {
            if(modifiers[i].source == source)
            {
                modifiers.RemoveAt(i);
            }
        }
    }

    public void RemoveModifier(Modifier modifier)
    {
        for (int i = 0; i < modifiers.Count; i++)
        {
            if (modifiers[i] == modifier)
            {
                Debug.Log("Remove");
                modifiers.RemoveAt(i);
            }
        }

        /*
        foreach (Modifier mod in modifiers)
        {
            if (mod.type == modifier.type && mod.value == modifier.value)
            {
                modifiers.Remove(mod);
                break;
            }
            if(mod == modifier)
                modifiers.Remove(mod);
                break;
        }*/
        
    }
    private int CompareModifierOrder(Modifier a,Modifier b)
    {
        if(a.type < b.type)
        {
            return -1;
        }else if(a.type > b.type)
        {
            return 1;
        }
        return 0;
    }

    public static string SubToNormalName(SubStatType subStatType)
    {
        switch (subStatType)
        {
            case SubStatType.HP:
                return "พลังชีวิต";
            case SubStatType.MP:
                return "มานา";
            case SubStatType.PAtk:
                return "โจมตี";
            case SubStatType.PDef:
                return "ป้องกัน";
            case SubStatType.MAtk:
                return "เวทย์";
            case SubStatType.MDef:
                return "ต้านเวทย์";
            case SubStatType.Spd:
                return "ความเร็ว";
            case SubStatType.Hit:
                return "เเม่นยำ";                
            case SubStatType.Eva:
                return "หลบหลีก";
            case SubStatType.Crate:
                return "อัตราคริติคอล";    
            case SubStatType.Cdmg:
                return "ดาเมจคริติคอล";                
        }

        return "";
    }
}

[System.Serializable]
public class Modifier
{
    public enum ModifierTime
    {
        Equip,Hunt,Turn
    }

    public float value;
    public ModifierType type;
    public ModifierTime timeType;
    public int time;
    public readonly object source;

    public Modifier(float _value, object _source, ModifierType _type = ModifierType.Flat,ModifierTime _timeType = ModifierTime.Equip,int _time = int.MaxValue)
    {
        value = _value;
        type = _type;
        time = _time;
        timeType = _timeType;
        source = _source;
    }
}
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(Stat))]
public class StatDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var obj = property.serializedObject.targetObject;
        if (!(obj as Monster))
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
        else
        {
            EditorGUI.PropertyField(position, property.FindPropertyRelative("baseValue"), label);
        }

    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property);
    }
}

#endif
