using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InventoryManager : MonoBehaviour
{
    public static UI_InventoryManager instance;

    Inventory inventory;

    [SerializeField] Transform content;
    List<UI_InventorySlot> slots = new List<UI_InventorySlot>();

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        inventory = Inventory.instance;
    }

    public void UpdateSlot()
    {
        if (inventory == null)
            inventory = Inventory.instance;

        for (int i = 0; i < inventory.space; i++)
        {
            if (slots.Count < inventory.space)
            {
                
            }
        }
    }

}
