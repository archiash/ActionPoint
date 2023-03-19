using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EquipmentSlectBar : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemStatusDesc;
    public TextMeshProUGUI itemPercent;
    public TextMeshProUGUI itemEnchantment;

    [SerializeField] Image itemRarity;
    [SerializeField] Image itemRarityFrame;

    Equipment equipment;
    
    public void Init(Equipment equipment)
    {
        this.equipment = equipment;
        itemIcon.sprite = equipment.icon;
        itemName.text = equipment.itemName;
        itemStatusDesc.text =  equipment.GetDesc(false,false);
        itemPercent.text = $"{equipment.powerPercent}%";
        itemEnchantment.text = $"+{equipment.enchantment}";
        itemRarity.color = RarityColor.color(equipment.rarity);
        itemRarityFrame.color = RarityColor.color(equipment.rarity);
    }

    public void Equip()
    {
        UIManager.Instance.equipmentSelectPanel.CompareNewItem(equipment);
        //Character.instance.Equip(equipment);
        //UIManager.Instance.equipmentSelectPanel.ClosePanel();
    }

}
