using System.Collections.Generic;
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

    public override int price
    {
        get
        {
            return (int)(sellPrice * (1 + (enchantment / 5f)));
        }
    }

    public void Equip(Character character)
    {
        foreach(EquipmentModifier mod in modifiers)
        {
            float amount = mod.amount;

            if (mod.isPowerEffect)
            {
                amount *= powerPercent / 100f;

                if (mod.isEnchantEffect)
                    amount *= (1 + (enchantment * 0.2f));
            }
            if (mod.modifierType == StatType.Main)
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
            desc = itemDes + " \n";

        foreach (EquipmentModifier mod in modifiers)
        {
            float displayValue = mod.amount;

            if (mod.isPowerEffect)
            {
                displayValue *= powerPercent / 100f;

                if (mod.isEnchantEffect)
                    displayValue *= (1 + (enchantment * 0.2f));
            }
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
                            desc += $"พลังชีวิต + {displayValue}";
                        else
                            desc += $"พลังชีวิต {displayValue}";
                        break;
                    case SubStatType.MP:
                        if (mod.amount >= 0)
                            desc += $"มานา + {displayValue}";
                        else
                            desc += $"มานา {displayValue}";
                        break;
                    case SubStatType.PAtk:
                        if (mod.amount >= 0)
                            desc += $"โจมตี + {displayValue}";
                        else
                            desc += $"โจมตี {displayValue}";
                        break;
                    case SubStatType.PDef:
                        if (mod.amount >= 0)
                            desc += $"ป้องกัน + {displayValue}";
                        else
                            desc += $"ป้องกัน {displayValue}";
                        break;
                    case SubStatType.MAtk:
                        if (mod.amount >= 0)
                            desc += $"เวทย์ + {displayValue}";
                        else
                            desc += $"เวทย์ {displayValue}";
                        break;
                    case SubStatType.MDef:
                        if (mod.amount >= 0)
                            desc += $"ต้านเวทย์ + {displayValue}";
                        else
                            desc += $"ต้านเวทย์ {displayValue}";
                        break;
                    case SubStatType.Spd:
                        if (mod.amount >= 0)
                            desc += $"ความเร็ว + {displayValue}";
                        else
                            desc += $"ความเร็ว {displayValue}";
                        break;
                    case SubStatType.Hit:
                        if (mod.amount >= 0)
                            desc += $"เเม่นยำ + {displayValue}";
                        else
                            desc += $"เเม่นยำ {displayValue}";
                        break;
                    case SubStatType.Eva:
                        if (mod.amount >= 0)
                            desc += $"หลบหลีก + {displayValue}";
                        else
                            desc += $"หลบหลีก {displayValue}";
                        break;
                    case SubStatType.Crate:
                        if (mod.amount >= 0)
                            desc += $"โอกาสคริ + {displayValue}";
                        else
                            desc += $"โอกาสคริ {displayValue}";
                        break;
                    case SubStatType.Cdmg:
                        if (mod.amount >= 0)
                            desc += $"ความเสียหายคริ + {displayValue}";
                        else
                            desc += $"ความเสียหายคริ {displayValue}";
                        break;
                }
            }

            if(mod.type == ModifierType.Pecentage)
            {
                desc += "% ";
            }else
            {
                desc += " ";
            }

            if (!mod.isPowerEffect)
            {
                desc += "**";            
            }else
            {
                if (!mod.isEnchantEffect)
                {
                    desc += "*";
                }
            }

            desc += "\n";
        }
        return desc;
    }
    public string GetDesc(int percent)
    {
        string desc = "";
        if(itemDes != "")
            desc = itemDes + " \n";
        foreach (EquipmentModifier mod in modifiers)
        {
            float displayValue = mod.amount;

            if (mod.isPowerEffect)
                displayValue *= percent / 100f;

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
                            desc += $"พลังชีวิต + {displayValue}";
                        else
                            desc += $"พลังชีวิต {displayValue}";
                        break;
                    case SubStatType.MP:
                        if (mod.amount >= 0)
                            desc += $"มานา + {displayValue}";
                        else
                            desc += $"มานา {displayValue}";
                        break;
                    case SubStatType.PAtk:
                        if (mod.amount >= 0)
                            desc += $"โจมตี + {displayValue}";
                        else
                            desc += $"โจมตี {displayValue}";
                        break;
                    case SubStatType.PDef:
                        if (mod.amount >= 0)
                            desc += $"ป้องกัน + {displayValue}";
                        else
                            desc += $"ป้องกัน {displayValue}";
                        break;
                    case SubStatType.MAtk:
                        if (mod.amount >= 0)
                            desc += $"เวทย์ + {displayValue}";
                        else
                            desc += $"เวทย์ {displayValue}";
                        break;
                    case SubStatType.MDef:
                        if (mod.amount >= 0)
                            desc += $"ต้านเวทย์ + {displayValue}";
                        else
                            desc += $"ต้านเวทย์ {displayValue}";
                        break;
                    case SubStatType.Spd:
                        if (mod.amount >= 0)
                            desc += $"ความเร็ว + {displayValue}";
                        else
                            desc += $"ความเร็ว {displayValue}";
                        break;
                    case SubStatType.Hit:
                        if (mod.amount >= 0)
                            desc += $"เเม่นยำ + {displayValue}";
                        else
                            desc += $"เเม่นยำ {displayValue}";
                        break;
                    case SubStatType.Eva:
                        if (mod.amount >= 0)
                            desc += $"หลบหลีก + {displayValue}";
                        else
                            desc += $"หลบหลีก {displayValue}";
                        break;
                    case SubStatType.Crate:
                        if (mod.amount >= 0)
                            desc += $"โอกาสคริ + {displayValue}";
                        else
                            desc += $"โอกาสคริ {displayValue}";
                        break;
                    case SubStatType.Cdmg:
                        if (mod.amount >= 0)
                            desc += $"ความเสียหายคริ + {displayValue}";
                        else
                            desc += $"ความเสียหายคริ {displayValue}";
                        break;
                }
            }

            if (mod.type == ModifierType.Pecentage)
            {
                desc += "% ";
            }
            else
            {
                desc += " ";
            }

            if (!mod.isPowerEffect)
            {
                desc += "**";
            }
            else
            {
                if (!mod.isEnchantEffect)
                {
                    desc += "*";
                }
            }

            desc += "\n";
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
    public ModifierType type;
    public int amount;
    public bool isEnchantEffect = true;
    public bool isPowerEffect = true;
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
        var modTypeRect = new Rect(position.x + 300, position.y, 100, position.height -25);
        EditorGUI.PropertyField(statRect, property.FindPropertyRelative("modifierType"), GUIContent.none);
        if(property.FindPropertyRelative("modifierType").enumValueIndex == 0)
        {
            EditorGUI.PropertyField(typeRect, property.FindPropertyRelative("mainType"), GUIContent.none);
        }else
        {
            EditorGUI.PropertyField(typeRect, property.FindPropertyRelative("statType"), GUIContent.none);
        }
        EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("amount"), GUIContent.none);
        EditorGUI.PropertyField(modTypeRect, property.FindPropertyRelative("type"), GUIContent.none);
        statRect.y += 25;
        statRect.width = position.width / 2;
        EditorGUI.PropertyField(statRect, property.FindPropertyRelative("isPowerEffect"));
        if (property.FindPropertyRelative("isPowerEffect").boolValue == true)
        {
            statRect.x += statRect.width;
            EditorGUI.PropertyField(statRect, property.FindPropertyRelative("isEnchantEffect"));
        }

    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) + 30;
    }
}

[CustomEditor(typeof(Equipment))]
public class EquipmentEditor : Editor
{
    int enchantment;
    int percentage;

    public override void OnInspectorGUI()
    {
        Equipment t = (Equipment)target;
        Texture2D aTexture = t.icon? SpriteUtility.GetSpriteTexture(t.icon, false):null;
        GUILayout.Label(aTexture);
        DrawDefaultInspector();
        percentage = EditorGUILayout.IntSlider("PowerPercentage", percentage,0,10);
        enchantment = EditorGUILayout.IntSlider("Enchantment", enchantment, 0, 10);
        if (GUILayout.Button("Get Item"))
        {
           Equipment newItem = (Equipment)(t.GetCopyItem());
            newItem.powerPercent = 50 + (percentage * 5);
            newItem.enchantment = enchantment;
            Inventory.instance.GetItem(newItem);
        }
    }
}
#endif
