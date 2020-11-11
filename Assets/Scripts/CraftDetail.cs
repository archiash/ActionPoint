using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CraftDetail : MonoBehaviour
{
    public static CraftDetail instance;
    private void Start()
    {
        instance = this;
    }
    [SerializeField]
    private bool freeCraft;

    Recipe recipe;

    public TextMeshProUGUI itemName;
    public Image image;
    public TextMeshProUGUI itemDesc;
    public TextMeshProUGUI cost;

    public GameObject pf_materialbar;
    public Transform parent;
    public GameObject detailPanel;

    public Button craftButton;

    [Header("Result")]
    [SerializeField] GameObject resultPanel;
    [SerializeField]Image resultImage;// ItemIcon
    [SerializeField] TextMeshProUGUI PP; //PowerPercentage
    [SerializeField] TextMeshProUGUI resultName; //Name of ResultItem
    [SerializeField] TextMeshProUGUI resultStat; // Stat of ResultItem

    void ShowResult(Equipment resultItem)
    {
        resultImage.sprite = resultItem.icon;
        PP.text = "ระดับพลัง " + resultItem.powerPercent + "%";
        resultName.text = resultItem.itemName;
        resultStat.text = resultItem.GetDesc(false);
        resultPanel.SetActive(true);
    }


    public void ShowDetail(Recipe newRecipe)
    {
        recipe = newRecipe;
        itemName.text = recipe.resulItem.itemName;
        itemDesc.text = recipe.resulItem.GetDesc();
        if(recipe.resulItem is Equipment)
        {
            itemDesc.text = ((Equipment)recipe.resulItem).GetDesc(100);
        }
            
        image.sprite = recipe.resulItem.icon;
        cost.text = "Cost: " + recipe.cost.ToString() + " $";
        ClearDetail();
        foreach (StackItem stackItem in recipe.material)
        {
            GameObject materialbar = Instantiate(pf_materialbar, parent);
            materialbar.GetComponent<CraftDetail_Material>().Init(stackItem);
        }
        if (CheckCondition() && Inventory.instance.getMoney >= recipe.cost && Inventory.instance.HaveEmptySpace() || freeCraft)
        {
            craftButton.interactable = true;
            craftButton.GetComponentInChildren<TextMeshProUGUI>().text = "สร้าง";
        }
        else
        {
            craftButton.interactable = false;
            if (!CheckCondition())
                craftButton.GetComponentInChildren<TextMeshProUGUI>().text = "ไม่มีวัตถุดิบ";
            else if(Inventory.instance.getMoney < recipe.cost)
                craftButton.GetComponentInChildren<TextMeshProUGUI>().text = "เงินไม่เพียงพอ";
            else 
                craftButton.GetComponentInChildren<TextMeshProUGUI>().text = "กระเป๋าเต็ม";

        }
        detailPanel.SetActive(true);
    }

    public void ClearDetail()
    {
        Transform[] transforms = parent.GetComponentsInChildren<Transform>();

        foreach (Transform x in transforms)
        {
            if (x.GetComponent<CraftDetail_Material>())
            {
                Destroy(x.gameObject);
            }
        }
    }
    public bool CheckCondition()
    {
        if (Inventory.instance.items.Count < 1)
        {
            return false;
        }

        bool result = true;
        foreach (StackItem material in recipe.material)
        {
            Debug.Log("Check " + material.item.itemName);
            foreach (StackItem item in Inventory.instance.items)
            {
                Debug.Log("Check with " + item.item.itemName);

                if (item.item.ID == material.item.ID && item.amount >= material.amount)
                {
                    result = true;
                    Debug.Log("Have");
                    break;

                }
                result = false;
            }
            if (result == false)
                return false;
        }
        return true;
    }

    public void Crafting()
    {
        if (!freeCraft)
        {
            foreach (StackItem material in recipe.material)
            {
                foreach (StackItem item in Inventory.instance.items)
                {
                    if (item.item.ID == material.item.ID && item.amount >= material.amount)
                    {
                        Inventory.instance.UseAsMaterial(item.item, material.amount);
                        break;
                    }
                }
            }
            Inventory.instance.UseMoney(recipe.cost);
        }
        Item result = recipe.resulItem.GetCopyItem();
        ShowResult((Equipment)result);
        Inventory.instance.GetItem(result);
        detailPanel.SetActive(false);
    }
}
