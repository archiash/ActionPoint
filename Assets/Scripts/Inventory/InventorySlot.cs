using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI amount;
    public TextMeshProUGUI power;
    public Image rarity;

    public StackItem item;


    private void Update()
    {
        if (item != null)
        {
            amount.text = item.amount.ToString();
            if(item.item is Equipment) 
            {
                amount.enabled = false;
            }else
            {
                amount.enabled = true;
            }
        }           
    }

    

    public void AddItem(StackItem newItem)
    {        
        item = newItem;
        rarity.color = RarityColor(newItem.item);
        icon.sprite = item.item.icon;
        icon.enabled = true;    
    }

    public void ClearSlot()
    {
        item = null;
        rarity.color = Color.white;
        icon.enabled = false;
        icon.sprite = null;
        amount.enabled = false;
    }

    public void OnSlotButton()
    {
        if (item != null)
            ItemDetailPanel.instance.ShowItem(item);

    }

    public static Color RarityColor(Item item)
    {
        switch (item.rarity)
        {
            case Rarity.Common:
                return Color.white;
            case Rarity.Uncommon:
                return Color.green;
        }
        return Color.white;
    }
}
