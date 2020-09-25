using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_DifficultDropdown : MonoBehaviour
{
    public List<Color> colors = new List<Color>();

    TMP_Dropdown dropdown;
    TextMeshProUGUI text;

    private void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ChangeColor()
    {
        text.color = colors[dropdown.value];    
    }
}
