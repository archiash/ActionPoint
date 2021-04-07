﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Runtime.Serialization;

public enum SkillType { Damage, Heal, Buff, DPS ,Counter}

[CreateAssetMenu(menuName = "Skill/Basic")]
public class BasicSkill : Skill
{
    [System.Serializable]
    public class BasicAction
    {
        public enum Target { Self, Target }
        
        [System.Serializable]
        public class ActionModifier
        {
            public StatType statType;
            public MainStatType mainType;
            public SubStatType subType;
            public float amount;

        }


        public Target target = Target.Target;
        public SkillType skillType;
        public float value;

        public DamageType damageType;
        public CriticalType critical;
             

        public List<ActionModifier> actionModifiers = new List<ActionModifier>();

        public SubStatType statToBuff;
        public ModifierType buffModType;
        public int turnDaration;

        public DDType dType;
        public int penetrate;

        public bool isDrain;
        [Range(0,100)]
        public int drainPercent;
    }
    public bool isUsePoint = true;
    public List<BasicAction> actions = new List<BasicAction>();



    public override bool Use<U, T>(U user, T enermy, ArenaType arena = ArenaType.Hunting)
    {
        Activate(user,enermy,arena);
        return isUsePoint;
    }

    public override void Activate<U,T>(U user, T enermy, ArenaType arena = ArenaType.Hunting)
    {
        ActiveSkill(user, enermy, arena);
    }

    public virtual void ActiveSkill<U, T>(U userStat, T enermyStat, ArenaType arena)
    {
        Debug.Log(skillName);
        for (int i = 0; i < actions.Count; i++)
        {
            Status user = new Status();

            if (userStat is Character)
                user = (userStat as Character).status;
            else if (userStat is Monster)
                user = (userStat as Monster).status;

            Status target = new Status();



            if (actions[i].target == BasicAction.Target.Target)
            {
                if (enermyStat is Character)
                    target = (enermyStat as Character).status;
                else if (enermyStat is Monster)
                    target = (enermyStat as Monster).status;
            }
            else if (actions[i].target == BasicAction.Target.Self)
            {
                if (userStat is Character)
                    target = (userStat as Character).status;
                else if (userStat is Monster)
                    target = (userStat as Monster).status;
            }

            if (actions[i].skillType == SkillType.Damage)
            {
                DamageAction(user, target, i);
            }
            else if (actions[i].skillType == SkillType.Heal)
            {
                HealAction(user, target, i);
            }
            else if (actions[i].skillType == SkillType.Buff)
            {
                BuffAction(user, target, i);
            }
            else if (actions[i].skillType == SkillType.DPS)
            {

                if (actions[i].dType != DDType.Stun)
                {
                    if (arena == ArenaType.Hunting)
                    {
                        if (userStat is Character)
                            DpsAction(user, HuntingManager.instance.monsterDDPS, i);
                        else if (userStat is Monster)
                            DpsAction(user, HuntingManager.instance.characterDDPS, i);
                    }

                }
                else if (actions[i].dType == DDType.Stun)
                {
                    if (arena == ArenaType.Hunting)
                    {
                        if (userStat is Character)
                        {
                            if ((int)actions[i].turnDaration > HuntingManager.instance.monsterStun)
                                HuntingManager.instance.monsterStun = (int)actions[i].turnDaration;
                        }
                        else if (userStat is Monster)
                            if ((int)actions[i].turnDaration > HuntingManager.instance.characterStun)
                                HuntingManager.instance.characterStun = (int)actions[i].turnDaration;
                    }
                }
            }

        }
    }
    void DamageAction(Status user, Status target, int i)
    {
        float damage = actions[i].value;
        for (int m = 0; m < actions[i].actionModifiers.Count; m++)
        {
            if (actions[i].actionModifiers[m].statType == StatType.Main)
            {
                var stat = (user.GetType().GetField(actions[i].actionModifiers[m].mainType.ToString()).GetValue(user));
                var value = stat.GetType().GetProperty("Value").GetValue(stat);
                damage += (float)value * actions[i].actionModifiers[m].amount;
            }
            else if (actions[i].actionModifiers[m].statType == StatType.Sub)
            {
                var stat = (user.GetType().GetField(actions[i].actionModifiers[m].subType.ToString()).GetValue(user));
                var value = stat.GetType().GetProperty("Value").GetValue(stat);
                damage += (float)value * actions[i].actionModifiers[m].amount;
            }

        }

        float finalDamage = Formula.DamageFormula(user, target, actions[i].damageType, true, damage);
        if (Formula.CriticalFormula(user, target, ref finalDamage, actions[i].critical)) 
        {
            Debug.Log("Critical");
        }

        if(target.GetDamage(ref finalDamage,user))
        {
            Debug.Log($"Deal {finalDamage} {actions[i].damageType} Damage");
            if (actions[i].isDrain)
            {
                float drain = finalDamage * actions[i].drainPercent / 100;
                user.currentHP += drain;
                Debug.Log($"Drain {drain}");
            }
        }
        
        if (user.currentHP > user.HP.Value)
            user.currentHP = user.HP.Value;
        

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
        if (target.currentHP + heal > target.HP.Value)
        {
            heal = target.HP.Value - target.currentHP;
            target.currentHP = target.HP.Value;
        }
        else
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
                buffValue += (float)value;
            }
            else if (actions[i].actionModifiers[m].statType == StatType.Sub)
            {
                var stat = (user.GetType().GetField(actions[i].actionModifiers[m].subType.ToString()).GetValue(user));
                var value = stat.GetType().GetProperty("Value").GetValue(stat);
                buffValue += (float)value;
            }

        }

        Stat statToBuff = (Stat)(target.GetType().GetField(actions[i].statToBuff.ToString()).GetValue(target));
        float oldValue = statToBuff.Value;
        statToBuff.AddModifier(new Modifier(buffValue, this, actions[i].buffModType, Modifier.ModifierTime.Turn, actions[i].turnDaration));
        string debug = "";
        string pOrM = "+";
        if (buffValue < 0)
            pOrM = "";


        if (target == user)
            debug += ("User " + actions[i].statToBuff.ToString() + " " + pOrM + " " + buffValue.ToString());
        else
            debug += ("Target " + actions[i].statToBuff.ToString() + " " + pOrM + " " + buffValue.ToString());

        if (actions[i].buffModType == ModifierType.Pecentage)
            debug += "%";
        Debug.Log(debug);
        Debug.Log($"{actions[i].statToBuff}: {oldValue} -> {statToBuff.Value}");
    }
    void DpsAction(Status user, List<DebuffDamage> targetDDPS, int i)
    {

        float dpsValue = actions[i].value;
        for (int m = 0; m < actions[i].actionModifiers.Count; m++)
        {
            if (actions[i].actionModifiers[m].statType == StatType.Main)
            {
                var stat = (user.GetType().GetField(actions[i].actionModifiers[m].mainType.ToString()).GetValue(user));
                var value = stat.GetType().GetProperty("Value").GetValue(stat);
                Debug.Log(actions[i].actionModifiers[m].mainType + " " + value);
                dpsValue += (float)value * actions[i].actionModifiers[m].amount;
            }
            else if (actions[i].actionModifiers[m].statType == StatType.Sub)
            {
                var stat = (user.GetType().GetField(actions[i].actionModifiers[m].subType.ToString()).GetValue(user));
                var value = stat.GetType().GetProperty("Value").GetValue(stat);
                Debug.Log(actions[i].actionModifiers[m].subType + " " + value);
                dpsValue += (float)value * actions[i].actionModifiers[m].amount;
            }

        }

        
        DebuffDamage debuffDamage = new DebuffDamage(this, "...", user, actions[i].dType, actions[i].damageType, dpsValue, actions[i].turnDaration, actions[i].penetrate);
        targetDDPS.Add(debuffDamage);
        Debug.Log("Deal DPS" + debuffDamage + " " + actions[i].turnDaration + " Turn");
    }

}
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(BasicSkill.BasicAction))]
public class BasicActionDrawer : PropertyDrawer
{

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        position.height = 16;
        EditorGUI.PropertyField(position, property) ;
        if (property.isExpanded)
        {
            position.y += 20;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("skillType"));
            position.y += 20;

