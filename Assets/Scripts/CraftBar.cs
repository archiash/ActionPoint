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
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI costText;

    public void Init(Recipe newRecipe)
    {
        ClearMaterialList();
        recipe = newRecipe;
        resultItem = newRecipe.resulItem;
        materials = newRecipe.material;
        itemImage.sprite = resultItem.icon;
        itemName.text = resultItem.itemName;
        costText.text = newRecipe.cost.ToString() + " Hex";

        foreach (StackItem material in materials)
        {
            GameObject newMaterial = Instantiate(pf_material, parent);
            newMaterial.GetComponent<CraftBar_Material>().Init(material);
        }
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
