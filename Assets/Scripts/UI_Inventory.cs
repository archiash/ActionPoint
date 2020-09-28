using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] Camera InventoryCamera;

    public static UI_Inventory instance;

    [SerializeField] UI_InventorySlot slotPrefab;
    Transform parent;

    Inventory inventory;
    List<UI_InventorySlot> slots = new List<UI_InventorySlot>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        inventory = Inventory.instance;
        parent = GameObject.FindGameObjectWithTag("InventoryContent").transform;
        inventory.onItemChange += UpdateSlot;
    }

    public void UpdateSlot()
    {
        if(inventory == null)
            inventory = Inventory.instance;

        if (InventoryCamera.gameObject.activeSelf)
        {
                ClearSlot();

                for (int i = 0; i < inventory.space; i++)
                {
                    if (slots.Count < inventory.space)
                    {
                        AddSlot();
                        slots = parent.GetComponentsInChildren<UI_InventorySlot>().ToList();
                        UpdateUI();
                    }
                }
        }
    }

    void UpdateUI()
    {
        if (InventoryCamera.gameObject.activeSelf)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (i < inventory.items.Count)
                {
                    slots[i].AddItem(inventory.items[i]);
                }
                else
                {
                    slots[i].ClearSlot();
                }
            }
        }
    }

    public void AddSlot()
    {
        Instantiate(slotPrefab, parent);
    }

    public void ClearSlot()
    {
        parent = GameObject.FindGameObjectWithTag("InventoryContent").transform;
        if (slots.Count > 0)
        {
            foreach (UI_InventorySlot i in slots)
            {
                if (i != null)
                    GameObject.Destroy(i.gameObject);

            }
            parent.DetachChildren();
        }
        slots.Clear();
    }
}
