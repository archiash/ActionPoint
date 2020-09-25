using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftDetail_Material : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI materialName;
    [SerializeField] TextMeshProUGUI amount;

    public void Init(StackItem item)
    {
        image.sprite = item.item.icon;
        materialName.text = item.item.itemName;
        amount.text = string.Format($"item.amount");
        if (Inventory.instance.items.Count < 1)
            amount.text = "0/" + item.amount.ToString();
        foreach (StackItem i in Inventory.instance.items)
        {
            if (i.item == item.item || i.item.ID == item.item.ID)
            {
                amount.text = i.amount.ToString() + "/" + item.amount.ToString();
                break;

            }
            amount.text = "0/" + item.amount.ToString();
        }
    }
}
