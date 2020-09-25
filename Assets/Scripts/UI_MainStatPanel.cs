using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_MainStatPanel : MonoBehaviour
{
    Character character;

    [SerializeField] TextMeshProUGUI strText;
    [SerializeField] TextMeshProUGUI intText;
    [SerializeField] TextMeshProUGUI agiText;
    [SerializeField] TextMeshProUGUI dexText;
    [SerializeField] TextMeshProUGUI conText;

    private void Start()
    {
        character = Character.instance;
    }

    private void Update()
    {
        strText.text = "Str: " + character.status.STR.Value.ToString();
        intText.text = "Int: " + character.status.INT.Value.ToString();
        agiText.text = "Agi:" + character.status.AGI.Value.ToString();
        dexText.text = "Dex: " + character.status.DEX.Value.ToString();
        conText.text = "Con: " + character.status.CON.Value.ToString();
    }
}
