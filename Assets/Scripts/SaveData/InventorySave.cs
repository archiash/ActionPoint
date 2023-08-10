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
    public int restLevel;
    public PointSaveData(double amount, double pps,DateTime exit,int restLevel)
    {        

        point = amount;
        pointPerSec = pps;
        lastExit = exit;
        this.restLevel = restLevel;
    }

}

[Serializable]
public class CharacterSaveData
{
    public float lastHealth;
    public DateTime lastExit;
    public int level;
    public int currentLevelBonus;
    public int skillPoint;
    public float exp;

    public int[] statLevel;
    public Character.CharacterClass characterClass;

    public CharacterSaveData(float health,DateTime exit,int lvl,int[] sL, float xp,Character character)
    {        
        lastHealth = health;
        lastExit = exit;
        level = lvl;
        skillPoint = lvl - 1 - sL[0] - sL[1] - sL[2] - sL[3] - sL[4];
        statLevel = sL;
        exp = xp;
        characterClass = character.Class;
        currentLevelBonus = character.highestLevelBonus;
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

[Serializable]
public class FollowerSaveData
{
    public int followerID;
    public int level;

    public FollowerSaveData(int ID, int level)
    {
        followerID = ID;
        this.level = level;
    }
}

[Serializable]
public class FollowerListSaveData
{
    public FollowerSaveData[] followerSaveData;
    public int[] followerInTeam;

    public FollowerListSaveData(FollowerSaveData[] followerDataList, int[] followerInTeam)
    {
        followerSaveData = followerDataList;
        this.followerInTeam = followerInTeam;
    }
}