using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MonstarDetailPanel : MonoBehaviour
{
    public static UI_MonstarDetailPanel insctance;

    Monster monster;

    [SerializeField]Image monsterImage;
    [SerializeField] TextMeshProUGUI monsterName;
    [SerializeField] TextMeshProUGUI monsterSubName;
    [SerializeField] TextMeshProUGUI skillText;

    [SerializeField] TextMeshProUGUI hp;
    [SerializeField] TextMeshProUGUI atk;
    [SerializeField] TextMeshProUGUI def;
    [SerializeField] TextMeshProUGUI mp;
    [SerializeField] TextMeshProUGUI mag;
    [SerializeField] TextMeshProUGUI res;
    [SerializeField] TextMeshProUGUI spd;
    [SerializeField] TextMeshProUGUI eva;
    [SerializeField] TextMeshProUGUI hit;
    [SerializeField] TextMeshProUGUI crate;
    [SerializeField] TextMeshProUGUI cdmg;

    [SerializeField] TextMeshProUGUI usePoint;

    [SerializeField] GameObject panel;
    [SerializeField] Button button;

    private void Awake()
    {
        insctance = this;
    }

    public void ShowDetail(Monster _monster)
    {
        panel.SetActive(true);

        monster = _monster;
        monsterName.text = monster.Name;
        monsterImage.sprite = monster.sprite;
        monsterSubName.text = monster.Desc;
        usePoint.text = monster.usePoint.ToString() + " Point";
        skillText.text = monster.GetSkillList();

        hp.text = monster.status.HP.Value.ToString();
        atk.text = monster.status.PAtk.Value.ToString();
        def.text = monster.status.PDef.Value.ToString();
        mp.text = monster.status.MP.Value.ToString();
        mag.text = monster.status.MAtk.Value.ToString();
        res.text = monster.status.MDef.Value.ToString();
        spd.text = monster.status.Spd.Value.ToString();
        eva.text = monster.status.Eva.Value.ToString();
        hit.text = monster.status.Hit.Value.ToString();
        crate.text = monster.status.Crate.Value.ToString();
        cdmg.text = monster.status.Cdmg.Value.ToString();

        if (PointManager.instance.GetActionPoint >= monster.usePoint && Inventory.instance.HaveEmptySpace())
        {
            button.interactable = true;
            button.GetComponentInChildren<TextMeshProUGUI>().text = "สู้";
        }
        else
        {
            button.interactable = false;
            if(PointManager.instance.GetActionPoint < monster.usePoint)
                button.GetComponentInChildren<TextMeshProUGUI>().text = "Point ไม่พอ";
            else
                button.GetComponentInChildren<TextMeshProUGUI>().text = "กระเป๋าเต็ม";
        }
    }

    public void Hunt()
    {
        PointManager.instance.UseAction(monster.usePoint);
        HuntingManager.instance.Setup(monster);
        HuntingManager.instance.Hunt();
        panel.SetActive(false);
    }
}
