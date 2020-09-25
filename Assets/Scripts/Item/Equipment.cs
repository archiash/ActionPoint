﻿using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Sprites;
#endif
using UnityEngine;

public enum EquipmentPart {Weapon,Head,Body,Arms,Legs, Accessory }

[CreateAssetMenu(menuName = "Item/Equipment", fileName = "New Equipment")]
public class Equipment : Item
{
    public int enchantment = 0;
    public int powerPercent = 100;
    public EquipmentPart part;
    public List<EquipmentModifier> modifiers = new List<EquipmentModifier>();

    public void Equip(Character character)
    {
        foreach(EquipmentModifier mod in modifiers)
        {
            //float amount = mod.amount * ((powerPercent + (enchantment * 20))/100f);

            float amount = (mod.amount * powerPercent/100f) * (1 + (enchantment * 0.2f));
            if(mod.modifierType == StatType.Main)
            {
                switch(mod.mainType)
                {
                    case MainStatType.STR:
                        character.status.STR.AddModifier(new Modifier(amount,this, mod.type));
                        break;
                    case MainStatType.DEX:
                        character.status.DEX.AddModifier(new Modifier(amount,this, mod.type));
                        break;
                    case MainStatType.AGI:
                        character.status.AGI.AddModifier(new Modifier(amount, this, mod.type));
                        break;
                    case MainStatType.INT:
                        character.status.INT.AddModifier(new Modifier(amount, this, mod.type));
                        break;
                    case MainStatType.CON:
                        character.status.CON.AddModifier(new Modifier(amount, this, mod.type));
                        break;
                }
            }else if(mod.modifierType == StatType.Sub)
            {
                switch(mod.statType)
                {
                    case SubStatType.HP:
                        character.status.HP.AddModifier(new Modifier(amount, this,mod.type));
                        break;
                    case SubStatType.MP:
                        character.status.MP.AddModifier(new Modifier(amount, this, mod.type));
                        break;
                    case SubStatType.PAtk:
                        character.status.PAtk.AddModifier(new Modifier(amount, this, mod.type));
                        break;
                    case SubStatType.PDef:
                        character.status.PDef.AddModifier(new Modifier(amount, this, mod.type));
                        break;
                    case SubStatType.MAtk:
                        character.status.MAtk.AddModifier(new Modifier(amount, this, mod.type));
                        break;
                    case SubStatType.MDef:
                        character.status.MDef.AddModifier(new Modifier(amount, this, mod.type));
                        break;
                    case SubStatType.Spd:
                        character.status.Spd.AddModifier(new Modifier(amount, this, mod.type));
                        break;
                    case SubStatType.Hit:
                        character.status.Hit.AddModifier(new Modifier(amount, this, mod.type));
                        break;
                    case SubStatType.Eva:
                        character.status.Eva.AddModifier(new Modifier(amount, this, mod.type));
                        break;
                    case SubStatType.Crate:
                        character.status.Crate.AddModifier(new Modifier(amount, this, mod.type));
                        break;
                    case SubStatType.Cdmg:
                        character.status.Cdmg.AddModifier(new Modifier(amount, this, mod.type));
                        break;
                }
            }
        }
    }
    public void Unequip(Character character)
    {
        character.status.STR.RemoveModifier(this);
        character.status.DEX.RemoveModifier(this);
        character.status.AGI.RemoveModifier(this);
        character.status.INT.RemoveModifier(this);
        character.status.CON.RemoveModifier(this);

        character.status.HP.RemoveModifier(this);
        character.status.MP.RemoveModifier(this);
        character.status.PAtk.RemoveModifier(this);
        character.status.PDef.RemoveModifier(this);
        character.status.MAtk.RemoveModifier(this);
        character.status.MDef.RemoveModifier(this);
        character.status.Spd.RemoveModifier(this);
        character.status.Hit.RemoveModifier(this);
        character.status.Eva.RemoveModifier(this);
        character.status.Crate.RemoveModifier(this);
        character.status.Cdmg.RemoveModifier(this);
    }

