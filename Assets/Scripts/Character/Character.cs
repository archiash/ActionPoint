using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEditor;

public enum StatType { Main, Sub }

public class Character : MonoBehaviour
{
    public static Character instance;

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
        status.currentHP = status.HP.Value;
        status.currentMP = status.MP.Value;
    }

    public int Level = 1;
    public float exp;

    public Status status;
    [Range(1,100)]

    public int hpRegen;
    public float regenRate;

    public Equipment weapon;
    public Equipment head;
    public Equipment body;
    public Equipment arms;
    public Equipment legs;
    public Equipment accessory;

    public List<Skill> learnedSkill = new List<Skill>();
    public List<Skill> currentSkill = new List<Skill>();

    public UpgradeLevel upgradeLevel;

    private void Update()
    {
        if (status.currentHP > 0)
        {
            if (status.currentHP < status.HP.Value)
            {
                if (regenRate < status.HP.Value * hpRegen / 100)
                    regenRate += status.HP.Value * hpRegen / 100 * Time.deltaTime;
            }
            else
                regenRate = 0;
        }
        else
            regenRate = 0;
        
        status.currentHP += regenRate * Time.deltaTime;

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
    public bool LearnSkill(Skill skill)
    {
        for(int i = 0; i<learnedSkill.Count; i++)
        {
            if(learnedSkill[i] == skill)
            {
                Debug.Log("Already Learned");
                return false;
            }
        }

        learnedSkill.Add(skill);
        return true;
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
    {
        if (exp >= Mathf.RoundToInt(30 * Mathf.Pow(Level, 2) - (50 * (Level - 1))))
        {
            exp -= Mathf.RoundToInt(30 * Mathf.Pow(Level, 2) - (50 * (Level - 1)));
            Level++;
            //skillPoint++;
        }
    }
    public void GetExp(int amount)
    {
        //exp += amount + Mathf.RoundToInt(amount * expBuff);
        LevelUp();
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
[CustomEditor(typeof(Character))]
public class CharacterEditor : Editor
{
    public bool ShowSetting;
    public override void OnInspectorGUI()
    {
        Character t = (Character)target;


        if(!Application.isPlaying)
            base.OnInspectorGUI();
        else
        {
            if (GUILayout.Button("OpenSetting"))
            {
                if (ShowSetting)
                    ShowSetting = false;
                else
                    ShowSetting = true;
            }

            if (ShowSetting)
                base.OnInspectorGUI();

            GUILayout.Label("HP: " + t.status.currentHP.ToString() + "/" + t.status.HP.Value.ToString());
            GUILayout.Label("MP: " + t.status.currentMP.ToString() + "/" + t.status.MP.Value.ToString());
            GUILayout.Label("Attack: " + t.status.PAtk.Value.ToString());
            GUILayout.Label("Defense: " + t.status.PDef.Value.ToString());
            GUILayout.Label("Magic: " + t.status.MAtk.Value.ToString());
            GUILayout.Label("Magic Resist: " + t.status.MDef.Value.ToString());
            GUILayout.Label("Speed: " + t.status.Spd.Value.ToString());
            GUILayout.Label("Hit: " + t.status.Hit.Value.ToString());
            GUILayout.Label("Eva: " + t.status.Eva.Value.ToString());
            GUILayout.Label("Critical Rate: " + t.status.Crate.Value.ToString());
            GUILayout.Label("Critical Damage: " + t.status.Cdmg.Value.ToString());

            GUILayout.Space(20);

            GUILayout.Label("Equipment");
            GUILayout.Label("Weapon: " + t.weapon?.itemName);
            GUILayout.Label("Head: " + t.head?.itemName);
            GUILayout.Label("Body: " + t.body?.itemName);
            GUILayout.Label("Arms: " + t.arms?.itemName);
            GUILayout.Label("Legs: " + t.legs?.itemName);
            GUILayout.Label("Accessory: " + t.accessory?.itemName);

            GUILayout.Label("Atk Mod Count: " + t.status.PAtk.modifiers.Count);

        }
    }
}
#endif