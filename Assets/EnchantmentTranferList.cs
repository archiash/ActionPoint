using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnchantmentTranferList : MonoBehaviour
{

    [SerializeField] EnchantmentTranfer enchantmentTranfer;
    [SerializeField] EnchantTranferBar tranferBarPrefab;

    [SerializeField] Transform content;

    public void CreateReciverList()
    {
        gameObject.SetActive(true);
        ClearList();
        Inventory inventory = Inventory.instance;
        for(int i = 0;i< inventory.items.Count;i++)
        {
            if(inventory.items[i].item is Equipment)
            {
                EnchantTranferBar bar = Instantiate(tranferBarPrefab, content);
                bar.Init((Equipment)inventory.items[i].item, true);
            }
        }
    }

    public void CreateGiverList()
    {
        gameObject.SetActive(true);
        ClearList();
        Inventory inventory = Inventory.instance;
        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i].item is Equipment && inventory.items[i].item.ID == enchantmentTranfer.reciver.ID
                && inventory.items[i].item != enchantmentTranfer.reciver)
            {
                EnchantTranferBar bar = Instantiate(tranferBarPrefab, content);
                bar.Init((Equipment)inventory.items[i].item, false);
            }
        }
    }

    private void ClearList()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }
}
