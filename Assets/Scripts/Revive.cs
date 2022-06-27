using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Revive : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI cost;
    private void Update()
    {
        if (Character.instance.status.currentHP <= 0)
        {
            ShowPanel();
        }else
        {
            panel.SetActive(false);
        }
    }

    public void ShowPanel()
    {     
        panel.SetActive(true);
        cost.text = "จ่าย $ " + ((int)(Inventory.instance.getMoney * 50 / 100f)).ToString() + ", " + ((int)(PointManager.instance.GetActionPoint * 30 / 100f)).ToString() + " Point";
    }

    public void OnReviveButton()
    {
        Inventory.instance.UseMoneyPercent(50);
        PointManager.instance.UseAction((int)(PointManager.instance.GetActionPoint * 30 / 100f));
        Character.instance.status.currentHP = Character.instance.status.HP.Value;
        panel.SetActive(false);
    }
}
