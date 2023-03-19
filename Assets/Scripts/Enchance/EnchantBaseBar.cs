using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnchantBaseBar : EnchantMaterialBar
{
    public override void CreateBar(Equipment equipment,Enchantment enchantment)
    {
        this.equipment = equipment;
        itemIcon.sprite = equipment.icon;
        itemName.text = equipment.itemName;
        enchantmentText.text = $"+{equipment.enchantment}";
        itemDesc.text = equipment.GetDesc();
        powerPercent.text = equipment.powerPercent.ToString();
        this.enchantment = Enchantment.instance;
        itemRarity.color = RarityColor.color(equipment.rarity);
        itemRarityFrame.color = RarityColor.color(equipment.rarity);
    }

    public override void Select()
    {
        enchantment.SelectEnchant(equipment);
        EnchantMaterialSelect.instance.ClosePanel();
    }
}
