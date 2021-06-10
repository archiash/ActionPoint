using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSelectPanel : MonoBehaviour
{
    public Equipment currentItem;

    [SerializeField] TextMeshProUGUI currentItemName;
    [SerializeField] TextMeshProUGUI currentItemStat;
    [SerializeField] TextMeshProUGUI currentItemPower;
    [SerializeField] Image currentItemIcon;
    [SerializeField] Image rarity;

    [SerializeField] Transform parent;
    [SerializeField] EquipmentSlectBar slectBarPrefab;

    public static EquipmentSelectPanel instance;

    [SerializeField] GameObject panel;

    private void Awake()
    {
        instance = this;
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
    }    

    public void ShowPanel(EquipmentPart part)
    {
        currentItem = Character.instance.GetEquipmentPart(part);
        if(currentItem != null)
        {
            rarity.enabled = true;
            currentItemName.enabled = true;
            currentItemStat.enabled = true;
            currentItemPower.enabled = true;
            currentItemIcon.enabled = true;
            rarity.color =  RarityColor.color(currentItem.rarity);
            currentItemName.text = currentItem.itemName + " +" + currentItem.enchantment;
            currentItemStat.text = currentItem.GetDesc(false);
            currentItemPower.text = currentItem.powerPercent.ToString() + "%";
            currentItemIcon.sprite = currentItem.icon;
        }else
        {
            rarity.enabled = false;
            currentItemPower.enabled = false;
            currentItemName.enabled = false;
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
                    newBar.CreateBar(equipment);
                }
            }
        }

        panel.SetActive(true);

    }
    public void Unequip()
    {
        if(currentItem != null)
            Character.instance.Unequip(currentItem);
        
        ClosePanel();
    }
}
