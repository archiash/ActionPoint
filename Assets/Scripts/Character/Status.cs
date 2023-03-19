using System;
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

    public Status()
    {
        STR = new MainStat();
        DEX = new MainStat();
        AGI = new MainStat();
        INT = new MainStat();
        CON = new MainStat();

        HP = new Stat();
        currentHP = 0;
        MP = new Stat();
        currentMP = 0;
        PAtk = new Stat();
        PDef = new Stat();
        Pen = new Stat();
        MAtk = new Stat();
        MDef = new Stat();
        Neu = new Stat();
        Spd = new Stat();
        Hit = new Stat();
        Eva = new Stat();
        Crate = new Stat();
        Cdmg = new Stat();
        Cres = new Stat();
    }

    public void SetFollowerMainStat(int[] stat)
    {
        STR.baseValue = stat[0];
        DEX.baseValue = stat[1];
        AGI.baseValue = stat[2];
        INT.baseValue = stat[3];
        CON.baseValue = stat[4];

    }

    public void GetDamage(ref float damage,DamageType damageType = DamageType.Physic,Status attacker = null, int penetrate = 0)
    {
        float additionalHitChange = 0;
        float additionalDamage = 0;

        bool isRogue = this == Character.instance.status && Character.instance.Class == Character.CharacterClass.Rogue;

        if (isRogue)
        {
            Debug.Log("Rogue!! DougeRate + 10%");
            additionalHitChange -= 10;
            additionalDamage += 20;
        }


        if (attacker != null)
        {
            if (!Formula.HitFormula(attacker.Hit.Value, Eva.Value, additionalHitChange))
            {
                Debug.Log("Missed");
                damage = -1;
                return;
            }
        }

        if (isRogue) Debug.Log("Rogue!! GetDamage + 20%");
        if (damageType == DamageType.Physic)
        {
            damage -= (PDef.Value - (PDef.Value * penetrate / 100)) / 2;
            damage = Formula.Damage(damage);

            if (damage <= 1)
                damage = 1;

            if (isRogue) Debug.Log("Rogue!! GetDamage: " + damage);
            damage *= (1 + (additionalDamage / 100f));
            if (isRogue) Debug.Log("Rogue!! GetDamage When Bonus: " + damage);
            currentHP -= damage;
        }
        else if(damageType == DamageType.Magic)
        {
            damage -= (MDef.Value - (MDef.Value * penetrate / 100)) / 2;
            damage = Formula.Damage(damage);

            if (damage <= 1)
                damage = 1;

            if (isRogue) Debug.Log("Rogue!! GetDamage: " + damage);
            damage *= (1 + (additionalDamage / 100f));
            if (isRogue) Debug.Log("Rogue!! GetDamage When Bonus: " + damage);
            currentHP -= damage;
        }

    }

    public bool GetDamage(ref float damage,Status attacker,bool dodgeAble = true,int evaReduce = 0)
    {
        float additionalHitChange = 0;
        float additionalDamage = 0;

        bool isRogue = this == Character.instance.status && Character.instance.Class == Character.CharacterClass.Rogue;

        if (isRogue)
        {
            Debug.Log("Rogue!! DougeRate + 10%");
            additionalHitChange -= 10;
            additionalDamage += 20;
        }

        if (dodgeAble)
        {
            float hitRate = attacker.Hit.Value;
            float evaRate = Eva.Value * (1 - (evaReduce / 100));


            if (Formula.HitFormula(hitRate,evaRate,additionalHitChange))
            {
                if (isRogue) Debug.Log("Rogue!! GetDamage + 20%");
                damage *= UnityEngine.Random.Range(0.8f, 1.2f);
                if (isRogue) Debug.Log("Rogue!! GetDamage: " + damage);
                damage *= (1 + (additionalDamage / 100f));
                if (isRogue) Debug.Log("Rogue!! GetDamage When Bonus: " + damage);
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
            if (isRogue) Debug.Log("Rogue!! GetDamage + 20%");
            damage *= UnityEngine.Random.Range(0.8f, 1.2f);
            if (isRogue) Debug.Log("Rogue!! GetDamage: " + damage);
            damage *= (1 + (additionalDamage / 100f));
            if (isRogue) Debug.Log("Rogue!! GetDamage When Bonus: " + damage);
            currentHP -= damage;
            if (counterSkill != null)
                counterSkill(ref damage, attacker);
            return true;
        }
    }

    public bool isFullMP
    {
        get {
            if (currentMP >= MP.Value)
            {
                currentMP = MP.Value;
                return true;
            }
            return false;
        }
        set { 
            if(value)
                currentMP = MP.Value; 
        }
    }


    public void RemoveInHuntModifier()
    {
        foreach (var statName in Enum.GetValues(typeof(SubStatType)))
        {

            Stat stat = (Stat)(this.GetType().GetField(statName.ToString()).GetValue(this));
            for (int i = stat.modifiers.Count - 1; i >+ 0; i--)
            {

                if (stat.modifiers[i].timeType == Modifier.ModifierTime.Turn)
                {
                    stat.RemoveModifier(stat.modifiers[i]);
                }
            }
        }
    }

}
