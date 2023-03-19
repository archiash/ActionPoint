using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_InventoryManager : MonoBehaviour
{
    public static UI_InventoryManager instance;

    Inventory inventory;

    [SerializeField] UI_InventorySlot slotPrefab;
    [SerializeField] UI_InventoryDetail detailPanel;
    [SerializeField] Transform content;
    List<UI_InventorySlot> slots = new List<UI_InventorySlot>();

    [SerializeField] TextMeshProUGUI space;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        //inventory = Inventory.instance;
        //UpdateSlot();
        Inventory.instance.onItemChange += UpdateSlot;
    }

    public void UpdateSlot()
    {
        ClearSlots();

        if (inventory == null)
            inventory = Inventory.instance;

        for (int i = 0; i < inventory.items.Count; i++)
        {
            UI_InventorySlot slot = Instantiate(slotPrefab, content);
            slot.Init(inventory.items[i]);
            
        }

        space.text = $"{inventory.items.Count}/{inventory.space}";
    }

    public void OpenInventory()
    {
        gameObject.SetActive(true);
        detailPanel.gameObject.SetActive(false);
        UpdateSlot();
    }

    public void ShowItemDetail(StackItem item)
    {
        detailPanel.gameObject.SetActive(true);
        detailPanel.ShowDetail(item);
    }

    public void ClearSlots()
    {
        foreach(Transform i in content)
        {
            Destroy(i.gameObject);
            slots.Clear();
        }
    }
}
