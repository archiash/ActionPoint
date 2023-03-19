using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enchantment : MonoBehaviour
{
    [SerializeField] Sprite plusIcon;

    public Image image;
    public Equipment item;

    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemDesc;
    public TextMeshProUGUI enchantmentLevel;
    public TextMeshProUGUI powerPercentage;

    public static Enchantment instance;

    [SerializeField] Image itemRarity;
    [SerializeField] Image itemRarityFrame;

    public List<Equipment> materials = new List<Equipment>();
    [SerializeField] EnchantMaterialSlot[] materialSlots;

    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] Button button;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void SelectEnchant(Equipment equipment)
    {
        ClearMaterialSlot();
        materials.Clear();

        for (int i = 0; i < materialSlots.Length; i++)
        {
            materialSlots[i].gameObject.SetActive(equipment.enchantment >= i);
        }

        item = equipment;
        image.sprite = equipment.icon;
        itemName.text = equipment.itemName;
        enchantmentLevel.text = $"+{item.enchantment} > + {item.enchantment + 1}";
        powerPercentage.text = $"{item.powerPercent}%";
        costText.text = $"{CalculateCost(equipment)}$";

        itemRarity.color = RarityColor.color(equipment.rarity);
        itemRarityFrame.color = RarityColor.color(equipment.rarity);

        button.interactable = Condition(item);

        Equipment nextLevelEquipment = (Equipment)(equipment.GetCopyItem());
        nextLevelEquipment.powerPercent = equipment.powerPercent;
        nextLevelEquipment.enchantment = equipment.enchantment + 1;
        itemDesc.text = EquipmentComparer.GetCompareStatusDescMerge(EquipmentComparer.GetCompareStatus(equipment, nextLevelEquipment));

        costText.enabled = true;
        image.enabled = true;
        enchantmentLevel.enabled = true;
        powerPercentage.enabled = true;

        // 1 2 3 4       

    }

    public void ResetSelect()
    {
        item = null;
        button.interactable = false;
        costText.enabled = false;
        itemRarityFrame.color = Color.white;
        itemRarity.color = Color.white;
        image.sprite = plusIcon;
        itemName.text = "";
        itemDesc.text = "";
        enchantmentLevel.enabled = false;
        powerPercentage.enabled = false;
        ClearMaterialSlot();
    }

    void ClearMaterialSlot()
    {
        for (int i = 0; i < materialSlots.Length; i++)
        {
            materialSlots[i].itemRarity.color = Color.white;
            materialSlots[i].itemRarityFrame.color = Color.white;
            materialSlots[i].itemIcon.sprite = plusIcon;
            materialSlots[i].enchantText.text = "";
            materialSlots[i].percentText.text = "";
        
        }
    }    


    public void Enchant()
    {   if (item == null)
            return;
        
        if (Condition(item))
        {
            Inventory.instance.UseMoney(CalculateCost(item));
            item.enchantment++;
            UseMaterial();
            SelectEnchant(item);
        }
    }

    bool Condition(Equipment item)
    {
        if (materials.Count == materialCount(item) && Inventory.instance.Money >= CalculateCost(item))
            return true;
        return false;
    }

    void UseMaterial()
    {
        foreach(Equipment x in materials)
        {
            Inventory.instance.UseAsMaterial(x);
        }
        materials.Clear();
    }

    public void AddMaterial(Equipment material)
    {
        materials.Add(material);
        button.interactable = Condition(item);
    }

    public void RemoveMaterial(Equipment material)
    {
        materials.Remove(material);
    }

    public int materialCount(Equipment item)
    {
        return item.enchantment + 1;
    }

    public int CalculateCost(Equipment equipment)
    {
        return CalculateCost(equipment.enchantment);
    }

    public int CalculateCost(int level)
    {
        return (level + 1) * (level + 2) / 2 * 100;
    }
}
