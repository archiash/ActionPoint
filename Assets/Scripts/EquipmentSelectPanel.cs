using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static EquipmentComparer;

public class EquipmentSelectPanel : MonoBehaviour
{
    public Equipment currentItem;
    public Equipment newItem;

    [SerializeField] TextMeshProUGUI currentItemStat;
    [SerializeField] TextMeshProUGUI currentItemPower;
    [SerializeField] TextMeshProUGUI currentItemEnchantment;
    [SerializeField] Image currentItemIcon;

    [SerializeField] TextMeshProUGUI newItemPower;
    [SerializeField] TextMeshProUGUI newItemEnchantment;
    [SerializeField] Image newItemIcon;

    [SerializeField] Image currentItemRarity;
    [SerializeField] Image newItemRarity;

    [SerializeField]
    TextMeshProUGUI[] compareStatusText;

    [SerializeField] Transform parent;
    [SerializeField] EquipmentSlectBar slectBarPrefab;

    [SerializeField] GameObject panel;
    public void ClosePanel()
    {
        panel.SetActive(false);
    }    

    public void ShowPanel(EquipmentPart part)
    {
        for (int i = 0; i < compareStatusText.Length; i++)
        {
            compareStatusText[i].text = "";
        }

        currentItem = Character.instance.GetEquipmentPart(part);

        newItemIcon.enabled = false;
        newItemPower.enabled = false;
        newItemRarity.enabled = false;
        newItemEnchantment.enabled = false;

        if (currentItem != null)
        {
            currentItemRarity.enabled = true;
            currentItemStat.enabled = true;
            currentItemPower.enabled = true;
            currentItemEnchantment.enabled = true;
            currentItemEnchantment.text = $"+{currentItem.enchantment}";
            currentItemIcon.enabled = true;
            currentItemRarity.color =  RarityColor.color(currentItem.rarity);
            //currentItemStat.text = currentItem.GetDesc(false);
            currentItemPower.text = currentItem.powerPercent.ToString() + "%";
            currentItemIcon.sprite = currentItem.icon;
        }else
        {
            currentItemEnchantment.enabled = false;
            currentItemRarity.enabled = false;
            currentItemPower.enabled = false;
            currentItemStat.enabled = false;
            currentItemIcon.enabled = false;
        }

        for(int i = 0;i< parent.childCount;i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }

        for(int i = 0; i < Inventory.instance.items.Count;i++)
        {
            if(Inventory.instance.items[i].item is Equipment)
            {
                Equipment equipment = (Equipment)Inventory.instance.items[i].item;
                if(equipment.part == part)
                {
                    EquipmentSlectBar newBar = Instantiate(slectBarPrefab, parent);
                    newBar.Init(equipment);
                }
            }
        }

        panel.SetActive(true);

    }

    public void CompareNewItem(Equipment newEquipment)
    {
        newItem = newEquipment;
        newItemIcon.enabled = true;
        newItemPower.enabled = true;
        newItemRarity.enabled = true;
        newItemEnchantment.enabled = true;
        newItemIcon.sprite = newEquipment.icon;
        newItemPower.text = $"{newItem.powerPercent}%";
        newItemRarity.color = RarityColor.color(newItem.rarity);
        newItemEnchantment.text = $"+{newItem.enchantment}";

        string[] compareStatus = GetCompareStatusDesc(GetCompareStatus(currentItem,newItem));
        for(int i = 0; i < compareStatus.Length;i++)
        {
            compareStatusText[i].enabled = true;
            compareStatusText[i].text = compareStatus[i];
        }
    }
    public void Unequip()
    {
        if(currentItem != null)
            Character.instance.Unequip(currentItem);
        
        ClosePanel();
    }

    public void Equip()
    {
        Character.instance.Equip(newItem);
        gameObject.SetActive(false);
    }
}
