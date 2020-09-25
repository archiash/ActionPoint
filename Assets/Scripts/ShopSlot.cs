using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public Item item;

    public Image icon;

    public void OnCreate()
    {
        icon.sprite = item.icon;
    }

    public void OnButton()
    {
        ShopDetail.SD.OnShowChange(item);
    }
}
