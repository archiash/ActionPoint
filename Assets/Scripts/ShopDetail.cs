using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopDetail : MonoBehaviour
{
    public static ShopDetail SD;

    public Item showItem;

    public TextMeshProUGUI showName;
    public TextMeshProUGUI showDetail;
    public TextMeshProUGUI showPrize;

    public GameObject buyButton;

    public void OnShowChange(Item changeItem)
    {
        showItem = changeItem;
        showName.text = showItem.itemName;
        showDetail.text = showItem.itemDes;
    }

    public void OnBuyButton()
    {

    }

    private void Update()
    {
        if (showItem != null)
            buyButton.SetActive(true);
        else
            buyButton.SetActive(false);
    }

    private void Start()
    {
        if (SD == null) SD = this;
    }
}
