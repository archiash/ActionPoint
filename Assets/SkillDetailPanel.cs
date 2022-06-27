using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SkillDetailPanel : MonoBehaviour
{
    public static SkillDetailPanel instance;

    Skill skill;
    [SerializeField] TextMeshProUGUI skillName;
    [SerializeField] Image skillIcon;
    [SerializeField] TextMeshProUGUI skillDesc;
    [SerializeField] Button button;
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] GameObject skillDetailPanel;

    private void Start()
    {
        if (instance == null)
            instance = this;
    }

    public void OnShow(Skill skill)
    {
        this.skill = skill;
        skillName.text = skill.skillName;
        skillIcon.sprite = skill.skillIcon;
        skillDesc.text = skill.skillDesc;



        skillDetailPanel.SetActive(true);   
    }

}
