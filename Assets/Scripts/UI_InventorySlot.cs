using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI amount;
    public TextMeshProUGUI power;
    public Image[] rarity;

    public StackItem item;

    public void Init(StackItem newItem)
    {
        item = newItem;
        icon.sprite = item.item.icon;
        foreach (Image i in rarity) i.color = RarityColor.color(item.item.rarity); 
        if(item.item is Equipment)
        {
            amount.text = "+" + ((Equipment)item.item).enchantment.ToString();
            power.enabled = true;
            power.text = ((Equipment)item.item).powerPercent.ToString() + "%";
        }else
        {
            amount.text = item.amount.ToString();
            power.enabled = false;
        }
    }

    public void ShowOnPanel()
    {
        UI_InventoryManager.instance.ShowItemDetail(item);
        //if (item != null)
       //     ItemDetailPanel.instance.ShowItem(item);
    }

    public void ClearSlot()
    {
        item = null;
        foreach (Image i in rarity) i.color = Color.white;
        icon.enabled = false;
        power.enabled = false;
        amount.enabled = false;
    }

}