            EditorGUI.PropertyField(position, property.FindPropertyRelative("target"));
            position.y += 20;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("value"));
            if (property.FindPropertyRelative("skillType").enumValueIndex == 0)
            {

                position.y += 20;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("damageType"));
                position.y += 20;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("critical"));
                position.y += 20;
                Rect percentDrain = position;
                percentDrain.width /= 2;
                EditorGUI.PropertyField(percentDrain, property.FindPropertyRelative("isDrain"));
                percentDrain.x += percentDrain.width;
                EditorGUI.PropertyField(percentDrain, property.FindPropertyRelative("drainPercent"), GUIContent.none);
                position.y += 20;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("actionModifiers"), true);
            }
            else if (property.FindPropertyRelative("skillType").enumValueIndex == 1)
            {
                position.y += 20;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("actionModifiers"), true);
            }
            else if (property.FindPropertyRelative("skillType").enumValueIndex == 2)
            {
                position.y += 20;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("statToBuff"));
                position.y += 20;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("buffModType"));
                position.y += 20;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("turnDaration"));
            }
            else if (property.FindPropertyRelative("skillType").enumValueIndex == 3)
            {
                position.y += 20;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("damageType"));
                position.y += 20;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("critical"));
                position.y += 20;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("dType"));
                position.y += 20;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("penetrate"));
                position.y += 20;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("turnDaration"));
                position.y += 20;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("actionModifiers"), true);
            }
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int x = 0;
        int y = 0;
        if (property.isExpanded)
        {
            x = property.FindPropertyRelative("actionModifiers").arraySize * 20;
            y = 0; ;
            switch (property.FindPropertyRelative("skillType").enumValueIndex)
            {
                case 0:
                    y = 160;
                    break;
                case 1:
                    y = 100;
                    break;
                case 2:
                    y = 130;
                    break;
                case 3:
                    y = 200;
                    break;
            }
        }
        return EditorGUIUtility.singleLineHeight + y + x;
        //return base.GetPropertyHeight(property, label);
        //return EditorGUI.GetPropertyHeight(property, label,true);
    }
}


[CustomPropertyDrawer(typeof(BasicSkill.BasicAction.ActionModifier))]
public class BasicActionModifierDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        position.height = 16;
        position.width /= 3;
        EditorGUI.PropertyField(position, property.FindPropertyRelative("statType"), GUIContent.none);
        var statTyper = property.FindPropertyRelative("statType");
        position.x += position.width - 5;
        if (statTyper.enumValueIndex == 0)
        {
            EditorGUI.PropertyField(position, property.FindPropertyRelative("mainType"),GUIContent.none);
        }else if(statTyper.enumValueIndex == 1)         
        {
            EditorGUI.PropertyField(position, property.FindPropertyRelative("subType"), GUIContent.none);
        }
        position.x += position.width - 5;
        EditorGUI.PropertyField(position, property.FindPropertyRelative("amount"), GUIContent.none);
    }


    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }
}
#endif