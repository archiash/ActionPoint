using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
   
    [SerializeField] private double money;
    [SerializeField] private float moneyBuff;
    [SerializeField] private double storageSize;
    [SerializeField] private double storageCurrent;

    public double getStorageSize { get { return storageSize; } }
    public double getStorageCurr { get { return storageCurrent; } }
    public double Money { get { return Mathf.RoundToInt((float)money); } }
    public double setMoney { set { money = value; } }
    public double getMoneyBuff { get { return moneyBuff; } }
    
    public void GetMoney(int amount)
    {
        money += amount + Mathf.RoundToInt(amount * moneyBuff);
    }

    public void UseMoney(int amount)
    {
        money -= amount;
    }

    public void UseMoneyPercent(int amount)
    {
        money *= ((100 - amount) / 100f);
    }


    private void Awake()
    {
    
        instance = this;
    }

    public int level { get { return inventoryLevel; }
        set { inventoryLevel = value;
            space = GetSpaceFormLevel(level);
            if (onItemChange != null)
                onItemChange.Invoke();
        }
    }

    public int GetSpaceFormLevel(int level)
    {
        return 20 + (level - 1) * 5;
    }

    [SerializeField]int inventoryLevel = 1;
    public int space = 10;

    public List<StackItem> items = new List<StackItem>();

    public bool GetItem(Item newItem,int amount = 1)
    {
        if (!HaveEmptySpace())
        {
            Debug.Log("Inventory Full");
            return false;
        }

        foreach (StackItem item in items)
        {
            if (newItem.ID == item.item.ID && newItem.itemType != ItemType.Equipment)
            {
                item.amount += amount;
                if (onItemChange != null)
                    onItemChange.Invoke();
                return true;
            }
        }
        
        StackItem newStack = new StackItem(newItem,amount);
        items.Add(newStack);

        if (onItemChange != null)
            onItemChange.Invoke();

        return true;
    }

    public bool RemoveItem(Item item,int amount = 1)
    {
        foreach(StackItem stack in items)
        {
            if(stack.item == item)
            {
                if(amount <= stack.amount)
                {
                    stack.amount -= amount;
                    IsOutStack(item);
                    return true;
                }
            }
        }
        return false;
    }
    public void UseItem(Item useItem)
    {        
        foreach(StackItem item in items)
        {
            if(item.item == useItem)
            {
                if(item.item.UseItem(Character.instance.status))
                    item.amount--;

                break;
            }
        }

        IsOutStack(useItem);


        if (onItemChange != null)
            onItemChange.Invoke();
    }
    public void UseAsMaterial(Item useItem,int useAmount)
    {
        foreach (StackItem item in items)
        {
            if (useItem.ID == item.item.ID)
            {
                item.amount -= useAmount;
            }

            if (item.amount <= 0)
            {
                items.Remove(item);
                break;
            }
        }

        if (onItemChange != null)
            onItemChange.Invoke();
    }

    public void UseAsMaterial(Equipment equipment)
    {
        foreach (StackItem item in items)
        {
            if (equipment == item.item)
            {
                items.Remove(item);
                break;
            }
        }

        if (onItemChange != null)
            onItemChange.Invoke();
    }
    public void SellItem(Item sellItem)
    {
        foreach(StackItem item in items)
        {
            if(item.item == sellItem)
            {
                item.amount--;
                money += sellItem.price;
            }

            if(item.amount <= 0)
            {
                items.Remove(item);
                break;
            }
        }

        if (onItemChange != null)
            onItemChange.Invoke();
    }
    public void SellItem(Item sellItem,int amount)
    {
        foreach (StackItem item in items)
        {
            if (item.item == sellItem)
            {
                item.amount -= amount;
                money += sellItem.price * amount;
            }

            if (item.amount <= 0)
            {
                items.Remove(item);
                break;
            }
        }

        if (onItemChange != null)
            onItemChange.Invoke();
    }

    public delegate void ItemChange();
    public ItemChange onItemChange;
    public bool HaveEmptySpace()
    {
        return items.Count < space;
    }
    public bool IsOutStack(Item checkItem)
    {
        foreach(StackItem item in items )
        {
            if(item.item == checkItem && item.amount <= 0)
            {
                items.Remove(item);
                return true;
            }                                  
        }
        return false;
    }
}
