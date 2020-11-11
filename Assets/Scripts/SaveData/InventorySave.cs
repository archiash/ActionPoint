using System;
using UnityEngine;

[Serializable]
public class ItemSlotSaveData
{
    public string itemID; //use to reference item
    public int amount; //if item is Equipment amount is enchantment
    public int powerpercentage = 0; //if item is not Equipment dont have powerpercent

    public ItemSlotSaveData(string id,int amount,int percent)
    {
        itemID = id;
        this.amount = amount;
        powerpercentage = percent;
    }
}

[Serializable]
public class ItemContainerSaveData
{
    public ItemSlotSaveData[] SavedSlots;
    public double money;
    public int inventoryLevel;

    public ItemContainerSaveData(int numItems, double money = 0,int level = 0)
    {
        SavedSlots = new ItemSlotSaveData[numItems];
        this.money = money;
        inventoryLevel = level;
    }
}

[Serializable]
public class PointSaveData
{
    public double point;
    public double pointPerSec;
    public DateTime lastExit;

    public PointSaveData(double amount, double pps,DateTime exit)
    {        
        point = amount;
        pointPerSec = pps;
        lastExit = exit;
    }

}

[Serializable]
public class CharacterSaveData
{
    public float lastHealth;
    public DateTime lastExit;
    public int level;
    public int skillPoint;
    public float exp;

    public int[] statLevel;

    public CharacterSaveData(float health,DateTime exit,int lvl,int sp,int[] sL, float xp)
    {        
        lastHealth = health;
        lastExit = exit;
        level = lvl;
        skillPoint = sp;
        statLevel = sL;
        exp = xp;
    }
}

[Serializable]
public class RaidSaveData
{
    public string raid;
    public float currentHP;
    public int round;
    public bool isRaid;

    public RaidSaveData(string rb,float hp,int round,bool isRaiding)
    {
        raid = rb;
        currentHP = hp;
        this.round = round;
        isRaid = isRaiding;
    }
}