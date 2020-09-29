using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUpgrade : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI descText;

    [SerializeField] Button button;
    [SerializeField] GameObject panel;

    Inventory inventory;

    [SerializeField] int maxLevel;

    private void Start()
    {
        inventory = Inventory.instance;
    }

    public void ShowPanel()
    {
        panel.SetActive(true);
        if (inventory.level < maxLevel)
        {
            levelText.text = "พื้นที่ - ระดับ " + inventory.level;
            descText.text = $"พื้นที่: {inventory.GetSpaceFormLevel(inventory.level)} > {inventory.GetSpaceFormLevel(inventory.level + 1)}\n" +
                $"ราคา: {CalculatePrize(inventory.level)}";
            button.interactable = CheckCondition();
        }else
        {
            levelText.text = "พื้นที่ - ระดับสูงสุด";
            descText.text = $"พื้นที่: {inventory.GetSpaceFormLevel(inventory.level)}";
            button.interactable = false;
        }
    }

    int CalculatePrize(int level)
    {
        return (int)(100 * Mathf.Round((500 + 300 * (level - 1) + 100 * (level - 1) * (level - 1) + 30 * (level - 1) * (level - 1) * (level - 1)) / 100f));         
    }

    bool CheckCondition()
    {
        return inventory.getMoney >= CalculatePrize(inventory.level); 
    }

    public void OnButton()
    {
        inventory.UseMoney(CalculatePrize(inventory.level));
        inventory.level++;
        ShowPanel();
    }
}
