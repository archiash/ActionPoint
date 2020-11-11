using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffEffect : UsableItem.UsageEffect
{
    public SubStatType statType;
    public float value;
    public ModifierType modifierType;
    public int duration;

    public override bool Use(Status user, object source = null)
    {
        Stat stat = (Stat)user.GetType().GetField(statType.ToString()).GetValue(user);
        for (int i = 0; i < stat.modifiers.Count; i++)
        {
            if (stat.modifiers[i].source == source && stat.modifiers[i].value == value && stat.modifiers[i].type == modifierType)
            {
                stat.modifiers[i].time += duration;
                return true;
            }                
        }
        stat.AddModifier(new Modifier(value, source ?? this, modifierType, Modifier.ModifierTime.Hunt, duration));        
        return true;
    }
}
