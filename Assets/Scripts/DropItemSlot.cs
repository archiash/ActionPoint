using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DropItemSlot : MonoBehaviour
{
    public TextMeshProUGUI amount;
    public Image image;
    public Image rarity;

    public void OnCreate(StackItem drop)
    {
        amount.text = drop.amount.ToString();
        image.sprite = drop.item.icon;
        rarity.color = RarityColor.color(drop.item.rarity);
    }

}
