using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LearnedSkill
{
    public Skill skill;
    public object source;

    public LearnedSkill(Skill skill,object source)
    {
        this.skill = skill;
        this.source = source;
    }
}
