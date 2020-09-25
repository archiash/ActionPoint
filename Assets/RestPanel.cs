using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RestPanel : MonoBehaviour
{
    public int level;
    //public TextMeshProUGUI levelText;
    public TextMeshProUGUI costText;
    //public TextMeshProUGUI descText;

    int cost;

    [SerializeField] Button button;

    Character character;
    PointManager pointManager;
    private void Start()
    {
        character = Character.instance;
        pointManager = PointManager.instance;
    }

    private void Update()
    {
        cost = Mathf.RoundToInt(character.status.HP.Value - character.status.currentHP) * 2;
        costText.text = "จ่าย " + cost + " Point";
        if(!CheckCondition())
        {
            button.interactable = false;
        }else
        {
            button.interactable = true;
        }
    }

    public void OnRestButton()
    {
        if(CheckCondition())
        {
            pointManager.UseAction(cost);
            character.status.currentHP = character.status.HP.Value;
            gameObject.SetActive(false);
        }
    }

    bool CheckCondition()
    {
        if (pointManager.GetActionPoint >= cost)
            return true;

        return false;
    }
}
