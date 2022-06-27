using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CraftFilterOption : MonoBehaviour
{
    public string filterName; 

    public void OnSetFilter()
    {
        bool isActive = CraftFilter.instance.setFilter(filterName);
        SetButtonFiler(isActive);
    }

    public void SetButtonFiler(bool active)
    {
        Color buttonColor = Color.grey;
        if (active)
        {
            ColorUtility.TryParseHtmlString("#34C9BA", out buttonColor);
        }
        GetComponent<Image>().color = buttonColor;
    }
}
