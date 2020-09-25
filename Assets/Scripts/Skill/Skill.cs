using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum DamageType
{
    Physic,Magic
}

public enum CriticalType
{
    Able,Unable,Sure
}

[CreateAssetMenu]
public class Skill : ScriptableObject
{
    [System.Serializable]
    public class SkillAction
    {
        public enum Target { Self,Target}        
        public enum SkillType {Damage,Heal,Buff}
        [System.Serializable]
        public class ActionModifier
        {
            public StatType statType;
            public MainStatType mainType;
            public SubStatType subType;
            public float amount;
            
        }

        public CriticalType critical;
        public Target target = Target.Target;
        public SkillType skillType;
        public DamageType damageType;
        public float value;
        public List<ActionModifier> actionModifiers = new List<ActionModifier>();

        public SubStatType statToBuff;
        public Modifier.ModifierType buffModType;
        public int turnDaration;

    }

    public string skillName;
    public string skillDesc;
    public List<SkillAction> actions = new List<SkillAction>();
    public float manaCost;
    public int coolTime;

    public bool UseSkill(Status user, out float[] damage,out DamageType[] damageTypes)
    {
        damage = new float[actions.Count];
        damageTypes = new DamageType[actions.Count];
        if (user.currentMP > manaCost)
        {
            user.currentMP -= manaCost;
            for (int i = 0; i < actions.Count; i++)
            {
                damage[i] = 0;
                if (actions[i].target == SkillAction.Target.Target)
                {
                    damageTypes[i] = actions[i].damageType;
                    if(actions[i].skillType == SkillAction.SkillType.Damage)
                    {
                        for(int j = 0;j<actions[i].actionModifiers.Count;j++)
                        {
                            damage[i] = actions[i].value;
                            if(actions[i].actionModifiers[j].statType == StatType.Main)
                            {
                                switch(actions[i].actionModifiers[j].mainType)
                                {
                                    case MainStatType.STR:
                                        damage[i] += user.STR.Value * actions[i].actionModifiers[j].amount;
                                        break;
                                    case MainStatType.DEX:
                                        damage[i] += user.DEX.Value * actions[i].actionModifiers[j].amount;
                                        break;
                                    case MainStatType.AGI:
                                        damage[i] += user.AGI.Value * actions[i].actionModifiers[j].amount;
                                        break;
                                    case MainStatType.INT:
                                        damage[i] += user.INT.Value * actions[i].actionModifiers[j].amount;
                                        break;
                                    case MainStatType.CON:
                                        damage[i] += user.CON.Value * actions[i].actionModifiers[j].amount;
                                        break;
                                }
                            }
                            else if(actions[i].actionModifiers[j].statType == StatType.Sub)
                            {
                                switch (actions[i].actionModifiers[j].subType)
                                {
                                    case SubStatType.HP:
                                        damage[i] += user.HP.Value * actions[i].actionModifiers[j].amount;
                                        break;
                                    case SubStatType.MP:
                                        damage[i] += user.MP.Value * actions[i].actionModifiers[j].amount;
                                        break;
                                    case SubStatType.PAtk:
                                        damage[i] += user.PAtk.Value * actions[i].actionModifiers[j].amount;
                                        break;
                                    case SubStatType.PDef:
                                        damage[i] += user.PDef.Value * actions[i].actionModifiers[j].amount;
                                        break;
                                    case SubStatType.MAtk:
                                        damage[i] += user.MAtk.Value * actions[i].actionModifiers[j].amount;
                                        break;
                                    case SubStatType.MDef:
                                        damage[i] += user.MDef.Value * actions[i].actionModifiers[j].amount;
                                        break;
                                    case SubStatType.Spd:
                                        damage[i] += user.Spd.Value * actions[i].actionModifiers[j].amount;
                                        break;
                                    case SubStatType.Hit:
                                        damage[i] += user.Hit.Value * actions[i].actionModifiers[j].amount;
                                        break;
                                    case SubStatType.Eva:
                                        damage[i] += user.Eva.Value * actions[i].actionModifiers[j].amount;
                                        break;
                                    case SubStatType.Crate:
                                        damage[i] += user.Crate.Value * actions[i].actionModifiers[j].amount;
                                        break;
                                    case SubStatType.Cdmg:
                                        damage[i] += user.Cdmg.Value * actions[i].actionModifiers[j].amount;
                                        break;
                                }
                            }                                   
                        }
                    }
                }
                if(actions[i].target == SkillAction.Target.Self)
                {
                    float heal = actions[i].value;
                    for (int j = 0; j < actions[i].actionModifiers.Count; j++)
                    {
                        
                        if (actions[i].actionModifiers[j].statType == StatType.Main)
                        {
                            switch (actions[i].actionModifiers[j].mainType)
                            {
                                case MainStatType.STR:
                                    heal += user.STR.Value * actions[i].actionModifiers[j].amount;
                                    break;
                                case MainStatType.DEX:
                                    heal += user.DEX.Value * actions[i].actionModifiers[j].amount;
                                    break;
                                case MainStatType.AGI:
                                    heal += user.AGI.Value * actions[i].actionModifiers[j].amount;
                                    break;
                                case MainStatType.INT:
                                    heal += user.INT.Value * actions[i].actionModifiers[j].amount;
                                    break;
                                case MainStatType.CON:
                                    heal += user.CON.Value * actions[i].actionModifiers[j].amount;
                                    break;
                            }
                        }
                        else if (actions[i].actionModifiers[j].statType == StatType.Sub)
                        {
                            switch (actions[i].actionModifiers[j].subType)
                            {
                                case SubStatType.HP:
                                    heal += user.HP.Value * actions[i].actionModifiers[j].amount;
                                    break;
                                case SubStatType.MP:
                                    heal += user.MP.Value * actions[i].actionModifiers[j].amount;
                                    break;
                                case SubStatType.PAtk:
                                    heal += user.PAtk.Value * actions[i].actionModifiers[j].amount;
                                    break;
                                case SubStatType.PDef:
                                    heal += user.PDef.Value * actions[i].actionModifiers[j].amount;
                                    break;
                                case SubStatType.MAtk:
                                    heal += user.MAtk.Value * actions[i].actionModifiers[j].amount;
                                    break;
                                case SubStatType.MDef:
                                    heal += user.MDef.Value * actions[i].actionModifiers[j].amount;
                                    break;
                                case SubStatType.Spd:
                                    heal += user.Spd.Value * actions[i].actionModifiers[j].amount;
                                    break;
                                case SubStatType.Hit:
                                    heal += user.Hit.Value * actions[i].actionModifiers[j].amount;
                                    break;
                                case SubStatType.Eva:
                                    heal += user.Eva.Value * actions[i].actionModifiers[j].amount;
                                    break;
                                case SubStatType.Crate:
                                    heal += user.Crate.Value * actions[i].actionModifiers[j].amount;
                                    break;
                                case SubStatType.Cdmg:
                                    heal += user.Cdmg.Value * actions[i].actionModifiers[j].amount;
                                    break;
                            }
                        }
                    }
                    if (heal <= 0)
                    {
                        heal = 0;
                    }
                    user.currentHP += heal;
                    damage[i] = heal;
                    if(user.currentHP > user.HP.Value)
                    {
                        user.currentHP = user.HP.Value;
                    }
                }
               
            }
            return true;
        }
        return false;
    }

