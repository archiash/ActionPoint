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

        image.enabled = true;
        enchantmentLevel.enabled = true;
        powerPercentage.enabled = true;

        CreateMaterialSlot();
    }

    void ResetSelect()
    {
        item = null;
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
            item.enchantment++;
            UseMaterial();          
            ResetSelect();
        }
    }

    bool Condition(Equipment item)
    {
        if (materials.Count == materialCount(item))
            return true;
        return false;
    }

    void UseMaterial()
    {
        foreach(Equipment x in materials)
        {
            Inventory.instance.UseAsMaterial(x,1);
        }
        materials.Clear();
    }

    public List<Equipment> materials = new List<Equipment>();

    public void AddMaterial(Equipment material)
    {
        materials.Add(material);
    }

    public void RemoveMaterial(Equipment material)
    {
        materials.Remove(material);
    }

    public int materialCount(Equipment item)
    {
        return Mathf.FloorToInt(1 + (item.enchantment / 3));
    }
}
