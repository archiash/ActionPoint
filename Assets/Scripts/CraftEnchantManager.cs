using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CraftEnchantManager : MonoBehaviour
{
    [SerializeField] TMP_Dropdown optionDropdown;
    [SerializeField] TMP_Dropdown tierDropdown;
    public int level;

    [SerializeField] RecipeManager recipeManager;
    [SerializeField] GameObject enchantment;
    [SerializeField] GameObject crafting;

    public void OptionChange()
    {
        List<TMP_Dropdown.OptionData> option = new List<TMP_Dropdown.OptionData>();
        tierDropdown.ClearOptions();
        if (optionDropdown.value == 0)
        {
            tierDropdown.enabled = true;
            enchantment.SetActive(false);
            crafting.SetActive(true);
            for(int i = 0;i< level;i++)
            {
                option.Add(new TMP_Dropdown.OptionData() { text = "Tier " + (i + 1) });
            }

        }else if(optionDropdown.value == 1)
        {
            tierDropdown.enabled = false;
            enchantment.SetActive(true);
            crafting.SetActive(false);
        }
        tierDropdown.AddOptions(option);
        tierDropdown.value = 1;
        tierDropdown.value = 0;
        TierChange();
    }

    public void TierChange()
    {
        if(optionDropdown.value == 0)
        {            
            recipeManager.UpdateRecipe(tierDropdown.value);            
        }
    }

}