    public virtual void ActiveSkill(Status userStat,Status enermyStat)
    {
        Debug.Log(skillName);
        for(int i = 0;i< actions.Count;i++)
        {          
            Status target = new Status();
            if(actions[i].target == SkillAction.Target.Target)
            {
                target = enermyStat;
            }else if(actions[i].target == SkillAction.Target.Self)
            {
                target = userStat;
            }

            if(actions[i].skillType == SkillAction.SkillType.Damage)
            {
                DamageAction(userStat, target, i);
            }else if(actions[i].skillType == SkillAction.SkillType.Heal)
            {
                HealAction(userStat, target, i);
            }else if(actions[i].skillType == SkillAction.SkillType.Buff)
            {
                BuffAction(userStat, target, i);
            }
        }
    }

    void DamageAction(Status user,Status target,int i)
    {
        float damage = actions[i].value;
        for (int m = 0; m < actions[i].actionModifiers.Count; m++)
        {
            if (actions[i].actionModifiers[m].statType == StatType.Main)
            {
                var stat = (user.GetType().GetField(actions[i].actionModifiers[m].mainType.ToString()).GetValue(user));
                var value = stat.GetType().GetProperty("Value").GetValue(stat);
                damage += (float)value;
            }
            else if (actions[i].actionModifiers[m].statType == StatType.Sub)
            {
                var stat = (user.GetType().GetField(actions[i].actionModifiers[m].subType.ToString()).GetValue(user));
                var value = stat.GetType().GetProperty("Value").GetValue(stat);
                damage += (float)value;
            }

        }

        if (actions[i].critical == CriticalType.Sure) 
        {
            damage = Uility.CriticalDamage(damage, user.Cdmg.Value);                   
        }
        else if(actions[i].critical == CriticalType.Able)                    
        {
            if (Uility.IsCritical(user.Crate.Value))
                damage = Uility.CriticalDamage(damage, user.Cdmg.Value);
        }
        

        target.GetDamage(ref damage, actions[i].damageType,user);
    }
    void HealAction(Status user, Status target, int i)
    {
        float heal = actions[i].value;
        for (int m = 0; m < actions[i].actionModifiers.Count; m++)
        {
            if (actions[i].actionModifiers[m].statType == StatType.Main)
            {
                var stat = (user.GetType().GetField(actions[i].actionModifiers[m].mainType.ToString()).GetValue(user));
                var value = stat.GetType().GetProperty("Value").GetValue(stat);
                Debug.Log(actions[i].actionModifiers[m].mainType + " " + value);
                heal += (float)value * actions[i].actionModifiers[m].amount;
            }
            else if (actions[i].actionModifiers[m].statType == StatType.Sub)
            {
                var stat = (user.GetType().GetField(actions[i].actionModifiers[m].subType.ToString()).GetValue(user));
                var value = stat.GetType().GetProperty("Value").GetValue(stat);
                Debug.Log(actions[i].actionModifiers[m].subType + " " + value);
                heal += (float)value * actions[i].actionModifiers[m].amount;
            }

        }
        if(target.currentHP + heal > target.HP.Value)
        {
            heal = target.HP.Value - target.currentHP;
            target.currentHP = target.HP.Value;
        }else
        {
            target.currentHP += heal;
        }        
        Debug.Log("Heal : " + heal);
    }
    void BuffAction(Status user, Status target, int i)
    {
        float buffValue = actions[i].value;
        for (int m = 0; m < actions[i].actionModifiers.Count; m++)
        {
            if (actions[i].actionModifiers[m].statType == StatType.Main)
            {
                var stat = (user.GetType().GetField(actions[i].actionModifiers[m].mainType.ToString()).GetValue(user));
                var value = stat.GetType().GetProperty("Value").GetValue(stat);
                Debug.Log(actions[i].actionModifiers[m].mainType + " " + value);
                buffValue += (float)value;
            }
            else if (actions[i].actionModifiers[m].statType == StatType.Sub)
            {
                var stat = (user.GetType().GetField(actions[i].actionModifiers[m].subType.ToString()).GetValue(user));
                var value = stat.GetType().GetProperty("Value").GetValue(stat);
                Debug.Log(actions[i].actionModifiers[m].subType + " " + value);
                buffValue += (float)value;
            }

        }
        
        Stat statToBuff = (Stat)(target.GetType().GetField(actions[i].statToBuff.ToString()).GetValue(target));
        float oldValue = statToBuff.Value;
        statToBuff.AddModifier(new Modifier(buffValue, this, actions[i].buffModType, Modifier.ModifierTime.Turn, actions[i].turnDaration));
        string debug = "";
        if (target == user)
            debug += ("User " + actions[i].statToBuff.ToString() + " + " + buffValue.ToString());
        else
            debug +=("Target " + actions[i].statToBuff.ToString() + " + " + buffValue.ToString());

        if (actions[i].buffModType == Modifier.ModifierType.Pecentage)
            debug += "%";
        Debug.Log(debug);
        Debug.Log($"{actions[i].statToBuff}: {oldValue} -> {statToBuff.Value}");
    }
}

