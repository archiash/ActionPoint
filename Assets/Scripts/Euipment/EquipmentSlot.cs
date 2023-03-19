using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    public Image icon;
    public Image rarity;
    public EquipmentPart part;

    public TextMeshProUGUI enchantment;
    public TextMeshProUGUI powerpercent;

    Equipment onSlot;

    [SerializeField] Sprite defaultIcon;
    private void OnValidate()
    {
        gameObject.name = part.ToString() + "Slot";
        icon = transform.Find("Button/Icon").GetComponentInChildren<Image>();
        rarity = GetComponent<Image>();
    }

    private void Start()
    {
        icon = transform.Find("Button/Icon").GetComponentInChildren<Image>();
        rarity = GetComponent<Image>();
    }

    public void ChangeEquipment()
    {
        UIManager.Instance.equipmentSelectPanel.ShowPanel(part);
    }

    private void Update()
    {
       switch (part)
        {
            case EquipmentPart.Weapon:
                if (Character.instance.weapon != null)
                {
                    onSlot = Character.instance.weapon;
                    icon.sprite = Character.instance.weapon.icon;
                    icon.enabled = true;
                    icon.color = Color.white;
                    rarity.color = RarityColor.color(Character.instance.weapon.rarity);
                }
                else
                {
                    onSlot = null;
                    rarity.color = Color.white;
                    icon.sprite = defaultIcon;
                    icon.color = RarityColor.GetColor("#333333");
                }
                break;
            case EquipmentPart.Head:
                if (Character.instance.head != null)
                {
                    onSlot = Character.instance.head;
                    icon.sprite = Character.instance.head.icon;
                    icon.enabled = true;
                    icon.color = Color.white;
                    rarity.color = rarity.color = RarityColor.color(Character.instance.head.rarity);
                }
                else
                {
                    onSlot = null;
                    rarity.color = Color.white;
                    icon.sprite = defaultIcon;
                    icon.color = RarityColor.GetColor("#333333");
                }
                break;
            case EquipmentPart.Body:
                if (Character.instance.body != null)
                {
                    onSlot = Character.instance.body;
                    icon.sprite = Character.instance.body.icon;
                    icon.enabled = true;
                    icon.color = Color.white;
                    rarity.color = rarity.color = RarityColor.color(Character.instance.body.rarity);
                }
                else
                {
                    onSlot = null;
                    rarity.color = Color.white;
                    icon.sprite = defaultIcon;
                    icon.color = RarityColor.GetColor("#333333");
                }
                break;
            case EquipmentPart.Arms:
                if (Character.instance.arms != null)
                {
                    onSlot = Character.instance.arms;
                    icon.sprite = Character.instance.arms.icon;
                    icon.enabled = true;
                    icon.color = Color.white;
                    rarity.color = rarity.color = RarityColor.color(Character.instance.arms.rarity);
                }
                else
                {
                    onSlot = null;
                    rarity.color = Color.white;
                    icon.sprite = defaultIcon;
                    icon.color = RarityColor.GetColor("#333333");
                }
                break;
            case EquipmentPart.Legs:
                if (Character.instance.legs != null)
                {
                    onSlot = Character.instance.legs;
                    icon.sprite = Character.instance.legs.icon;
                    icon.enabled = true;
                    icon.color = Color.white;
                    rarity.color = rarity.color = RarityColor.color(Character.instance.legs.rarity);
                }
                else
                {
                    onSlot = null;
                    rarity.color = Color.white;
                    icon.sprite = defaultIcon;
                    icon.color = RarityColor.GetColor("#333333");
                }
                break;
            case EquipmentPart.Accessory:
                if (Character.instance.accessory != null)
                {
                    onSlot = Character.instance.accessory;
                    icon.sprite = Character.instance.accessory.icon;
                    icon.enabled = true;
                    icon.color = Color.white ;
                    rarity.color = rarity.color = RarityColor.color(Character.instance.accessory.rarity);
                }
                else
                {
                    onSlot = null;
                    rarity.color = Color.white;
                    icon.sprite = defaultIcon;
                    icon.color = RarityColor.GetColor("#333333");
                }
                break;
        }
        if (enchantment != null && powerpercent != null)
        {
            if (onSlot != null)
            {
                enchantment.text = "+" + onSlot.enchantment.ToString();
                enchantment.enabled = true;
                powerpercent.text = onSlot.powerPercent.ToString() + "%";
                powerpercent.enabled = true;
            }
            else
            {
                enchantment.enabled = false;
                powerpercent.enabled = false;
            }
        }
    }

}
