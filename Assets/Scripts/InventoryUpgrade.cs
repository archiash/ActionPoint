using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUpgrade : MonoBehaviour
{
    //[SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI currentLevel_Text;
    [SerializeField] TextMeshProUGUI nextLevel_Text;

    [SerializeField] TextMeshProUGUI levelArrow;
    [SerializeField] TextMeshProUGUI spaceArrow;

    [SerializeField] TextMeshProUGUI currentSpace_Text;
    [SerializeField] TextMeshProUGUI nextSpace_Text;

    [SerializeField] TextMeshProUGUI cost_Text;    
    
    //[SerializeField] TextMeshProUGUI descText;

    [SerializeField] Button button;
    [SerializeField] GameObject panel;

    [SerializeField] TextMeshProUGUI upgradeText;

    Inventory inventory;

    [SerializeField] int maxLevel;

    public void ShowPanel()
    {
        inventory ??= Inventory.instance;

        gameObject.SetActive(true);

        currentLevel_Text.text = inventory.level.ToString();
        currentSpace_Text.text = inventory.space.ToString();

        if (inventory.level < maxLevel)
        {
            nextLevel_Text.text = (inventory.level + 1).ToString();
            levelArrow.text = ">";
            spaceArrow.text = ">";
            nextSpace_Text.text = inventory.GetSpaceFormLevel(inventory.level + 1).ToString();

            cost_Text.text = CalculatePrize(inventory.level).ToString();
            upgradeText.text = "Upgrade";

            //levelText.text = "พื้นที่ - ระดับ " + inventory.level;
            //descText.text = $"พื้นที่: {inventory.GetSpaceFormLevel(inventory.level)} > {inventory.GetSpaceFormLevel(inventory.level + 1)}\n" +
            //$"ราคา: {CalculatePrize(inventory.level)}";
            button.interactable = CheckCondition();
        }else
        {
            nextLevel_Text.text = "";
            levelArrow.text = "";
            spaceArrow.text = "";
            nextSpace_Text.text = "";

            cost_Text.text = "-";
            upgradeText.text = "Max";
            //levelText.text = "พื้นที่ - ระดับสูงสุด";
            //descText.text = $"พื้นที่: {inventory.GetSpaceFormLevel(inventory.level)}";
            button.interactable = false;
        }
    }

    int CalculatePrize(int level)
    {
        return (int)(100 * Mathf.Round((500 + 300 * (level - 1) + 100 * (level - 1) * (level - 1) + 30 * (level - 1) * (level - 1) * (level - 1)) / 100f));         
    }

    bool CheckCondition()
    {
        return inventory.Money >= CalculatePrize(inventory.level); 
    }

    public void UpgradeInventory()
    {
        inventory.UseMoney(CalculatePrize(inventory.level));
        inventory.level++;
        ShowPanel();
    }
}
