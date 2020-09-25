using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSaveManager : MonoBehaviour
{
    [SerializeField] ItemDatabase itemDatabase;

    private const string InventoryFileName = "Inventory";
    private const string EquipmentFileName = "Equipment";
    private const string PointFileName = "Point";
    private const string CharacterFileName = "Character";

    public void LoadInventory(Inventory inventory = null)
    {
        ItemContainerSaveData saveData = ItemSaveIO.LoadItems(InventoryFileName);
        if (saveData == null) return;

        if (inventory == null)
            inventory = Inventory.instance;

        if(saveData.money != null)
            inventory.setMoney = saveData.money;

        inventory.items.Clear();
        for(int i =0;i < saveData.SavedSlots.Length;i++)
        {
            StackItem stackItem = new StackItem();
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
    public void SaveInventory(Inventory inventory = null)
    {
        if (inventory == null)
            SaveItem(Inventory.instance.items,Inventory.instance.getMoney, InventoryFileName);
        else
            SaveItem(inventory.items,inventory.getMoney, InventoryFileName);
    }
    private void SaveItem(IList<StackItem> stackItems,double money,string fileName)
    {
        var saveData = new ItemContainerSaveData(stackItems.Count,money);

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
        double pointWhileExit = (DateTime.Now - saveData.lastExit).TotalSeconds * saveData.pointPerSec / 2;
        if (pointWhileExit > 3600)
            pointWhileExit = 3600;

        pointManager.GetActionPerSec = saveData.pointPerSec;
        pointManager.GetActionPoint = saveData.point + pointWhileExit;       
    }
    public void SavePoint()
    {
        PointManager pointManager = PointManager.instance;

        PointSaveData saveData = new PointSaveData(pointManager.GetActionPoint,pointManager.GetActionPerSec,DateTime.Now);
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
                equipment.enchantment = saveData.SavedSlots[i].amount;
                equipment.powerPercent = saveData.SavedSlots[i].powerpercentage;
                character.Equip(equipment,true);
            }
        
    }
    public void SaveCharacterData(Character character =null)
    {
        if (character == null)
        {
            character = Character.instance;
        }

        var saveData = new CharacterSaveData(character.status.currentHP, DateTime.Now);
        ItemSaveIO.SaveCharacter(saveData, CharacterFileName);
    }
    public void LoadCharacterData(Character character = null)
    {
        if (character == null)
        {
            character = Character.instance;
        }
        CharacterSaveData saveData = ItemSaveIO.LoadCharacter(CharacterFileName);
        if (saveData == null)
            return;
        double healWhileExit = (DateTime.Now - saveData.lastExit).TotalSeconds / 2;
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
