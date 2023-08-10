using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGetHitEffect
{
    public Dictionary<string, object> effectParamiter;
    
    public OnGetHitEffect()
    {
        effectParamiter = new Dictionary<string, object>();
    }

    public bool OnActivate(AttackData attackData, Status user)
    {
        string effectType = (string)effectParamiter["Effect"];
        if (effectType == "Buff")
        {
            BuffOnHit(attackData.attacker, user);
        }
        return true;
    }

    public void BuffOnHit(Status attacker, Status user)
    {
        Debug.LogWarning("OGHE!!");
        Status target = null;
        object t = effectParamiter["Target"];
        if(t.GetType().Equals(typeof(string)))
        {
            if((string)t == "Attacker")
            {
                target = attacker;
            }

            if((string)t == "User")
            {
                target = user;
            }
        }
        string buff = (string)effectParamiter["Buff"];
        bool IS_CONTAINS_KEYWORD = buff.Contains("Stat:");
        if (!IS_CONTAINS_KEYWORD)
        {
            Debug.LogError($"Buff not Contain stat: keyword in {buff}");
            return;
        }
        buff = buff.Replace("Stat:", "");
        int statEnumIndex = -1;
        bool CANT_PARSE_TO_INT = !int.TryParse(buff, out statEnumIndex);
        if (CANT_PARSE_TO_INT)
        {
            Debug.LogError($"Can't Parse Stat Enum Index of OnGetHitEffect of Stat:{buff}");
            return;
        }
        ModifierType modifierType = ModifierType.Flat;
        ModifierType.TryParse((string)effectParamiter["ModifierValueType"], out modifierType);
        float value = 0;
        float.TryParse((string)effectParamiter["ModifierValue"], out value);
        string m = (string)effectParamiter["ModifierTime"];
        string[] modifierTimeData = m.Split(":");
        Modifier.ModifierTime modifierTimeType = Modifier.ModifierTime.Step;
        if (modifierTimeData[0] == "Turn") modifierTimeType = Modifier.ModifierTime.Turn;
        int modifierTimeValue = -1;
        CANT_PARSE_TO_INT = !int.TryParse(modifierTimeData[1], out modifierTimeValue);
        if (CANT_PARSE_TO_INT)
        {
            Debug.LogError($"Can't Parse ModifierTimeValue of OnGetHitEffect");
            return;
        }
        object source = effectParamiter["Source"];
        target.GetStat((SubStatType)statEnumIndex).AddModifier(new Modifier(value, source, modifierType, modifierTimeType, modifierTimeValue));
    }
} 