    public override string GetDesc(bool fulldesc = true)
    {
        string desc = "";
        if(fulldesc)
            desc = itemDes + " ";

        foreach (EquipmentModifier mod in modifiers)
        {
            //float displayValue = mod.amount * ((powerPercent + (enchantment * 20)) / 100f);
            float displayValue = (mod.amount * powerPercent/100f) * (1 + (enchantment * 0.2f));

            if (mod.modifierType == StatType.Main)
            {
                switch (mod.mainType)
                {
                    case MainStatType.STR:                                                   
                        if (mod.amount >= 0)
                            desc += $"STR + {displayValue}";
                        else
                            desc += $"STR {displayValue}";
                        break;
                    case MainStatType.DEX:
                        if (mod.amount >= 0)
                            desc += $"DEX + {displayValue}";
                        else
                            desc += $"DEX {displayValue}";
                        break;                        
                    case MainStatType.AGI:
                        if (mod.amount >= 0)
                            desc += $"AGI + {displayValue}";
                        else
                            desc += $"AGI {displayValue}";
                        break;
                    case MainStatType.INT:
                        if (mod.amount >= 0)
                            desc += $"INT + {displayValue}";
                        else
                            desc += $"INT {displayValue}";
                        break;
                    case MainStatType.CON:
                        if (mod.amount >= 0)
                            desc += $"CON + {displayValue}";
                        else
                            desc += $"CON {displayValue}";
                        break;
                }
            }
            else if (mod.modifierType == StatType.Sub)
            {
                switch (mod.statType)
                {
                    case SubStatType.HP:
                        if (mod.amount >= 0)
                            desc += $"HP + {displayValue}";
                        else
                            desc += $"HP {displayValue}";
                        break;
                    case SubStatType.MP:
                        if (mod.amount >= 0)
                            desc += $"MP + {displayValue}";
                        else
                            desc += $"MP {displayValue}";
                        break;
                    case SubStatType.PAtk:
                        if (mod.amount >= 0)
                            desc += $"Attack + {displayValue}";
                        else
                            desc += $"Attack {displayValue}";
                        break;
                    case SubStatType.PDef:
                        if (mod.amount >= 0)
                            desc += $"Defense + {displayValue}";
                        else
                            desc += $"Defense {displayValue}";
                        break;
                    case SubStatType.MAtk:
                        if (mod.amount >= 0)
                            desc += $"Magic + {displayValue}";
                        else
                            desc += $"Magic {displayValue}";
                        break;
                    case SubStatType.MDef:
                        if (mod.amount >= 0)
                            desc += $"MagicResist + {displayValue}";
                        else
                            desc += $"MagicResist {displayValue}";
                        break;
                    case SubStatType.Spd:
                        if (mod.amount >= 0)
                            desc += $"Speed + {displayValue}";
                        else
                            desc += $"Speed {displayValue}";
                        break;
                    case SubStatType.Hit:
                        if (mod.amount >= 0)
                            desc += $"Hit + {displayValue}";
                        else
                            desc += $"Hit {displayValue}";
                        break;
                    case SubStatType.Eva:
                        if (mod.amount >= 0)
                            desc += $"Eva + {displayValue}";
                        else
                            desc += $"Eva {displayValue}";
                        break;
                    case SubStatType.Crate:
                        if (mod.amount >= 0)
                            desc += $"Cri rate + {displayValue}";
                        else
                            desc += $"Cri rate {displayValue}";
                        break;
                    case SubStatType.Cdmg:
                        if (mod.amount >= 0)
                            desc += $"Cri dmg + {displayValue}";
                        else
                            desc += $"Cri dmg {displayValue}";
                        break;
                }
            }

            if(mod.type == Modifier.ModifierType.Pecentage)
            {
                desc += "% ";
            }else
            {
                desc += " ";
            }
            
        }
        return desc;
    }
    public string GetDesc(int percent)
    {
        string desc = itemDes + " ";
        foreach (EquipmentModifier mod in modifiers)
        {
            float displayValue = mod.amount * (percent / 100f);

            if (mod.modifierType == StatType.Main)
            {
                switch (mod.mainType)
                {
                    case MainStatType.STR:
                        if (mod.amount >= 0)
                            desc += $"STR + {displayValue}";
                        else
                            desc += $"STR {displayValue}";
                        break;
                    case MainStatType.DEX:
                        if (mod.amount >= 0)
                            desc += $"DEX + {displayValue}";
                        else
                            desc += $"DEX {displayValue}";
                        break;
                    case MainStatType.AGI:
                        if (mod.amount >= 0)
                            desc += $"AGI + {displayValue}";
                        else
                            desc += $"AGI {displayValue}";
                        break;
                    case MainStatType.INT:
                        if (mod.amount >= 0)
                            desc += $"INT + {displayValue}";
                        else
                            desc += $"INT {displayValue}";
                        break;
                    case MainStatType.CON:
                        if (mod.amount >= 0)
                            desc += $"CON + {displayValue}";
                        else
                            desc += $"CON {displayValue}";
                        break;
                }
            }
            else if (mod.modifierType == StatType.Sub)
            {
                switch (mod.statType)
                {
                    case SubStatType.HP:
                        if (mod.amount >= 0)
                            desc += $"HP + {displayValue}";
                        else
                            desc += $"HP {displayValue}";
                        break;
                    case SubStatType.MP:
                        if (mod.amount >= 0)
                            desc += $"MP + {displayValue}";
                        else
                            desc += $"MP {displayValue}";
                        break;
                    case SubStatType.PAtk:
                        if (mod.amount >= 0)
                            desc += $"Attack + {displayValue}";
                        else
                            desc += $"Attack {displayValue}";
                        break;
                    case SubStatType.PDef:
                        if (mod.amount >= 0)
                            desc += $"Defense + {displayValue}";
                        else
                            desc += $"Defense {displayValue}";
                        break;
                    case SubStatType.MAtk:
                        if (mod.amount >= 0)
                            desc += $"Magic + {displayValue}";
                        else
                            desc += $"Magic {displayValue}";
                        break;
                    case SubStatType.MDef:
                        if (mod.amount >= 0)
                            desc += $"MagicResist + {displayValue}";
                        else
                            desc += $"MagicResist {displayValue}";
                        break;
                    case SubStatType.Spd:
                        if (mod.amount >= 0)
                            desc += $"Speed + {displayValue}";
                        else
                            desc += $"Speed {displayValue}";
                        break;
                    case SubStatType.Hit:
                        if (mod.amount >= 0)
                            desc += $"Hit + {displayValue}";
                        else
                            desc += $"Hit {displayValue}";
                        break;
                    case SubStatType.Eva:
                        if (mod.amount >= 0)
                            desc += $"Eva + {displayValue}";
                        else
                            desc += $"Eva {displayValue}";
                        break;
                    case SubStatType.Crate:
                        if (mod.amount >= 0)
                            desc += $"Cri rate + {displayValue}";
                        else
                            desc += $"Cri rate {displayValue}";
                        break;
                    case SubStatType.Cdmg:
                        if (mod.amount >= 0)
                            desc += $"Cri dmg + {displayValue}";
                        else
                            desc += $"Cri dmg {displayValue}";
                        break;
                }
            }

            if (mod.type == Modifier.ModifierType.Pecentage)
            {
                desc += "% ";
            }
            else
            {
                desc += " ";
            }

        }
        return desc;
    }
    public override Item GetCopyItem()
    {
        Equipment newItem = Instantiate(this);
        newItem.powerPercent = Random.Range(10, 21) * 5;
        return newItem;
    }
}

