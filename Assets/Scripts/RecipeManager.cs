using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager instance;
    private void Start()
    {
        if (instance == null) instance = this;
        recipes = Forgesmith.instance.recipeLists[0].recipes;
        UpdateRecipe(0);
    }

    public List<Recipe> recipes = new List<Recipe>();
    public GameObject pf_recipe;
    public Transform parent;

    public void UpdateRecipe(int tier)
    {
        recipes = Forgesmith.instance.recipeLists[tier].recipes;
        recipes = recipes.OrderBy(i => i.cost).ToList();
        ClearList();
        foreach(Recipe recipe in recipes)
        {
            GameObject newRecipe = Instantiate(pf_recipe, parent);
            newRecipe.GetComponent<CraftBar>().Init(recipe);
        }
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
