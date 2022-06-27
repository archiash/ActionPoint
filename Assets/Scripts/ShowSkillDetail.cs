using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowSkillDetail : MonoBehaviour
{
    #region instance
    public static ShowSkillDetail instance;
    private void Start()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    public SkillSlot skillSlot;
    public TextMeshProUGUI skillName;
    public TextMeshProUGUI skillDetail;
    public GameObject button;

    public void ShowDetail(SkillSlot skillSlot)
    {
        this.skillSlot = skillSlot;

        //if(skillSlot.CheckCondition() /*&& Character.character.skillPoint > 0*/)
        //{
         //   button.SetActive(true);
       // }else
       // {
      //      button.SetActive(false);
      //  }
    }

    public void OnLearnButton()
    {
        //skillSlot.OnLearnSkill();
    }
}