[System.Serializable]
public class EquipmentModifier
{
    
    public StatType modifierType;
    public MainStatType mainType;
    public SubStatType statType;
    public Modifier.ModifierType type;
    public int amount;
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(EquipmentModifier))]
public class EquipmentModifierDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        var statRect = new Rect(position.x, position.y, 100, position.height -25);
        var typeRect = new Rect(position.x + 100, position.y, 100, position.height -25);
        var amountRect = new Rect(position.x + 200, position.y, 100, position.height -25);
        var modTypeRect = new Rect(position.x + 200, position.y + 25, 100, position.height -25);
        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();
        EditorGUI.PropertyField(statRect, property.FindPropertyRelative("modifierType"), GUIContent.none);
        if(property.FindPropertyRelative("modifierType").enumValueIndex == 0)
        {
            EditorGUI.PropertyField(typeRect, property.FindPropertyRelative("mainType"), GUIContent.none);
        }else
        {
            EditorGUI.PropertyField(typeRect, property.FindPropertyRelative("statType"), GUIContent.none);
        }
        EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("amount"), GUIContent.none);
        EditorGUILayout.EndHorizontal();
        EditorGUI.PropertyField(modTypeRect, property.FindPropertyRelative("type"), GUIContent.none);
        EditorGUILayout.EndVertical();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) + 30;
    }
}

[CustomEditor(typeof(Equipment))]
public class EquipmentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Equipment t = (Equipment)target;
        Texture2D aTexture = SpriteUtility.GetSpriteTexture(t.icon, false);
        GUILayout.Label(aTexture);
        DrawDefaultInspector();
        if(GUILayout.Button("Get Item"))
        {
            Inventory.instance.GetItem(t.GetCopyItem());
        }
        if (GUILayout.Button("Equip Item"))
        {
            Item newItem = t.GetCopyItem();
            Inventory.instance.GetItem(newItem);
            Character.instance.Equip((Equipment)newItem);
        }
    }
}
#endif
