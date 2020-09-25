using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEffect : UsableItem.UsageEffect
{
    public float value;

    public override bool Use(Status user, object source = null)
    {
        if (user.currentHP >= user.HP.Value)
            return false;

        user.currentHP += value;
        if (user.currentHP >= user.HP.Value)
            user.currentHP = user.HP.Value;
        return true;
    }
}
