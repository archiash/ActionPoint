using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UI_InventoryDetail : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] Image[] rarity;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemDesc;

    [SerializeField] TextMeshProUGUI percentage;
    [SerializeField] TextMeshProUGUI enchantment;

    [SerializeField] TextMeshProUGUI itemType;

    [SerializeField] TextMeshProUGUI sellPrice;

    [SerializeField] TMP_InputField sellAmount; // <---- work in progress ---------------------------------

    public StackItem item;

    public void ShowDetail(StackItem item)
    {
        this.item = item;
        itemDesc.text = item.item.GetDesc(true,false);
        itemName.text = item.item.itemName;
        itemIcon.sprite = item.item.icon;
        foreach (Image i in rarity) i.color = RarityColor.color(item.item.rarity);
        sellPrice.text = $"Sell Price: {item.item.price}";
        if (item.item is Equipment)
        {
            percentage.text = $"{((Equipment)item.item).powerPercent}%";
            enchantment.text = $"+{((Equipment)item.item).enchantment}";
            itemType.text = $"{((Equipment)item.item).part}";
            sellAmount.gameObject.SetActive(false);
        }
        else
        {
            sellAmount.gameObject.SetActive(true);
            percentage.text = $"Amount:";
            enchantment.text = item.amount.ToString();
            itemType.text = $"Material";
            sellAmount.text = "1";
        }
    }

    public void OnSellAmountChange()
    {
        if ( Int32.Parse(sellAmount.text) > item.amount){
            sellAmount.text = item.amount.ToString();
        }
    }

    public void OnSellButton()
    {
        int currentSellAmount = 1;
        if (sellAmount.text != "") {
            currentSellAmount = Int32.Parse(sellAmount.text); 
        }

        if(currentSellAmount == item.amount)
        {
            Inventory.instance.SellItem(item.item, currentSellAmount);
            gameObject.SetActive(false);
        }
        else
        {
            Inventory.instance.SellItem(item.item, currentSellAmount);
            enchantment.text = item.amount.ToString();
        }
        
        
    }
}
