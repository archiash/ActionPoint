using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Status
{
    public delegate bool CounterSkill(ref float damage,Status Attacker);
    public CounterSkill counterSkill;
    [Header("MainStatus")]
    public MainStat STR;
    public MainStat DEX;
    public MainStat AGI;
    public MainStat INT;
    public MainStat CON;

    [Header("Staus")]
    public Stat HP;
    public float currentHP;
    public Stat MP;
    public float currentMP;
    public Stat PAtk;
    public Stat PDef;
    public Stat Pen;
    public Stat MAtk;
    public Stat MDef;
    public Stat Neu;
    public Stat Spd;
    public Stat Hit;
    public Stat Eva;
    public Stat Crate;
    public Stat Cdmg;
    public Stat Cres;

    public void GetDamage(ref float damage,DamageType damageType = DamageType.Physic,Status attacker = null, int penetrate = 0)
    {
        if(attacker != null)
        {
            if (!Formula.HitFormula(attacker.Hit.Value, Eva.Value))
            {
                Debug.Log("Missed");
                damage = -1;
                return;
            }
        }

        if (damageType == DamageType.Physic)
        {
            damage -= (PDef.Value - (PDef.Value * penetrate / 100)) / 2;
            damage = Formula.Damage(damage);

            if (damage <= 1)
                damage = 1;
            currentHP -= damage;
        }
        else if(damageType == DamageType.Magic)
        {
            damage -= (MDef.Value - (MDef.Value * penetrate / 100)) / 2;
            damage = Formula.Damage(damage);

            if (damage <= 1)
                damage = 1;
            currentHP -= damage;
        }

    }

    public bool GetDamage(ref float damage,Status attacker,bool dodgeAble = true,int evaReduce = 0)
    {
        if (dodgeAble)
        {
            float hitRate = attacker.Hit.Value;
            float evaRate = Eva.Value * (1 - (evaReduce / 100));

            if (Formula.HitFormula(hitRate,evaRate))
            {
                damage *= Random.Range(0.8f, 1.2f);
                currentHP -= damage;
                if (counterSkill != null)
                    counterSkill(ref damage, attacker);
                return true;

            }
            else
            {
                Debug.Log("Miss");
                return false;
            }
        }
        else
        {
            damage *= Random.Range(0.8f, 1.2f);
            currentHP -= damage;
            if (counterSkill != null)
                counterSkill(ref damage, attacker);
            return true;
        }
    }

    public bool isFullMP
    {
        get { return currentMP == MP.Value; }
        set { 
            if(value)
                currentMP = MP.Value; 
        }
    }

}
