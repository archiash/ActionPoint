using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffSlot : MonoBehaviour
{
    Modifier modifier;

    public TextMeshProUGUI turnAmount;
    public Image icon;


    Item source;
    string Descript;

    public void Init(Modifier _modifier,string statToBuff)
    {
        modifier = _modifier;
        turnAmount.text = modifier.time.ToString();
        Descript = $"{statToBuff}: {modifier.value}";
        if (modifier.type == ModifierType.Percentage)
            Descript += "%";

        Descript += $"\nLeft: {modifier.time} Hunt";

        source = _modifier.source as Item;

        if(source != null)
            icon.sprite = source.icon;

    }

    public void GetDetail()
    {
        GameObject panel = GameObject.FindGameObjectWithTag("BuffDetail");
        BuffDetail detail = panel.GetComponent<BuffDetail>();
        detail.ShowDetail(Descript);
    }
}
