using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HighFlex/DamageSkill")]
public class DamgaeSkill : Skill
{
    List<List<int>> writeData = new List<List<int>>();

    public class DamageSkillAction
    {

    }

    public override bool Use<U, T>(U user, T enermy, ArenaType arena = ArenaType.Hunting)
    {
            
        return base.Use(user, enermy, arena);
    }

    public override void Activate<U, T>(U user, T enermy, ArenaType arena = ArenaType.Hunting)
    {
        
    }
}
