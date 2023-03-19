using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager instance;

    [SerializeField] CraftFilter filter;

    private void Start()
    {
        if (instance == null) instance = this;
        recipes = UIManager.Instance.forgesmith.recipeLists[0].recipes;
        UpdateRecipe();
    }

    public List<Recipe> recipes = new List<Recipe>();
    public GameObject pf_recipe;
    public Transform parent;


    public void ShowRecipeList()
    {
        ClearList();
        for (int i = 0; i < UIManager.Instance.forgesmith.recipeLists.Count;i++)
        {
            if (filter.GetFilter($"Tier{i+1}"))
            {
                recipes = UIManager.Instance.forgesmith.recipeLists[i].recipes;
                recipes = recipes.OrderBy(i => i.cost).ToList();
                foreach (Recipe recipe in recipes)
                {
                    if (CheckMatchFilterStat((Equipment)recipe.resulItem))
                    {
                        GameObject newRecipe = Instantiate(pf_recipe, parent);
                        newRecipe.GetComponent<CraftBar>().Init(recipe);
                    }
                }
            }
        }
    }

    private bool CheckMatchFilterStat(Equipment equipment)
    {
        bool isMatch = filter.GetFilter(equipment.part.ToString());
        if (!isMatch) return isMatch;
        bool modMatch = false;
        for (int i = 0; i < equipment.modifiers.Count; ++i)
        {
            if(equipment.modifiers[i].modifierType == StatType.Main)
            {
                modMatch = filter.GetFilter(equipment.modifiers[i].mainType.ToString().Substring(0,1) 
                    + equipment.modifiers[i].mainType.ToString().Substring(1).ToLower());
                if (modMatch) return modMatch;
            }
            else
            {
                modMatch = filter.GetFilter(equipment.modifiers[i].statType);
                if (modMatch) return modMatch;
            }
        }

        return false;
    }

    public void UpdateRecipe()
    {
        ShowRecipeList();
    }

    public void ClearList()
    {

        Transform[] transforms = parent.GetComponentsInChildren<Transform>();

        foreach (Transform x in transforms)
        {
            if (x.GetComponent<CraftBar>())
            {
                Destroy(x.gameObject);
            }
        }
    }

    public void BackButton()
    {
        SceneManager.LoadScene("Main");
    }
}
