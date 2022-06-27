using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Formula
{
    public static bool HitFormula(float hit, float eva,float additionalHitChange = 0)
    {
        float hitChange = 1 - Mathf.Clamp((eva - hit) / eva, 0.2f, 0.8f);
        Debug.Log("HitChange:" + hitChange);
        hitChange += (additionalHitChange / 100f);
        Debug.Log("HitChange When Buff:" + hitChange);
        return Random.value <= hitChange;
    }


    public static bool CriticalFormula(Status user, Status target,ref float damage, CriticalType criticalType = CriticalType.Able)
    {
        float finalDamage = damage;
        bool isCri = false;
        if (criticalType == CriticalType.Able)
        {
            isCri = IsCritical(user.Crate.Value);
        }
        else if (criticalType == CriticalType.Sure)
        {
            isCri = true;
        }else
        {
            isCri = false;
        }

        if(isCri)
        {
            finalDamage *= (1 + (1 + user.Cdmg.Value / 100f) * 10f) / (10 + target.Cres.Value / 10f);
        }

        damage = finalDamage;
        return isCri;
    }

    public static float CriticalDamage(float value, float damage)
    {
        value *= 2f + damage / 100;
        return value;
    }

    public static bool IsCritical(float rate)
    {
        rate = ((50 + rate) / 5) / 100f;
        return Random.value <= rate;
    }

    public static float Damage(float value)
    {
        return Random.Range(value * .8f, value * 1.2f);
    }

    public static float DamageFormula(Status user, Status target, DamageType damageType = DamageType.Physic, bool isSkill = false, float damage = 0, float exPenetrate = 0 ,bool isNormAddExPen = false)
    {
        // isNormAddExPen mean is use Normal Stat Penetrate to Effective in Formula 
        float finalDamage = damage;

        if(!isSkill)
        {
            if(damageType == DamageType.Physic)
            {
                finalDamage = user.PAtk.Value * 100f / (100 + (target.PDef.Value * (1 - user.Pen.Value / 100f)));

            }else
            {
                finalDamage = user.MAtk.Value * 100f / (100 + (target.MDef.Value * (1 - user.Neu.Value / 100f)));
            }    
        }else
        {
            float penetrate = exPenetrate;
            
            if (isNormAddExPen)
            {
                if (damageType == DamageType.Physic)
                    penetrate += user.Pen.Value;
                else
                    penetrate += user.Neu.Value;
            }

            if (penetrate > 100)
                penetrate = 100;

            if (damageType == DamageType.Physic)
            {
                finalDamage *= 100f / (100 + (target.PDef.Value * (1 - penetrate / 100f)));
            }else
            {
                finalDamage *= 100f / (100 + (target.MDef.Value * (1 - penetrate / 100f)));
            }
        }

        return finalDamage;
    }
}
