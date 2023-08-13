using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ItemSaveManager : MonoBehaviour
{
    [SerializeField] ItemDatabase itemDatabase;
    [SerializeField] FollowerDatabase followerDatabase;

    private const string InventoryFileName = "Inventory";
    private const string EquipmentFileName = "Equipment";
    private const string PointFileName = "Point";
    private const string CharacterFileName = "Character";
    private const string RaidFileName = "Raid";
    private const string FollowerFileName = "Follower";
    private const string TimeStageFileName = "/TimeStage.json";

    public void SaveTimeStage()
    {
        TimeStageSaveData tssd = new TimeStageSaveData(UIManager.Instance.timeStageManager.stagesProgression);
        string tssdText = JsonUtility.ToJson(tssd);
        File.WriteAllText(Application.persistentDataPath + TimeStageFileName, tssdText);
    }

    public void LoadTimeStage()
    {
        string filePath = Application.persistentDataPath + TimeStageFileName;
        TimeStageSaveData tssd = null;
        if (File.Exists(filePath))
        {
            string textData = File.ReadAllText(filePath);
            tssd = JsonUtility.FromJson<TimeStageSaveData>(textData);          
        }
        UIManager.Instance.timeStageManager.LoadSave(tssd);

    }

    public void SaveFollower()
    {
        FollowerTeam ft = FollowerTeam.instance;
        FollowerSaveData[] followerSaveDatas = new FollowerSaveData[ft.followerList.Count];
        for(int i = 0; i < ft.followerList.Count; i++)
        {
            Debug.Log($"Save Follower {ft.followerList[i].followerID}");
            followerSaveDatas[i] = new FollowerSaveData(ft.followerList[i].followerID, ft.followerList[i].followerLevel);
        }
        int[] followerTeam = new int[3] {-1, -1, -1};
        List<Follower> flTeam = ft.followerTeam;
        for (int i = 0; i < flTeam.Count; i++)
        {
            //print($"Save Team: {flTeam[i].followerName}");
            followerTeam[i] = flTeam[i].followerID;
            //print(followerTeam[i]);
        }
        FollowerListSaveData saveData = new FollowerListSaveData(followerSaveDatas, followerTeam);
        ItemSaveIO.SaveItems(saveData, FollowerFileName);
    }

    public void LoadFollwer()
    {
        FollowerTeam ft = FollowerTeam.instance;
        FollowerListSaveData saveData = ItemSaveIO.LoadItems<FollowerListSaveData>(FollowerFileName);
        if (saveData != null)
        {
            for (int i = 0; i < saveData.followerSaveData.Length; i++)
            {
                //print(saveData.followerSaveData[i].followerID.ToString());
                Follower follower = followerDatabase.GetFollowerCopy(saveData.followerSaveData[i].followerID);
                follower.followerLevel = saveData.followerSaveData[i].level;
                ft.followerList.Add(follower);
            }

            for (int i = 0; i < 3; i++)
            {
                if (saveData.followerInTeam[i] > 0)
                {
                    //print($"Load Team: saveData.followerInTeam[i].ToString()");
                    Follower fl = ft.GetFollowerInListByID(saveData.followerInTeam[i]);
                    if (fl != null)
                        ft.ChangeFollower(fl, i + 1);
                    else
                        ft.DisableFollowerImage(i + 1);
                }
                else
                {
                    ft.DisableFollowerImage(i + 1);
                }
                
            }
        }
        else
        {
            ft.DisableFollowerImage(1);
            ft.DisableFollowerImage(2);
            ft.DisableFollowerImage(3);
        }
    }

    public void SaveRaid(string id,float hp,int time,bool isRaid)
    {
        var saveData = new RaidSaveData(id, hp, time,isRaid);
        ItemSaveIO.SaveItems(saveData, RaidFileName);
    }

    public RaidSaveData LoadRaid()
    {
        return ItemSaveIO.LoadItems<RaidSaveData>(RaidFileName);
    }

    public void LoadInventory(Inventory inventory = null)
    {
        ItemContainerSaveData saveData = ItemSaveIO.LoadItems(InventoryFileName);
        if (saveData == null) return;

        if (inventory == null)
            inventory = Inventory.instance;

        if(saveData.money != null)
            inventory.setMoney = saveData.money;

        if (saveData.inventoryLevel > 0)
            inventory.level = saveData.inventoryLevel;
        else
            inventory.level = 1;

        inventory.items.Clear();
        for(int i =0;i < saveData.SavedSlots.Length;i++)
        {
            StackItem stackItem = new StackItem(null,0);
            ItemSlotSaveData saveSlot = saveData.SavedSlots[i];

            if(saveSlot == null)
            {
                stackItem.item = null;
                stackItem.amount = 0;
            }else
            {
                stackItem.item = itemDatabase.GetItemCopy(saveSlot.itemID);
                if(stackItem.item is Equipment)
                {
                    stackItem.amount = 1;
                    ((Equipment)stackItem.item).enchantment = saveSlot.amount;
                    ((Equipment)stackItem.item).powerPercent = saveSlot.powerpercentage;
                } else
                {
                    stackItem.amount = saveSlot.amount;
                }

            }

            inventory.items.Add(stackItem);
        }
    }
    public void SaveInventory()
    {
        Inventory inventory = Inventory.instance;
        SaveItem(inventory.items, inventory.Money, inventory.level, InventoryFileName);
    }
    private void SaveItem(IList<StackItem> stackItems,double money,int level,string fileName)
    {
        var saveData = new ItemContainerSaveData(stackItems.Count, money, level);

        for(int i = 0; i < saveData.SavedSlots.Length; i++)
        {
            StackItem stackItem = stackItems[i];

            if(stackItem == null)
            {
                saveData.SavedSlots[i] = null;
            }else
            {
                if(stackItem.item is Equipment)
                {
                    saveData.SavedSlots[i] = new ItemSlotSaveData(stackItem.item.ID, ((Equipment)stackItem.item).enchantment, ((Equipment)stackItem.item).powerPercent);
                }else
                {
                    saveData.SavedSlots[i] = new ItemSlotSaveData(stackItem.item.ID, stackItem.amount, 100);
                }              
            }

            ItemSaveIO.SaveItems(saveData, fileName);
        }
    }
    public void LoadPoint(PointManager pointManager = null)
    {
        PointSaveData saveData = ItemSaveIO.LoadItems<PointSaveData>(PointFileName);
        if (pointManager == null)
        {
            pointManager = PointManager.instance;
        }
        pointManager.restLevel = 1;
        if (saveData == null)
            return;
        double pointWhileExit = (DateTime.UtcNow - saveData.lastExit).TotalSeconds * saveData.pointPerSec / 2;
        if (pointWhileExit > 3600)
            pointWhileExit = 3600;

        
        if (saveData.restLevel > pointManager.restLevel)
        {
            pointManager.restLevel = saveData.restLevel;
        }
     
        pointManager.GetActionPerSec = 1;
        pointManager.GetActionPoint = saveData.point + pointWhileExit;       
    }
    public void SavePoint()
    {
        PointManager pointManager = PointManager.instance;

        PointSaveData saveData = new PointSaveData(pointManager.GetActionPoint,pointManager.GetActionPerSec,DateTime.UtcNow,pointManager.restLevel);
        ItemSaveIO.SaveItems(saveData, PointFileName);
    }
    public void SaveEquipment(Character character = null)
    {
        if(character == null)
        {
            character = Character.instance;
        }
        var saveData = new ItemContainerSaveData(6);
        for(int i = 0;i< 6;i++)
        {
            Equipment equipment = character.GetEquipmentPart((EquipmentPart)i);
            if (equipment == null)
            {
                saveData.SavedSlots[i] = null;
            }
            else
            {
                saveData.SavedSlots[i] = new ItemSlotSaveData(equipment.ID,equipment.enchantment,equipment.powerPercent);
            }

        }

        ItemSaveIO.SaveItems(saveData, EquipmentFileName);
    }
    public void LoadEquipment(Character character = null)
    {
        if (character == null)
        {
            character = Character.instance;
        }
        ItemContainerSaveData saveData = ItemSaveIO.LoadItems(EquipmentFileName);
        if (saveData == null)
            return;

            for (int i = 0; i< saveData.SavedSlots.Length;i++)
            {
                if (saveData.SavedSlots[i] == null)
                    continue;

                Equipment equipment = (Equipment)itemDatabase.GetItemCopy(saveData.SavedSlots[i].itemID);
            if (equipment == null)
                continue;
                equipment.enchantment = saveData.SavedSlots[i].amount;
                equipment.powerPercent = saveData.SavedSlots[i].powerpercentage;
                character.Equip(equipment,true);
            }
        
    }
    public void SaveCharacterData(Character character = null)
    {

        StatusUpgrade status = StatusUpgrade.instance;
        if (character == null)
        {            
            character = Character.instance;
        }

        var saveData = new CharacterSaveData(character.status.currentHP, DateTime.UtcNow,character.Level,new int[5] {status.STR,status.DEX,status.AGI,status.INT,status.CON},character.exp,character);
        ItemSaveIO.SaveCharacter(saveData, CharacterFileName);
    }
    public void LoadCharacterData(Character character = null)
    {
        if (character == null)
        {
            character = Character.instance;
        }
        StatusUpgrade status = StatusUpgrade.instance;
        CharacterSaveData saveData = ItemSaveIO.LoadCharacter(CharacterFileName);
        if (saveData == null)
            return;

        if (saveData.level > 0)
        {
            character.highestLevelBonus = saveData.currentLevelBonus;
            character.Level = saveData.level;
            character.statusPoint = saveData.skillPoint;
            character.exp = saveData.exp;
            if(saveData.statLevel[0] > 15)
            {
                character.statusPoint += 15 - saveData.statLevel[0];
                status.STR = 15;
            }else status.STR = saveData.statLevel[0];

            if (saveData.statLevel[1] > 15)
            {
                character.statusPoint += 15 - saveData.statLevel[1];
                status.DEX = 15;
            }
            else status.DEX = saveData.statLevel[1];

            if (saveData.statLevel[2] > 15)
            {
                character.statusPoint += 15 - saveData.statLevel[2];
                status.AGI = 15;
            }
            else status.AGI = saveData.statLevel[2];

            if (saveData.statLevel[3] > 15)
            {
                character.statusPoint += 15 - saveData.statLevel[3];
                status.INT = 15;
            }
            else status.INT = saveData.statLevel[3];

            if (saveData.statLevel[4] > 15)
            {
                character.statusPoint += 15 - saveData.statLevel[4];
                status.CON = 15;
            }
            else status.CON = saveData.statLevel[4];
            character.Class = saveData.characterClass;
            
        }
        

        double healWhileExit = (DateTime.UtcNow - saveData.lastExit).TotalSeconds / 2;
        if (healWhileExit > 100)
            healWhileExit = 100;

        if (saveData.lastHealth > 0)
        {
            character.status.currentHP = (float)(saveData.lastHealth + (character.status.HP.Value * healWhileExit / 100f));
        }
        else
            character.status.currentHP = (float)(saveData.lastHealth);

        if (character.status.currentHP > character.status.HP.Value)
            character.status.currentHP = character.status.HP.Value;

    }
}
