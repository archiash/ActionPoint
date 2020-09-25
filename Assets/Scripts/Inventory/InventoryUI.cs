using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryUI : MonoBehaviour
{
    Transform itemParent;
    public GameObject slotPrefab;

    Inventory inventory;

    List<InventorySlot> slots = new List<InventorySlot>();

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;

        itemParent = GameObject.FindGameObjectWithTag("InventoryContent").transform;
        slots = itemParent.GetComponentsInChildren<InventorySlot>().ToList();
        UpdateSlot();
        inventory.onItemChange += UpdateSlot;      
    }

    public void FixBug()
    {
        itemParent = GameObject.FindGameObjectWithTag("InventoryContent").transform;
        if (slots.Count > 0)
        {
            foreach (InventorySlot i in slots)
            {
                if(i != null)
                    GameObject.Destroy(i.gameObject);

            }
            itemParent.DetachChildren();
        }
        slots.Clear();
    }

    void UpdateUI()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Inventory"))
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
        Instantiate(slotPrefab, itemParent);
    }

    public void UpdateSlot()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Inventory"))
        {
            FixBug();

            for (int i = 0; i < inventory.space; i++)
            {
                if (slots.Count < inventory.space)
                {
                    AddSlot();
                    slots = itemParent.GetComponentsInChildren<InventorySlot>().ToList();
                    UpdateUI();
                }
            }
        }
    }

    public void BackButton()
    {
        SceneManager.LoadScene(0);
    }
}
