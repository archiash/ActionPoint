using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusPanel : MonoBehaviour
{
    public TextMeshProUGUI mainStat;
    public TextMeshProUGUI pAtk;
    public TextMeshProUGUI pDef;
    public TextMeshProUGUI mAtk;
    public TextMeshProUGUI mDef;
    public TextMeshProUGUI spd;
    public TextMeshProUGUI eva;
    public TextMeshProUGUI hit;
    public TextMeshProUGUI cRate;
    public TextMeshProUGUI cDmg; 

    Character character;
    private void Start()
    {
        character = Character.instance;      
    }

    private void Update()
    {
        mainStat.text = string.Format($"STR: {character.status.STR.Value}  " +
            $"DEX: {character.status.DEX.Value}  " +
            $"AGI: {character.status.AGI.Value}  " +
            $"INT: {character.status.INT.Value}  " +
            $"CON: {character.status.CON.Value}");
        pAtk.text = "Attack:" + character.status.PAtk.Value.ToString();
        pDef.text = "Defense:" + character.status.PDef.Value.ToString();
        mAtk.text = "Magic:" + character.status.MAtk.Value.ToString();
        mDef.text = "MagicResist:" + character.status.MDef.Value.ToString();
        spd.text = "Speed:" + character.status.Spd.Value.ToString();
        hit.text = "Hit:" + character.status.Hit.Value.ToString();
        eva.text = "Eva:" + character.status.Eva.Value.ToString();
        cRate.text = "CriticalRate:" + character.status.Crate.Value.ToString();
        cDmg.text = "CriticalDamage:" + character.status.Cdmg.Value.ToString();
    }
}
