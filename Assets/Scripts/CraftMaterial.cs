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

    public void OnCreate(StackItem item)
    {
        material = item;
        image.sprite = material.item.icon;
        amount.text = material.amount.ToString();
    }
}
