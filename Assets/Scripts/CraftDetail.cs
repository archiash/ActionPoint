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

    public GameObject craftButton;

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
        cost.text = "Cost: " + recipe.cost.ToString() + " Hex";
        ClearDetail();
        foreach (StackItem stackItem in recipe.material)
        {
            GameObject materialbar = Instantiate(pf_materialbar, parent);
            materialbar.GetComponent<CraftDetail_Material>().Init(stackItem);
        }
        if (CheckCondition() && Inventory.instance.getMoney >= recipe.cost || freeCraft)
            craftButton.SetActive(true);
        else
            craftButton.SetActive(false);

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
        Inventory.instance.GetItem(recipe.resulItem.GetCopyItem());
        detailPanel.SetActive(false);
    }
}
