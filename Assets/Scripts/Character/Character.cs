using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum StatType { Main, Sub }

public class Character : MonoBehaviour
{
    [SerializeField] List<ClassInfluence> influenceData;

    public static Character instance;
    [SerializeField] LevelBonus levelBonus;
    private bool isLoad = true;
    [SerializeField] UI_LevelBonusPanel pfBonusPanel;

    public int highestLevelBonus;

    public int Level
    {
        set
        {
            int setLevel = value;
            if (setLevel > 25) setLevel = 25;
            level = setLevel;

            if(isLoad)
            {
                isLoad = false;
                Debug.Log(isLoad);
                for (int i = 0; i < levelBonus.bonus.Count; i++)
                {
                    if (level >= levelBonus.bonus[i].requireLevel)
                    {
                        /*
                        Debug.Log(levelBonus.bonus[i].requireLevel);
                        if (levelBonus.bonus[i].bonusType == LvBonusType.pointRate) {
                            PointManager.instance.actionPerSecLvBonus += levelBonus.bonus[i].value;
                            if (highestLevelBonus < levelBonus.bonus[i].requireLevel)
                            {
                                highestLevelBonus = levelBonus.bonus[i].requireLevel;
                                UI_LevelBonusPanel panel = Instantiate(pfBonusPanel); //สร้าง Bonus Panel Object
                                string text = $"อัตราการเพิ่ม Point +{levelBonus.bonus[i].value}/s";
                                panel.ShowText(text);
                            }
                        }
                        if (levelBonus.bonus[i].bonusType == LvBonusType.healRate) {
                            hpRegenLvBonus += levelBonus.bonus[i].value;
                            if (highestLevelBonus < levelBonus.bonus[i].requireLevel)
                            {
                                highestLevelBonus = levelBonus.bonus[i].requireLevel;
                                UI_LevelBonusPanel panel = Instantiate(pfBonusPanel); //สร้าง Bonus Panel Object
                                string text = $"อัตราการฟื้นฟูเพิ่ม +{levelBonus.bonus[i].value}%/s";
                                panel.ShowText(text);
                            }
                        }

                        */
                        
                    }
                }
                
            }else
            {
                /*
                for (int i = 0; i < levelBonus.bonus.Count; i++)
                {
                    if (level == levelBonus.bonus[i].requireLevel)
                    {
                        
                        if (levelBonus.bonus[i].bonusType == LvBonusType.pointRate)
                        {
                            PointManager.instance.actionPerSecLvBonus += levelBonus.bonus[i].value;
                            UI_LevelBonusPanel panel = Instantiate(pfBonusPanel); //สร้าง Bonus Panel Object
                            string text = $"อัตราการเพิ่ม Point +{levelBonus.bonus[i].value}/s";
                            panel.ShowText(text);
                            highestLevelBonus = levelBonus.bonus[i].requireLevel;
                        }
                        if (levelBonus.bonus[i].bonusType == LvBonusType.healRate)
                        {
                            hpRegenLvBonus += levelBonus.bonus[i].value;   
                            UI_LevelBonusPanel panel = Instantiate(pfBonusPanel); //สร้าง Bonus Panel Object
                            string text = $"อัตราการฟื้นฟูเพิ่ม +{levelBonus.bonus[i].value}%/s";
                            panel.ShowText(text);
                            highestLevelBonus = levelBonus.bonus[i].requireLevel;
                        }
                    }
                }*/
            }           
        }
        get { return level; }
    }
    public bool isFullHP
    {
        get { return status.currentHP == status.HP.Value; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
        }
    }

    public int level = 1;
    public float exp = 0;

    public int statusPoint = 0;

    public Status status;

    [Range(1,100)]
    public int hpRegen;
    public float hpRegenLvBonus;

    public float regenRate;

    public Equipment weapon;
    public Equipment head;
    public Equipment body;
    public Equipment arms;
    public Equipment legs;
    public Equipment accessory;

