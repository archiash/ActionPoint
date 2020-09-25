using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MaterialBar : MonoBehaviour
{
    public StackItem material;
    public Image image;

    public TextMeshProUGUI materialName;
    public TextMeshProUGUI amount;

    public void OnCreate(StackItem newItem)
    {
        material = newItem;
        image.sprite = material.item.icon;
        materialName.text = material.item.itemName;
        if(Inventory.instance.items.Count < 1)
            amount.text = "0/" + material.amount.ToString();
        foreach (StackItem item in Inventory.instance.items)
        {
            if (item.item == material.item || item.item.ID == material.item.ID)
            {
                amount.text = item.amount.ToString() + "/" + material.amount.ToString();
                break;

            }
            amount.text = "0/" + material.amount.ToString();
        }                    
    }

}
