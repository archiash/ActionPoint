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
    [SerializeField] TextMeshProUGUI strDesc;

    [SerializeField] TextMeshProUGUI intLvl;
    [SerializeField] TextMeshProUGUI intDesc;
    
    [SerializeField] TextMeshProUGUI dexLvl;
    [SerializeField] TextMeshProUGUI dexDesc;
    
    [SerializeField] TextMeshProUGUI agiLvl;
    [SerializeField] TextMeshProUGUI agiDesc;

    [SerializeField] TextMeshProUGUI conLvl;
    [SerializeField] TextMeshProUGUI conDesc;

    Character character;

    [SerializeField] Button[] buttons;

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
    }

    public void Refresh()
    {
        statPoint.text = "Point: " +  character.statusPoint.ToString();

        if(character.statusPoint > 0)
        {
            foreach (Button i in buttons)
                i.interactable = true;
        }else
        {
            foreach (Button i in buttons)
                i.interactable = false;
        }    

        strLvl.text = "STR +" + statusUpgrade.STR;
        intLvl.text = "INT +" + statusUpgrade.INT;
        dexLvl.text = "DEX +" + statusUpgrade.DEX;
        agiLvl.text = "AGI +" + statusUpgrade.AGI;
        conLvl.text = "CON +" + statusUpgrade.CON;

        strDesc.text = "";
        foreach(StatusUpgrade.SubModify mod in statusUpgrade.strSub)
        {
            strDesc.text += Stat.SubToNormalName(mod.subType) + ": " + mod.value * (1 + statusUpgrade.STR / statusUpgrade.scale) + " > " + mod.value * (1 + (statusUpgrade.STR + 1) / statusUpgrade.scale) + " / STR\n";
        }

        intDesc.text = "";
        foreach (StatusUpgrade.SubModify mod in statusUpgrade.intSub)
        {
            intDesc.text += Stat.SubToNormalName(mod.subType) + ": " + mod.value * (1 + statusUpgrade.INT / statusUpgrade.scale) + " > " + mod.value * (1 + (statusUpgrade.INT + 1) / statusUpgrade.scale) + " / INT\n";
        }

        dexDesc.text = "";
        foreach (StatusUpgrade.SubModify mod in statusUpgrade.dexSub)
        {
            dexDesc.text += Stat.SubToNormalName(mod.subType) + ": " + mod.value * (1 + statusUpgrade.DEX / statusUpgrade.scale) + " > " + mod.value * (1 + (statusUpgrade.DEX + 1) / statusUpgrade.scale) + " / DEX\n";
        }

        agiDesc.text = "";
        foreach (StatusUpgrade.SubModify mod in statusUpgrade.agiSub)
        {
            agiDesc.text += Stat.SubToNormalName(mod.subType) + ": " + mod.value * (1 + statusUpgrade.AGI / statusUpgrade.scale) + " > " + mod.value * (1 + (statusUpgrade.AGI + 1) / statusUpgrade.scale) + " / AGI\n";
        }

        conDesc.text = "";
        foreach (StatusUpgrade.SubModify mod in statusUpgrade.conSub)
        {
            conDesc.text += Stat.SubToNormalName(mod.subType) + ": " + mod.value * (1 + statusUpgrade.CON / statusUpgrade.scale) + " > " + mod.value * (1 + (statusUpgrade.CON + 1) / statusUpgrade.scale) + "/ CON\n";
        }

    }
}
