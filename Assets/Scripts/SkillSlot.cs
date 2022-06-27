using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillSystem;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public Skill skill;
    [SerializeField] Image icon;
    public void OnCreate(Skill skill)
    {
        this.skill = skill;
        icon.sprite = skill.skillIcon;
    }

    public void OnButton()
    {
        SkillDetailPanel.instance.OnShow(skill);
    }
}
