using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Skill/NormalAttackSkill")]
public class PassiveSkill : Skill
{
    public enum NormalAttackEffect {stackEffect}
    public NormalAttackEffect effect;
    public string stackName;

    public BasicSkill.BasicAction.ActionModifier actionModifier;


    public override void Activate<U, T>(U user, T enermy, ArenaType arena = ArenaType.Hunting)
    {
        base.Activate(user, enermy, arena);
    }

    public override bool Use<U, T>(U user, T enermy, ArenaType arena = ArenaType.Hunting)
    {
        return base.Use(user, enermy, arena);
    }

    public override bool Condition()
    {
        return base.Condition();
    }

}
