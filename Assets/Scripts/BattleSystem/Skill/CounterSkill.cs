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
                AttackData attackData = new AttackData(attacker, counterDamage);
                isHit = attacker.GetDamage(ref attackData);
            }
            else
            {
                counterDamage *= counterDamage / 100f;
                AttackData attackData = new AttackData(attacker, counterDamage);
                isHit = attacker.GetDamage(ref attackData);
            }
            if (isHit )
                Debug.Log(skillName + " Deal " + counterDamage + " to Attacker");
            return true;
       }
        return false; 
    }
}
