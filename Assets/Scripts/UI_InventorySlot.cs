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
    public Image rarity;

    public StackItem item;

    public void AddItem(StackItem newItem)
    {
        item = newItem;
        icon.sprite = item.item.icon;
        rarity.color = RarityColor(item.item);
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
        if (item != null)
            ItemDetailPanel.instance.ShowItem(item);
    }

    public void ClearSlot()
    {
        item = null;
        rarity.color = Color.white;
        icon.enabled = false;
        power.enabled = false;
        amount.enabled = false;
    }

    public Color RarityColor(Item item)
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
