using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftMaterial : MonoBehaviour
{
    public StackItem material;
    public Image image;
    public TextMeshProUGUI amount;

    public void Init(StackItem item)
    {
        material = item;
        image.sprite = item.item.icon;
        //if (Inventory.instance.items.Count < 1)
        amount.text = "0/" + item.amount.ToString();
        foreach (StackItem i in Inventory.instance.items)
        {
            if (i.item == item.item || i.item.ID == item.item.ID)
            {
                amount.text = i.amount.ToString() + "/" + item.amount.ToString();
                break;
            }
        }
    }

    public void Refrest()
    {
        if(material != null)
            Init(material);
    }
}
