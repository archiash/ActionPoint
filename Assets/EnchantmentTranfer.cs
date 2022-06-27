using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI ;
public class EnchantmentTranfer : MonoBehaviour
{
    public static EnchantmentTranfer instance;

    public Equipment reciver;
    public Equipment giver;

    [SerializeField] Image reciverIcon;
    [SerializeField] TextMeshProUGUI reciverEnchant;
    [SerializeField] TextMeshProUGUI reciverPercent;
    [SerializeField] Image reciverRarity;


    [SerializeField] Image giverIcon;
    [SerializeField] TextMeshProUGUI giverEnchant;
    [SerializeField] TextMeshProUGUI giverPercent;
    [SerializeField] Image giverRarity;

    [SerializeField] EnchantmentTranferList tranferList;

    [SerializeField] Button tranferButton;


    [SerializeField] TextMeshProUGUI costText;
    
    private void Awake()
    {
        instance = this;
    }

    public void SetReciver(Equipment equipment)
    {
        if (equipment == null)
        {
            reciver = null;
            reciverIcon.enabled = false;
            reciverEnchant.enabled = false;
            reciverPercent.enabled = false;
            reciverRarity.color = Color.white;
            return;
        }
        SetGiver(null);
        tranferList.gameObject.SetActive(false);
        reciver = equipment;
        reciverIcon.sprite = equipment.icon;
        reciverEnchant.text = "+ " + equipment.enchantment;
        reciverPercent.text = equipment.powerPercent + "%";
        reciverRarity.color = RarityColor.color(equipment.rarity);
        reciverIcon.enabled = true;
        reciverEnchant.enabled = true;
        reciverPercent.enabled = true;
    }

    public void SetGiver(Equipment equipment)
    {
        
        if (equipment == null)
        {
            giver = null;
            giverIcon.enabled = false;
            giverEnchant.enabled = false;
            giverPercent.enabled = false;
            giverRarity.color = Color.white;
            costText.enabled = false;
            tranferButton.interactable = false;
            return;
        }

        tranferList.gameObject.SetActive(false);
        giver = equipment;
        giverIcon.sprite = equipment.icon;
        giverEnchant.text = "+ " + equipment.enchantment;
        giverPercent.text = equipment.powerPercent + "%";
        giverRarity.color = RarityColor.color(equipment.rarity);
        giverIcon.enabled = true;
        giverEnchant.enabled = true;
        giverPercent.enabled = true;

        tranferButton.interactable = Inventory.instance.getMoney >= giver.enchantment * 100;
        costText.enabled = true;
        costText.text = "ค่าใช้จ่าย: " + giver.enchantment * 100;
    }

    public void Tranfer()
    {
        Inventory.instance.UseAsMaterial(giver);
        reciver.enchantment = giver.enchantment;
        ResetTranfer();
    }

    public void ResetTranfer()
    {
        tranferButton.interactable = false;
        SetGiver(null);
        SetReciver(null);
    }

}
