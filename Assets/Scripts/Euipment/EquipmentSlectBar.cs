using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EquipmentSlectBar : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI name;
    public TextMeshProUGUI stat;
    public TextMeshProUGUI power;

    [SerializeField] Image rarity;

    Equipment equipment;
    
    public void CreateBar(Equipment equipment)
    {
        this.equipment = equipment;
        icon.sprite = equipment.icon;
        name.text = equipment.itemName + " +" + equipment.enchantment;
        stat.text =  equipment.GetDesc(false);
        power.text = equipment.powerPercent.ToString() + "%";
        rarity.color = RarityColor.color(equipment.rarity);
    }

    public void Equip()
    {
        Character.instance.Equip(equipment);
        EquipmentSelectPanel.instance.ClosePanel();
    }

}
