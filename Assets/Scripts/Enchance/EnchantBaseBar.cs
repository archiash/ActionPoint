using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnchantBaseBar : EnchantMaterialBar
{
    public override void CreateBar(Equipment equipment,Enchantment enchantment)
    {
        this.equipment = equipment;
        icon.sprite = equipment.icon;
        name.text = equipment.itemName + " +" + equipment.enchantment;
        desc.text = equipment.GetDesc();
        powerPercent.text = equipment.powerPercent.ToString();
        this.enchantment = enchantment;
    }

    public override void Select()
    {
        enchantment.SelectEnchant(equipment);
        EnchantMaterialSelect.instance.ClosePanel();
    }
}
