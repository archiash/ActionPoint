using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EnchantMaterialSelect : MonoBehaviour
{
    public static EnchantMaterialSelect instance;


    [SerializeField] EnchantMaterialBar MaterialBarPrefab;
    [SerializeField] EnchantBaseBar BaseBarPrefab;

    [SerializeField] Transform parent;
    Enchantment enchantment;
    Inventory inventory;

    [SerializeField]GameObject panel;
    public EnchantMaterialSlot slot;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        enchantment = Enchantment.instance;
        inventory = Inventory.instance;
    }

    public void GetFormSlot(EnchantMaterialSlot newSlot)
    {
        slot = newSlot;
        ShowMaterialList();
    }

    List<Equipment> equipments = new List<Equipment>();

    public void ShowMaterialList()
    {
        inventory = Inventory.instance;
        ClearList();
        
        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i].item.ID == enchantment.item.ID && !enchantment.materials.Contains((Equipment)inventory.items[i].item) && inventory.items[i].item != enchantment.item)
            {
                EnchantMaterialBar newBar = Instantiate(MaterialBarPrefab, parent);
                newBar.CreateBar((Equipment)inventory.items[i].item,enchantment);
            }
        }

        panel.SetActive(true);
    }

    public void ShowEquipmentList()
    {
        inventory = Inventory.instance;
        ClearList();

        equipments.Clear();
        
        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i].item is Equipment)
            {
                Equipment item = (Equipment)inventory.items[i].item;
                if (item.enchantment < 5)
                {
                    equipments.Add((Equipment)inventory.items[i].item);

                }

            }
        }

        equipments = equipments.OrderByDescending(x => x.enchantment).ThenByDescending(x => x.powerPercent).ToList();

        for(int i = 0; i < equipments.Count; i++)
        {
            EnchantBaseBar newBar = Instantiate(BaseBarPrefab, parent);
            newBar.CreateBar(equipments[i], enchantment);
        }

        panel.SetActive(true);
    }

    void ClearList()
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
    }


}