    public List<LearnedSkill> learnedSkill = new List<LearnedSkill>();
    public List<Skill> currentSkill = new List<Skill>();

    public UpgradeLevel upgradeLevel;

    [SerializeField] CharacterClass characterClass;
    [SerializeField] ClassInfluence classInfluence;

    public ClassInfluence ClassInfluence
    {
        set
        {
            classInfluence = value;
            //print($"Change to {value.className}");
            for(int i = 0;i < 5;i++)
            {
                StatusUpgrade.SubModify[] subModifies = classInfluence.subModifies[i];

                foreach (StatusUpgrade.SubModify mod in subModifies)
                {
                    Stat a = (Stat)(status.GetType().GetField(mod.subType.ToString()).GetValue(status));

                    foreach (Stat.MainModifier mainModifier in a.mainModifiers)
                    {
                        if ((int)mainModifier.type == i)
                            mainModifier.modifier = mod.value;
                    }
                }
            }
        }
    }
    public CharacterClass Class
    {
        get { return characterClass; }
        set {
            RemoveModifier(this);
            ClassInfluence = influenceData[(int)value];
            characterClass = value; 
        }
    }

    public enum CharacterClass
    {
        Adventurer,
        Magician,
        Rogue,
        Defender
    }

    public (float,DamageType) GetDamageAttack()
    {
        switch (Class)
        {
            case CharacterClass.Adventurer:
                return (status.PAtk.Value * 1.1f,DamageType.Physic);
            case CharacterClass.Magician:
                if (status.currentMP >= 5 + status.MP.Value * 0.1f)
                {
                    status.currentMP -= 5 + status.MP.Value * 0.1f;
                    Debug.Log($"Magician Normal Attack, Current MP = {status.currentMP}");
                    return (status.MAtk.Value * 1.5f + 0.15f * status.MP.Value, DamageType.Magic);
                }else
                {
                    Debug.Log($"Out of MP Magician Normal Attack, Current MP = {status.currentMP}");
                    return (status.PAtk.Value / 2, DamageType.Physic);
                }
            case CharacterClass.Rogue:
                return ((status.PAtk.Value * 0.7f) + (status.Spd.Value + 0.35f), DamageType.Physic);
            case CharacterClass.Defender:
                AddModifier(SubStatType.PDef, new Modifier(2, this, ModifierType.Percentage, Modifier.ModifierTime.Step, 20, "DefenderClass", 10));
                AddModifier(SubStatType.MDef, new Modifier(2, this, ModifierType.Percentage, Modifier.ModifierTime.Step, 20, "DefenderClass", 10));
                return ((status.PAtk.Value * 0.8f) + Mathf.Min(status.HP.Value * 0.10f, 0.3f * status.PAtk.Value), DamageType.Physic);
        }

        return (status.PAtk.Value, DamageType.Physic);
    }
   
    public void RemoveModifier(Modifier modifier)
    {
        foreach (SubStatType statType in Enum.GetValues(typeof(SubStatType)))
        {
            Stat stat = (Stat)(status.GetType().GetField(statType.ToString()).GetValue(status));
            stat.RemoveModifier(modifier);
        }
        foreach (MainStatType statType in Enum.GetValues(typeof(MainStatType)))
        {
            MainStat stat = (MainStat)(status.GetType().GetField(statType.ToString()).GetValue(status));
            stat.RemoveModifier(modifier);
        }
    }
    public void RemoveModifier(object source)
    {
        foreach (SubStatType statType in Enum.GetValues(typeof(SubStatType)))
        {
            Stat stat = (Stat)(status.GetType().GetField(statType.ToString()).GetValue(status));
            stat.RemoveModifier(source);
        }
        foreach (MainStatType statType in Enum.GetValues(typeof(MainStatType)))
        {
            MainStat stat = (MainStat)(status.GetType().GetField(statType.ToString()).GetValue(status));
            stat.RemoveModifier(source);
        }
    }
    public void AddModifier(MainStatType statType, Modifier modifier)
    {
        MainStat stat = (MainStat)(status.GetType().GetField(statType.ToString()).GetValue(status));
        stat.AddModifier(modifier);
    }
    public void AddModifier(SubStatType statType, Modifier modifier)
    {
        Stat stat = (Stat)(status.GetType().GetField(statType.ToString()).GetValue(status));
        stat.AddModifier(modifier);
    }

