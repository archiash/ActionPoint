using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Counter")]
public class CounterSkill : Skill
{
    public SkillType skillType;
    public float CounterModify;
    public ModifierType modifier;    

    public bool Use(ref float damage,Status attacker)
    {
        bool isHit = false; ;
       float counterDamage = damage;
       if(counterDamage > 0)
       {
            if (modifier == ModifierType.Flat)
            {
                counterDamage = CounterModify;
                isHit = attacker.GetDamage(ref counterDamage,attacker);
            }
            else
            {
                counterDamage *= counterDamage / 100f;
                isHit = attacker.GetDamage(ref counterDamage,attacker);
            }
            if (isHit )
                Debug.Log(skillName + " Deal " + counterDamage + " to Attacker");
            return true;
       }
        return false; 
    }
}
