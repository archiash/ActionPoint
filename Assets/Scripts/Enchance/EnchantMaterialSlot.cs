using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnchantMaterialSlot : MonoBehaviour
{
    public Equipment slotItem;
    public Image itemIcon;
    public Image itemRarity;
    public Image itemRarityFrame;

    public TextMeshProUGUI percentText;
    public TextMeshProUGUI enchantText;
    public void SetSlotItem(Equipment newItem)
    {
        slotItem = newItem;
        itemIcon.sprite = slotItem.icon;
        itemIcon.enabled = true;
        itemRarity.color = RarityColor.color(newItem.rarity);
        itemRarityFrame.color = RarityColor.color(newItem.rarity);
        percentText.text = $"{newItem.powerPercent}%";
        enchantText.text = $"+{newItem.enchantment}";
    }

    public void OnSelect()
    {
        EnchantMaterialSelect.instance.GetFormSlot(this);
    }

}
