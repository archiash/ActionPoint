using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enchantment : MonoBehaviour
{
    public Image image;
    public Equipment item;

    public TextMeshProUGUI enchantmentLevel;
    public TextMeshProUGUI powerPercentage;

    public static Enchantment instance;

    [SerializeField]EnchantMaterialSlot slotPrefab;
    [SerializeField] Transform parent;
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

        item = equipment;
        image.sprite = equipment.icon;
        enchantmentLevel.text = "+" + item.enchantment.ToString();
        powerPercentage.text = item.powerPercent.ToString() + "%";
        costText.text = "ราคา:" + CalculateCost(equipment);

        button.interactable = Condition(item);

        costText.enabled = true;
        image.enabled = true;
        enchantmentLevel.enabled = true;
        powerPercentage.enabled = true;

        CreateMaterialSlot();
    }

    public void ResetSelect()
    {
        item = null;
        button.interactable = false;
        costText.enabled = false;
        image.enabled = false;
        enchantmentLevel.enabled = false;
        powerPercentage.enabled = false;
        ClearMaterialSlot();
    }

    void CreateMaterialSlot()
    {
        for(int i = 0; i < materialCount(item);i++)
        {
            Instantiate(slotPrefab, parent);
        }
        
    }

    void ClearMaterialSlot()
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
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
        if (materials.Count == materialCount(item) && Inventory.instance.getMoney >= CalculateCost(item))
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

    public List<Equipment> materials = new List<Equipment>();

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
        return Mathf.FloorToInt(1 + (item.enchantment / 3));
    }

    public int CalculateCost(Equipment equipment)
    {
        int cost = 0;
        for (int i = 0; i <= equipment.enchantment; i++)
        {
            cost += 100 * Mathf.FloorToInt(1 + (i / 3));
        }
        return cost;
    }

    public int CalculateCost(int level)
    {
        int cost = 0;
        for (int i = 0; i <= level; i++)
        {
            cost += 100 * Mathf.FloorToInt(1 + (i / 3));
        }
        return cost;
    }
}
