using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusUpgradeUI : MonoBehaviour
{
    StatusUpgrade statusUpgrade;

    [SerializeField] TextMeshProUGUI statPoint;

    [SerializeField] TextMeshProUGUI strLvl;

    [SerializeField] TextMeshProUGUI intLvl;
    
    [SerializeField] TextMeshProUGUI dexLvl;
    
    [SerializeField] TextMeshProUGUI agiLvl;

    [SerializeField] TextMeshProUGUI conLvl;

    [SerializeField] TextMeshProUGUI costText;

    Character character;

    [SerializeField] Button[] buttons;
    [SerializeField] Button resetButton;

    [SerializeField] TextMeshProUGUI[] perOneMainDesc;

    public void Start()
    {
        character = Character.instance;
        statusUpgrade = StatusUpgrade.instance;
    }

    public void Upgrade(int mainType)
    {
        switch(mainType)
        {
            case 0:
                statusUpgrade.STR++;
                break;
            case 3:
                statusUpgrade.INT++;
                break;
            case 1:
                statusUpgrade.DEX++;
                break;
            case 2:
                statusUpgrade.AGI++;
                break;
            case 4:
                statusUpgrade.CON++;
                break;
        }
        character.statusPoint--;
        Refresh();
    }

    public void ResetPoint()
    {
        Inventory.instance.UseMoney(ResetCost(Character.instance.level));
        statusUpgrade.ResetPoint();
        Refresh();
    }

    int ResetCost(int level)
    {
        return Mathf.Max((int)Mathf.Round(Mathf.Pow(Mathf.CeilToInt(Mathf.Log(level - 1) * 10),2) / 10) * 10, 0); 
    }

    public void Refresh()
    {
        costText.text = $"{ResetCost(Character.instance.level)}$";
        resetButton.interactable = Inventory.instance.Money >= ResetCost(Character.instance.level) && Character.instance.statusPoint < Character.instance.level - 1;

        buttons[0].interactable = Character.instance.statusPoint > 0 && StatusUpgrade.instance.AGI < 15;
        buttons[1].interactable = Character.instance.statusPoint > 0 && StatusUpgrade.instance.DEX < 15;
        buttons[2].interactable = Character.instance.statusPoint > 0 && StatusUpgrade.instance.STR < 15 ;
        buttons[3].interactable = Character.instance.statusPoint > 0 && StatusUpgrade.instance.CON < 15;
        buttons[4].interactable = Character.instance.statusPoint > 0 && StatusUpgrade.instance.INT < 15;

        statPoint.text = $"แต้มอัพเกรดที่เหลือ: {Character.instance.statusPoint}";
        strLvl.text = $"+{StatusUpgrade.instance.STR}";
        dexLvl.text = $"+{StatusUpgrade.instance.DEX}";
        agiLvl.text = $"+{StatusUpgrade.instance.AGI}";
        intLvl.text = $"+{StatusUpgrade.instance.INT}";
        conLvl.text = $"+{StatusUpgrade.instance.CON}";
    }

    public void ShowPerOneMainPoint()
    {
        ClassInfluence classInfluence = Character.instance.ClassInfluence;
        for (int i = 0; i < classInfluence.subModifies.Count; i++) {
            StatusUpgrade.SubModify[] subModify = classInfluence.subModifies[i];
            perOneMainDesc[i].text = "";
            for (int j = 0; j < subModify.Length; j++)
            {
                perOneMainDesc[i].text += $"+ {subModify[j].value} {subModify[j].subType}";
                if(j < subModify.Length - 1)
                {
                    perOneMainDesc[i].text += "\n";
                }
            }
        }
    }
}
