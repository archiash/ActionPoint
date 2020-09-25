using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HpPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI rengenText;

    [SerializeField] private Image hpBar;
    [SerializeField] private Image hpBarEnd;

    Character character;

    private void Start()
    {
        character = Character.instance;
    }

    private void Update()
    {
        hpText.text = ((int)character.status.currentHP).ToString() + "/" + ((int)character.status.HP.Value).ToString();
        rengenText.text = "+" + ((int)character.regenRate).ToString() + "/วิ";
        hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, character.status.currentHP / character.status.HP.Value, Time.deltaTime * 2);
        if (hpBar.fillAmount >= .9999f)
            hpBarEnd.enabled = true;
        else
            hpBarEnd.enabled = false;
    }
}
