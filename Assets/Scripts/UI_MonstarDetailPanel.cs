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
    [SerializeField] TextMeshProUGUI pen;
    [SerializeField] TextMeshProUGUI mp;
    [SerializeField] TextMeshProUGUI mag;
    [SerializeField] TextMeshProUGUI res;
    [SerializeField] TextMeshProUGUI neu;
    [SerializeField] TextMeshProUGUI spd;
    [SerializeField] TextMeshProUGUI eva;
    [SerializeField] TextMeshProUGUI hit;
    [SerializeField] TextMeshProUGUI crate;
    [SerializeField] TextMeshProUGUI cdmg;
    [SerializeField] TextMeshProUGUI cres;

    [SerializeField] TextMeshProUGUI usePoint;

    [SerializeField] GameObject panel;
    [SerializeField] Button button;

    [SerializeField] TextMeshProUGUI powerlizeText;

    private int powerlize;
    public int Powerlize
    {
        get { return powerlize; }
        set { powerlize = value;
            
        
        }
    }

    public float PowerlizeMultiply
    {
        get { return 1 + ((powerlize - 1) * 0.5f); }
    }

    private void Awake()
    {
        insctance = this;
    }

    public void ShowDetail(Monster _monster)
    {
        panel.SetActive(true);

        monster = _monster;
        Powerlize = 1;
        powerlizeText.text = $"x{Powerlize}";
        monsterName.text = monster.Name;
        monsterImage.sprite = monster.sprite;
        monsterSubName.text = monster.Desc;
        usePoint.text = $"{monster.usePoint * PowerlizeMultiply} Point";
        skillText.text = monster.GetSkillList();

        hp.text = $"{monster.status.HP.Value * PowerlizeMultiply}";
        atk.text = $"{monster.status.PAtk.Value * PowerlizeMultiply}";
        def.text = $"{monster.status.PDef.Value * PowerlizeMultiply}";
        pen.text = $"{monster.status.Pen.Value * PowerlizeMultiply}";
        mp.text = $"{monster.status.MP.Value * PowerlizeMultiply}";
        mag.text = $"{monster.status.MAtk.Value * PowerlizeMultiply}";
        res.text = $"{monster.status.MDef.Value * PowerlizeMultiply}";
        neu.text = $"{monster.status.Neu.Value * PowerlizeMultiply}";
        spd.text = $"{monster.status.Spd.Value * PowerlizeMultiply}";
        eva.text = $"{monster.status.Eva.Value * PowerlizeMultiply}";
        hit.text = $"{monster.status.Hit.Value * PowerlizeMultiply}";
        crate.text = $"{monster.status.Crate.Value * PowerlizeMultiply}";
        cdmg.text = $"{monster.status.Cdmg.Value * PowerlizeMultiply}";
        cres.text = $"{monster.status.Cres.Value * PowerlizeMultiply}";

        if (PointManager.instance.GetActionPoint >= monster.usePoint * PowerlizeMultiply && Inventory.instance.HaveEmptySpace())
        {
            button.interactable = true;
            button.GetComponentInChildren<TextMeshProUGUI>().text = "สู้";
        }
        else
        {
            button.interactable = false;
            if(PointManager.instance.GetActionPoint < monster.usePoint * PowerlizeMultiply)
                button.GetComponentInChildren<TextMeshProUGUI>().text = "Point ไม่พอ";
            else
                button.GetComponentInChildren<TextMeshProUGUI>().text = "กระเป๋าเต็ม";
        }
    }

    public void ChangePowerlize()
    {
        if(Powerlize < 3)
        {
            Powerlize++;
        }
        else
        {
            Powerlize = 1;
        }

        UpdateDetail();
        
    }

    private void UpdateDetail()
    {
        usePoint.text = $"{monster.usePoint * PowerlizeMultiply} Point";

        powerlizeText.text = $"x{Powerlize}";
        hp.text = $"{monster.status.HP.Value * PowerlizeMultiply}";
        atk.text = $"{monster.status.PAtk.Value * PowerlizeMultiply}";
        def.text = $"{monster.status.PDef.Value * PowerlizeMultiply}";
        pen.text = $"{monster.status.Pen.Value * PowerlizeMultiply}";
        mp.text = $"{monster.status.MP.Value * PowerlizeMultiply}";
        mag.text = $"{monster.status.MAtk.Value * PowerlizeMultiply}";
        res.text = $"{monster.status.MDef.Value * PowerlizeMultiply}";
        neu.text = $"{monster.status.Neu.Value * PowerlizeMultiply}";
        spd.text = $"{monster.status.Spd.Value * PowerlizeMultiply}";
        eva.text = $"{monster.status.Eva.Value * PowerlizeMultiply}";
        hit.text = $"{monster.status.Hit.Value * PowerlizeMultiply}";
        crate.text = $"{monster.status.Crate.Value * PowerlizeMultiply}";
        cdmg.text = $"{monster.status.Cdmg.Value * PowerlizeMultiply}";
        cres.text = $"{monster.status.Cres.Value * PowerlizeMultiply}";

        if (PointManager.instance.GetActionPoint >= monster.usePoint * PowerlizeMultiply && Inventory.instance.HaveEmptySpace())
        {
            button.interactable = true;
            button.GetComponentInChildren<TextMeshProUGUI>().text = "สู้";
        }
        else
        {
            button.interactable = false;
            if (PointManager.instance.GetActionPoint < monster.usePoint * PowerlizeMultiply)
                button.GetComponentInChildren<TextMeshProUGUI>().text = "Point ไม่พอ";
            else
                button.GetComponentInChildren<TextMeshProUGUI>().text = "กระเป๋าเต็ม";
        }
    }

    public void Hunt()
    {
        PointManager.instance.UseAction(this.monster.usePoint * PowerlizeMultiply);
        Monster monster = Instantiate(this.monster);
        monster.Powerlize(PowerlizeMultiply);
        HuntingManager.instance.Setup(monster,powerlize:Powerlize);
        HuntingManager.instance.Hunt();
        panel.SetActive(false);
    }
}
