using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnchantMaterialBar : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI name;
    public TextMeshProUGUI desc;
    public TextMeshProUGUI powerPercent;

    public Image rarity;

    public Equipment equipment;
    public Enchantment enchantment;

    public virtual void CreateBar(Equipment equipment, Enchantment enchantment)
    {
        
        this.equipment = equipment;
        icon.sprite = equipment.icon;
        name.text = equipment.itemName + " +" + equipment.enchantment;
        desc.text = equipment.GetDesc();
        powerPercent.text = equipment.powerPercent.ToString();
        this.enchantment = enchantment;
        rarity.color = RarityColor.color(equipment.rarity);
    }

    public virtual void Select()
    {   if (EnchantMaterialSelect.instance.slot.slotItem != null)
            enchantment.RemoveMaterial(EnchantMaterialSelect.instance.slot.slotItem);

        if (true)
        {
            EnchantMaterialSelect.instance.slot.SetSlotItem(equipment);
            enchantment.AddMaterial(equipment);
            Debug.Log("Pass");
        }

        EnchantMaterialSelect.instance.ClosePanel();
    }
}
