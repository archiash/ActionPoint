using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HuntingBar : MonoBehaviour
{
    public Monster monster;

    public TextMeshProUGUI actionNameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI redUsePointText;
    public Image icon;

    public void OnCreate()
    {
        icon.sprite = monster.sprite;
        actionNameText.text = monster.Name;
        descriptionText.text = monster.Desc;
        redUsePointText.text = monster.usePoint.ToString();
    }
    /*
    public void OnActionButton()
    {
        if(PointManager.instance.GetAction() >= monster.usePoint && Character.character.currentHealth > 0)
        {
            HuntingManager.instance.Hunt(monster);
            PointManager.instance.UseAction(monster.usePoint);
        }
            
    }

    public void OnCheckDetail()
    {
        MonsterDetail.instance.detailPanel.SetActive(true);
        MonsterDetail.instance.ShowMonsterDetail(this);
    }*/
    public void DestroyBar()
    {
        Destroy(this.gameObject);
    }
    private void Start()
    {
        OnCreate();
    }
}
