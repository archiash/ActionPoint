using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class Stack
{
    public string stackName;
    public int maxStack;
    private int stack;
    public int currentStack {
        get { return stack; } 
        set {
            stack = value;
            stack = Math.Min(stack, maxStack);
        }
    }
    public string[] tag;
    public object source;

    public Stack(string stackName, int maxStack, int currentStack, string[] tag = null, object source = null)
    {
        this.stackName = stackName;
        this.maxStack = maxStack;
        this.currentStack = currentStack;
        this.tag = tag;
        this.source = source;
    }
} 

[Serializable]
public class Status
{
    public Dictionary<string, Stack> stacks;

    public delegate bool CounterSkill(ref float damage,Status Attacker);
    public CounterSkill counterSkill;

    public delegate bool OnGetHitEffect(AttackData attackData, Status user);
    public OnGetHitEffect onGetHitEffect;

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

    public bool GetDamage(ref AttackData attackData, bool dodgeable = true, float evadeReduce = 0)
    {
        float damage = attackData.damage;
        DamageType damageType = attackData.damageType;

        float additionalEvadeChange = 0;
        float additionalPercantageDamageToRecive = 0;

        bool isRogue = this == Character.instance.status && Character.instance.Class == Character.CharacterClass.Rogue;

        if (isRogue)
        {
            Debug.Log("Rogue!! DougeRate + 10%");
            additionalEvadeChange += 10;
            additionalPercantageDamageToRecive += 20;
        }

        if (!dodgeable)
        {
            if (isRogue) Debug.Log("Rogue!! GetDamage + 20%");
            damage *= UnityEngine.Random.Range(0.8f, 1.2f);
            if (isRogue) Debug.Log("Rogue!! GetDamage: " + damage);
            damage *= (1 + (additionalPercantageDamageToRecive / 100f));
            if (isRogue) Debug.Log("Rogue!! GetDamage When Bonus: " + damage);
            currentHP -= damage;
            if (counterSkill != null) counterSkill(ref damage, attackData.attacker);
            if (onGetHitEffect != null) onGetHitEffect(attackData, this);
            return true;
        }

        float hitRate = attackData.attacker.Hit.Value;
        float evaRate = Eva.Value * (1 - (evadeReduce / 100));

        if (!Formula.HitFormula(hitRate, evaRate, -additionalEvadeChange))
        {
            Debug.Log("Miss");
            return false;
        }

        if (isRogue) Debug.Log("Rogue!! GetDamage + 20%");
        damage *= UnityEngine.Random.Range(0.8f, 1.2f);
        if (isRogue) Debug.Log("Rogue!! GetDamage: " + damage);
        damage *= (1 + (additionalPercantageDamageToRecive / 100f));
        if (isRogue) Debug.Log("Rogue!! GetDamage When Bonus: " + damage);
        currentHP -= damage;
        if(counterSkill != null) counterSkill(ref damage, attackData.attacker);
        if (onGetHitEffect != null) onGetHitEffect(attackData, this);
        return true;

    }
/*
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
    */
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

    public Stat GetStat(SubStatType statusEnum)
    {
        switch (statusEnum)
        {
            case SubStatType.HP:
                return HP;
            case SubStatType.MP:
                return MP;
            case SubStatType.PAtk:
                return PAtk;
            case SubStatType.PDef:
                return PDef;
            case SubStatType.MAtk:
                return MAtk;
            case SubStatType.MDef:
                return MDef;
            case SubStatType.Spd:
                return Spd;
            case SubStatType.Hit:

                return Hit;
            case SubStatType.Eva:
                return Eva;
            case SubStatType.Crate:
                return Crate;
            case SubStatType.Cdmg:
                return Cdmg;
            case SubStatType.Pen:
                return Pen;
            case SubStatType.Neu:
                return Neu;
            case SubStatType.Cres:
                return Cres;
        }
        return null;
    }
    public MainStat GetStat(MainStatType statusEnum)
    {
        switch (statusEnum)
        {
            case MainStatType.STR:
                return STR;
            case MainStatType.DEX:
                return DEX;
            case MainStatType.AGI:
                return AGI;
            case MainStatType.INT:
                return INT;
            case MainStatType.CON:
                return CON;
        }
        return null;
    }

}
