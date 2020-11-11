using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class MainStat
{
    public int baseValue;
    public float upgradeModifier;

    public float Value { get { return CalculateFinalValue(); } }

    public int CalculateFinalValue()
    {
        float value = baseValue;
        for (int i = 0; i < modifiers.Count; i++)
        {
            if (modifiers[i].type == ModifierType.Flat)
                value += modifiers[i].value;
            else
                value *= (1 + (modifiers[i].value / 100));
        }

        return (int)value;
    }
    public int AllModifierValue()
    {

        float modifierValue = 0;
        modifiers.ForEach(x => modifierValue += x.value);
        return (int)modifierValue;
    }

    [SerializeField] private List<Modifier> modifiers = new List<Modifier>();
    public void AddModifier(Modifier modifier)
    {
        if (modifier.value != 0)
            modifiers.Add(modifier);

        modifiers.Sort(CompareModifierOrder);
    }

    public void RemoveModifier(object source)
    {
        for (int i = 0; i < modifiers.Count; i++)
        {
            if (modifiers[i].source == source)
            {
                modifiers.RemoveAt(i);
            }
        }
    }

    public void RemoveModifier(Modifier modifier)
    {
        if (modifier.value != 0)
        {
            foreach(Modifier mod in modifiers)
            {
                if(mod.type == modifier.type && mod.value == modifier.value)
                {
                    modifiers.Remove(modifier);
                    break;
                }
            }
        }            
    }

    private int CompareModifierOrder(Modifier a, Modifier b)
    {
        if (a.type < b.type)
        {
            return -1;
        }
        else if (a.type > b.type)
        {
            return 1;
        }
        return 0;
    }
}

public enum MainStatType
{
    STR,DEX,AGI,INT,CON
}
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(MainStat))]
public class MainStatDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var obj = property.serializedObject.targetObject;
        if(!(obj as Monster))
        {
            EditorGUI.PropertyField(position, property, label, true);
        }else
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