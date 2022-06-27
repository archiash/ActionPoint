using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanel : MonoBehaviour
{
    [SerializeField] SkillSlot pfSkillSlot;
    [SerializeField] Transform skillListParent;
    Character character;

    public void ShowSkillList()
    {
        character = Character.instance;
        for(int i = 0; i<character.learnedSkill.Count;i++)
        {
            SkillSlot slot = Instantiate(pfSkillSlot, skillListParent);
            slot.OnCreate(character.learnedSkill[i].skill);
        }
    }

}