    public void Equip(Equipment item,bool fromSave = false)
    {
        if (Inventory.instance.RemoveItem(item) || fromSave)
        {                            
            Equipment previousItem;            
            previousItem = Character.instance.GetEquipmentPart(item.part);
            Character.instance.EquipmentChange(item);            
            if (previousItem != null)              
            {                  
                Inventory.instance.GetItem(previousItem);                    
                previousItem.Unequip(this);            
            }          
            item.Equip(this);
        }
    }
    public void Unequip(Equipment item)
    {
        if (Inventory.instance.HaveEmptySpace())
        {
            item.Unequip(this);
            UnequipPart(item.part);
            Inventory.instance.GetItem(item);
        }
    }

    public bool LearnSkill(Skill skill,object source)
    {
        for(int i = 0; i<learnedSkill.Count; i++)
        {
            if(learnedSkill[i].skill == skill)
            {
                Debug.Log("Already Learned");
                return false;
            }
        }

        learnedSkill.Add(new LearnedSkill(skill,source));
        return true;
    }

    public void UnlearnSkill(object source)
    {
        for (int i = 0; i < learnedSkill.Count; i++)
        {
            if (learnedSkill[i].source == source)
            {
                learnedSkill.RemoveAt(i);
            }
        }
    }

    public void EquipmentChange(Equipment item)
    {
        switch (item.part)
        {
            case EquipmentPart.Head:
                head = item;
                break;
            case EquipmentPart.Body:
                body = item;
                break;
            case EquipmentPart.Arms:
                arms = item;
                break;
            case EquipmentPart.Legs:
                legs = item;
                break;
            case EquipmentPart.Weapon:
                weapon = item;
                break;
            case EquipmentPart.Accessory:
                accessory = item;
                break;
        }
    }
    public void UnequipPart(EquipmentPart part)
    {
        switch (part)
        {
            case EquipmentPart.Head:
                head = null;
                break;
            case EquipmentPart.Body:
                body = null;
                break;
            case EquipmentPart.Arms:
                arms = null;
                break;
            case EquipmentPart.Legs:
                legs = null;
                break;
            case EquipmentPart.Weapon:
                weapon = null;
                break;
            case EquipmentPart.Accessory:
                accessory = null;
                break;
        }
    }
    public void LevelUp()
    {if (level < 25)
        {
            if (exp >= Mathf.RoundToInt(30 * Mathf.Pow(Level, 2) + (50 * (Level - 1))))
            {
                exp -= Mathf.RoundToInt(30 * Mathf.Pow(Level, 2) + (50 * (Level - 1)));
                Level++;
                statusPoint++;
                Class = Class;
            }
        }
    }
    public void GetExp(int amount)
    {
        if (level < 25)
        {
            exp += amount;
            LevelUp();
        }
    }
    public Equipment GetEquipmentPart(EquipmentPart part)
    {
        switch (part)
        {
            case EquipmentPart.Weapon:
                return weapon;
            case EquipmentPart.Head:
                return head;
            case EquipmentPart.Body:
                return body;
            case EquipmentPart.Arms:
                return arms;
            case EquipmentPart.Legs:
                return legs;
            case EquipmentPart.Accessory:
                return accessory;
        }
        return null;
    }
}

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(Character))]
public class CharacterDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUILayout.PropertyField(property.FindPropertyRelative("health"), true);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label);
    }
}

#endif