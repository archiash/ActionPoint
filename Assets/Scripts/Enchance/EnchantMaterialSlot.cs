using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnchantMaterialSlot : MonoBehaviour
{
    public Equipment slotItem;
    public Image icon;

    public void SetSlotItem(Equipment newItem)
    {
        slotItem = newItem;
        icon.sprite = slotItem.icon;
        icon.enabled = true;
    }

    public void OnSelect()
    {
        EnchantMaterialSelect.instance.GetFormSlot(this);
    }

}
