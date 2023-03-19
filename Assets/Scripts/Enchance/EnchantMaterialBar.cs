using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnchantMaterialBar : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDesc;
    public TextMeshProUGUI powerPercent;
    public TextMeshProUGUI enchantmentText;

    public Image itemRarity;
    public Image itemRarityFrame;

    [HideInInspector] public Equipment equipment;
    [HideInInspector] public Enchantment enchantment;

    public virtual void CreateBar(Equipment equipment, Enchantment enchantment)
    {

        this.equipment = equipment;
        itemIcon.sprite = equipment.icon;
        itemName.text = equipment.itemName;
        enchantmentText.text = $"+{equipment.enchantment}";
        itemDesc.text = equipment.GetDesc();
        powerPercent.text = $"{equipment.powerPercent}%";
        this.enchantment = Enchantment.instance;
        itemRarity.color = RarityColor.color(equipment.rarity);
        itemRarityFrame.color = RarityColor.color(equipment.rarity);
    }

    public virtual void Select()
    {   
        if (EnchantMaterialSelect.instance.slot.slotItem != null)
            enchantment.RemoveMaterial(EnchantMaterialSelect.instance.slot.slotItem);

        EnchantMaterialSelect.instance.slot.SetSlotItem(equipment);
        enchantment.AddMaterial(equipment);
        Debug.Log("Pass");
        

        EnchantMaterialSelect.instance.ClosePanel();
    }
}
