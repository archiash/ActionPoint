using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnchantTranferBar : MonoBehaviour
{
    public Equipment equipment;
    [SerializeField] Image icon;
    [SerializeField] Image rarity;
    [SerializeField] TextMeshProUGUI equipmentName;
    [SerializeField] TextMeshProUGUI percentage;
    [SerializeField] TextMeshProUGUI status;

    [SerializeField] bool isReciver;

    public void Init(Equipment equipment,bool isReciver)
    {
        this.isReciver = isReciver;
        this.equipment = equipment;
        icon.sprite = equipment.icon;
        rarity.color = RarityColor.color(equipment.rarity);
        equipmentName.text = equipment.itemName + " +" + equipment.enchantment;
        percentage.text = equipment.powerPercent + "%";
        status.text = equipment.GetDesc(false);
    }

    public void OnSelectEquipment()
    {
        if (isReciver) {
            EnchantmentTranfer.instance.SetReciver(equipment);
            gameObject.SetActive(false);
        }
        else
        {
            EnchantmentTranfer.instance.SetGiver(equipment);
            gameObject.SetActive(false);
        }
    }
}
