using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Status
{
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
    public Stat MAtk;
    public Stat MDef;
    public Stat Spd;
    public Stat Hit;
    public Stat Eva;
    public Stat Crate;
    public Stat Cdmg;

    public void GetDamage(ref float damage,DamageType damageType = DamageType.Physic,Status attacker = null)
    {
        if(attacker != null)
        {
            if (!Uility.Hit(attacker.Hit.Value, Eva.Value))
            {
                Debug.Log("Missed");
                damage = -1;
                return;
            }
        }

        if (damageType == DamageType.Physic)
        {
            damage -= PDef.Value / 2;
            damage = Uility.Damage(damage);

            if (damage <= 1)
                damage = 1;
            currentHP -= damage;
        }
        else if(damageType == DamageType.Magic)
        {
            damage -= MDef.Value / 2;
            damage = Uility.Damage(damage);

            if (damage <= 1)
                damage = 1;
            currentHP -= damage;
        }

    }

}
