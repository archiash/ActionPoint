using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillSystem;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public Skill skill;
    public bool learned = false;

    public SkillSlot[] condition;
    public Image[] beforeLink;
    public Image[] nextLink;

    public void OnLearnSkill()
    {
        if(CheckCondition())
        {

            learned = true;

            foreach(Image i in beforeLink)
                i.color = Color.green;
                this.gameObject.GetComponent<Image>().color = Color.green;
            foreach (Image i in nextLink)
                i.color = new Color32(47, 236, 255, 255);
        }
    }

    public void Update()
    {   if (CheckCondition())            
            this.gameObject.GetComponent<Image>().color = new Color32(47,236,255,255) ;
    }

    public void ShowDetailButton()
    {       
        ShowSkillDetail.instance.ShowDetail(this);
    }

    public bool CheckCondition()
    {

        if (condition == null) return true;

        if (learned == true) return false;

        foreach(SkillSlot skill in condition)
        {
            if(skill.learned != true)
            {
                return false;
            }
        }

        return true;
    }

}
