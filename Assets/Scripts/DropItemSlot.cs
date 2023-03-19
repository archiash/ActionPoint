using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DropItemSlot : MonoBehaviour
{
    public TextMeshProUGUI amount;
    public Image itemImage;
    public Image itemRarity;
    public Image itemRarityFrame;

    public void OnCreate(StackItem drop)
    {
        amount.text = drop.amount.ToString();
        itemImage.sprite = drop.item.icon;
        itemRarity.color = RarityColor.color(drop.item.rarity);
        itemRarityFrame.color = RarityColor.color(drop.item.rarity);
    }

}
