using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDetailPanel : MonoBehaviour
{
    public static ItemDetailPanel instance;
    public GameObject panel;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI statText;
    public TextMeshProUGUI powerText;
    public TextMeshProUGUI amountText;
    public Image iconImage;
    public TMP_InputField sellAmount;

    [SerializeField] TextMeshProUGUI sellPrice;

    public GameObject useButton;
    StackItem currentItem;

    private void Awake()
    {
        instance = this;
    }

    public void ShowItem(StackItem item)
    {
        if (item == null)
            return;

        currentItem = item;
        if (item.item is Equipment)
        {
            nameText.text = string.Format($"{currentItem.item.itemName} +{((Equipment)currentItem.item).enchantment}");
            powerText.text = ((Equipment)currentItem.item).powerPercent.ToString() + "%";
            powerText.enabled = true;
            amountText.enabled = false;
            useButton.SetActive(false);

        }
        else
        {


            nameText.text = currentItem.item.itemName;
            amountText.text = "จำนวน: " + currentItem.amount.ToString();
            amountText.enabled = true;
            powerText.enabled = false;

            useButton.SetActive(false);

            if (item.item is UsableItem)
            { 
                useButton.SetActive(true);
            }
        }
              
        iconImage.sprite = currentItem.item.icon;

        sellPrice.text = "ราคา: " + currentItem.item.sellPrice;

        sellAmount.text = "1";
        statText.text = item.item.GetDesc();

        panel.SetActive(true);
    }

    public void InputAmountCheck()
    {
        if(int.Parse(sellAmount.text) > currentItem.amount)
        {
            sellAmount.text = currentItem.amount.ToString();
        }
    }

    public void SellItem()
    {
        Inventory.instance.SellItem(currentItem.item, int.Parse(sellAmount.text));


        if (currentItem.amount <= 0)
            ClosePanel();
        else
            ShowItem(currentItem);
    }

    public void UseItem()
    {
        Inventory.instance.UseItem(currentItem.item);


        if (currentItem.amount <= 0)
            ClosePanel();
        else
            ShowItem(currentItem);
    }

    public void ClosePanel()
    {        
        panel.SetActive(false);
        UI_Inventory.instance.UpdateSlot();
    }
}
