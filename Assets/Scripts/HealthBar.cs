using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public TextMeshProUGUI health;
    public TextMeshProUGUI regenrate;
    public Image healthBar;

    Character character;
    private void Start()
    {
        character = Character.instance;
    }

    void Update()
    {
        health.text = string.Format("{0}/{1}", (int)character.status.currentHP, (int)character.status.HP.Value);
        regenrate.text = "+" + ((int)(character.regenRate)).ToString() + "/s";
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, (float)character.status.currentHP / (float)character.status.HP.Value, Time.deltaTime * 5);
    }
}
