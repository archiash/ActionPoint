using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum StatType { Main, Sub }

public class Character : MonoBehaviour
{
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

                        
                        
                    }
                }
                
            }else
            {
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
                }
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

    [SerializeField]CharacterClass characterClass;
    public CharacterClass Class
    {
        get { return characterClass; }
        set {

            RemoveModifier(this);

            characterClass = value; 
            if( value == CharacterClass.Magician)
            {
                float modValue = 5 + (0.5f * (level - 1));
                AddModifier(MainStatType.INT,new Modifier(modValue, this, ModifierType.Flat));
            }
            
        }
    }

    public enum CharacterClass
    {
        Adventurer,
        Magician,
        Rogue
    }

    public (float,DamageType) GetDamageAttack()
    {
        switch (Class)
        {
            case CharacterClass.Adventurer:
                return (status.PAtk.Value,DamageType.Physic);
            case CharacterClass.Magician:
                if (status.currentMP > 10)
                {
                    status.currentMP -= 10;
                    Debug.Log($"-10 MP Magician Normal Attack, Current MP = {status.currentMP}");
                    return (status.MAtk.Value, DamageType.Magic);
                }else
                {
                    Debug.Log($"Out of MP Magician Normal Attack, Current MP = {status.currentMP}");
                    return (status.PAtk.Value / 2, DamageType.Physic);
                }
            case CharacterClass.Rogue:
                return ((status.PAtk.Value * 0.75f) + (status.Spd.Value + 0.35f), DamageType.Physic);
        }

        return (status.PAtk.Value, DamageType.Physic);
    }
   
    public void ChangeClass()
    {

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

    private void Update()
    {
        if (status.currentHP > 0)
        {
            if (status.currentHP < status.HP.Value)
            {
                if (regenRate < status.HP.Value * (hpRegen + hpRegenLvBonus) / 100)
                    regenRate += status.HP.Value * (hpRegen + hpRegenLvBonus) / 100 * Time.deltaTime;
            }
            else
                regenRate = 0;
        }
        else
            regenRate = 0;
        
        status.currentHP += regenRate * Time.deltaTime;
        if (status.currentHP > status.HP.Value)
            status.currentHP = status.HP.Value;
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
    public MainStat GetMainStat(MainStatType type)
    {
        switch (type)
        {
            case MainStatType.STR:
                return status.STR;
            case MainStatType.DEX:
                return status.DEX;
            case MainStatType.AGI:
                return status.AGI;
            case MainStatType.INT:
                return status.INT;
            case MainStatType.CON:
                return status.CON;
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