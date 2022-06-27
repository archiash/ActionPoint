using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CraftBar : MonoBehaviour
{
    public Recipe recipe;
    public Item resultItem;
    public List<StackItem> materials = new List<StackItem>();

    public GameObject pf_material;
    public Transform parent;

    public Image itemImage;
    public Image rarity;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI costText;
    [SerializeField] TextMeshProUGUI statusText;

    public void Init(Recipe newRecipe)
    {
        ClearMaterialList();
        recipe = newRecipe;
        resultItem = newRecipe.resulItem;
        rarity.color = RarityColor.color(resultItem.rarity);
        materials = newRecipe.material;
        itemImage.sprite = resultItem.icon;
        itemName.text = resultItem.itemName;
        if (recipe.resulItem is Equipment)
        {
            itemName.text += " - " + ((Equipment)resultItem).part.ToString();
            statusText.text = ((Equipment)resultItem).GetDesc(100,false,false);
        }

        costText.text = newRecipe.cost.ToString() + " $";

        foreach (StackItem material in materials)
        {
            GameObject newMaterial = Instantiate(pf_material, parent);
            newMaterial.GetComponent<CraftBar_Material>().Init(material);
        }

        GameObject money = Instantiate(pf_material, parent);
        money.GetComponent<CraftBar_Material>().Init(newRecipe.cost);
    }

    public void ClearMaterialList()
    {
        Transform[] transforms = parent.GetComponentsInChildren<Transform>();

        foreach (Transform x in transforms)
        {
            if (x.GetComponent<CraftMaterial>())
            {
                Destroy(x.gameObject);
            }
        }
    }
    
    public void ShowCraftDetail()
    {
        CraftDetail.instance.ShowDetail(recipe);
    }
}
