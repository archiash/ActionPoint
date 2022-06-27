using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CraftEnchantManager : MonoBehaviour
{
    [SerializeField] TMP_Dropdown optionDropdown;
    //[SerializeField] TMP_Dropdown tierDropdown;
    public int level;

    [SerializeField] RecipeManager recipeManager;
    [SerializeField] GameObject enchantment;
    [SerializeField] GameObject crafting;
    [SerializeField] EnchantmentTranfer tranfer;

    [SerializeField] Button filterButton;
    [SerializeField] TextMeshProUGUI filterText;

    public void Start()
    {
        OptionChange();
    }

    public void OptionChange()
    {
        List<TMP_Dropdown.OptionData> option = new List<TMP_Dropdown.OptionData>();
        //tierDropdown.ClearOptions();
        filterButton.enabled = optionDropdown.value == 0;
        filterText.enabled = optionDropdown.value == 0;

        if (optionDropdown.value == 0)
        {
            //tierDropdown.enabled = true;
            enchantment.SetActive(false);
            crafting.SetActive(true);
            tranfer.gameObject.SetActive(false);
            for (int i = 0;i< level;i++)
            {
                option.Add(new TMP_Dropdown.OptionData() { text = "Tier " + (i + 1) });
            }
            TierChange();

        }
        else if(optionDropdown.value == 1)
        {
            //tierDropdown.enabled = false;
            enchantment.SetActive(true);
            crafting.SetActive(false);
            tranfer.gameObject.SetActive(false);
            
        }
        else if (optionDropdown.value == 2)
        {
            enchantment.SetActive(false);
            crafting.SetActive(false);
            tranfer.gameObject.SetActive(true);
            tranfer.ResetTranfer();
        }


        //TierChange();
    }

    public void TierChange()
    {     
        recipeManager.UpdateRecipe();            

    }

}
