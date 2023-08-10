using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Runtime.Serialization;
using static Modifier;

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
        public ModifierTime modifierTime;
        public int turnDaration;

        public DDType dType;
        public int penetrate;

        public bool isDrain;
        [Range(0,100)]
        public int drainPercent;
    }

    public bool isUsePoint = true;
    [SerializeField] protected List<BasicAction> actions = new List<BasicAction>();

    public override bool Use<U, T>(U user, T enermy, ArenaType arena = ArenaType.Hunting)
    {
        Activate(user, enermy, arena);
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
            else if (userStat is Follower)
            {
                Follower follower = userStat as Follower;
                Debug.Log(follower.followerName);
                Debug.Log(user);
                user.SetFollowerMainStat(follower.followerStatus.MainStatArray(follower.followerLevel));
            }

            Status target = (enermyStat is Status) ? enermyStat as Status : new Status();

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
                BasicAction action = actions[i];
                DamageAction(user, target, action);
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
                            DpsAction(user, UIManager.Instance.huntingManager.monsterDDPS, i);
                        else if (userStat is Monster)
                            DpsAction(user, UIManager.Instance.huntingManager.characterDDPS, i);
                    }else if (arena == ArenaType.OffensiveTest)
                    {
                        DpsAction(user, OffensiveTest.instance.testDDPS, i);
                    }else if (arena == ArenaType.DefensiveTest)
                    {
                        DpsAction(user, DefensiveTest.instance.testDDPS, i);
                    }

                }
                else if (actions[i].dType == DDType.Stun)
                {
                    if (arena == ArenaType.Hunting)
                    {
                        if (userStat is Character)
                        {
                            if (actions[i].turnDaration > UIManager.Instance.huntingManager.monsterStun)
                                UIManager.Instance.huntingManager.monsterStun = actions[i].turnDaration;
                        }
                        else if (userStat is Monster)
                            if (actions[i].turnDaration > UIManager.Instance.huntingManager.characterStun)
                                UIManager.Instance.huntingManager.characterStun = actions[i].turnDaration;
                    }else if(arena == ArenaType.DefensiveTest)
                    {
                        if(actions[i].turnDaration > DefensiveTest.instance.testerStun)
                        {
                            DefensiveTest.instance.testerStun = actions[i].turnDaration;
                        }
                    }
                }
            }

        }
    }
    public void DamageAction(Status user, Status target, BasicAction action)
    {
        float damage = action.value;
        for (int m = 0; m < action.actionModifiers.Count; m++)
        {
            if (action.actionModifiers[m].statType == StatType.Main)
            {
                var stat = (user.GetType().GetField(action.actionModifiers[m].mainType.ToString()).GetValue(user));
                var value = stat.GetType().GetProperty("Value").GetValue(stat);
                damage += (float)value * action.actionModifiers[m].amount;
            }
            else if (action.actionModifiers[m].statType == StatType.Sub)
            {
                var stat = (user.GetType().GetField(action.actionModifiers[m].subType.ToString()).GetValue(user));
                var value = stat.GetType().GetProperty("Value").GetValue(stat);
                damage += (float)value * action.actionModifiers[m].amount;
            }

        }

        float finalDamage = Formula.DamageFormula(user, target, action.damageType, true, damage);
        if (Formula.CriticalFormula(user, target, ref finalDamage, action.critical)) 
        {
            Debug.Log("Critical");
        }
        AttackData attackData = new AttackData(user, finalDamage);
        if(target.GetDamage(ref attackData))
        {
            Debug.Log($"Deal {finalDamage} {action.damageType} Damage");
            if (action.isDrain)
            {
                float drain = finalDamage * action.drainPercent / 100;
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
        statToBuff.AddModifier(new Modifier(buffValue, this, actions[i].buffModType, actions[i].modifierTime, actions[i].turnDaration));
        string debug = "";
        string pOrM = "+";
        if (buffValue < 0)
            pOrM = "";


        if (target == user)
            debug += ("User " + actions[i].statToBuff.ToString() + " " + pOrM + " " + buffValue.ToString());
        else
            debug += ("Target " + actions[i].statToBuff.ToString() + " " + pOrM + " " + buffValue.ToString());

        if (actions[i].buffModType == ModifierType.Percentage)
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
                var stat = user.GetType().GetField(actions[i].actionModifiers[m].mainType.ToString()).GetValue(user);
                var value = stat.GetType().GetProperty("Value").GetValue(stat);
                Debug.Log(actions[i].actionModifiers[m].mainType + " " + value);
                dpsValue += (float)value * actions[i].actionModifiers[m].amount;
            }
            else if (actions[i].actionModifiers[m].statType == StatType.Sub)
            {
                var stat = user.GetType().GetField(actions[i].actionModifiers[m].subType.ToString()).GetValue(user);
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
        var obj = property.serializedObject.targetObject;
        if (!(obj as nHitSkill))
        {
            position.height = EditorGUIUtility.singleLineHeight;
            position.y += position.height;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("skillType"));
            position.y += position.height;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("target"));
            position.y += position.height;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("value"));
            if (property.FindPropertyRelative("skillType").enumValueIndex == 0)
            {

                position.y += position.height;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("damageType"));
                position.y += position.height;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("critical"));
                position.y += position.height;
                Rect percentDrain = position;
                percentDrain.width /= 2;
                EditorGUI.PropertyField(percentDrain, property.FindPropertyRelative("isDrain"));
                percentDrain.x += percentDrain.width;
                EditorGUI.PropertyField(percentDrain, property.FindPropertyRelative("drainPercent"), GUIContent.none);
                position.y += position.height;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("actionModifiers"), true);
            }
            else if (property.FindPropertyRelative("skillType").enumValueIndex == 1)
            {
                position.y += position.height;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("actionModifiers"), true);
            }
            else if (property.FindPropertyRelative("skillType").enumValueIndex == 2)
            {
                position.y += position.height;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("statToBuff"));
                position.y += position.height;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("buffModType"));
                position.y += position.height;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("modifierTime"));
                position.y += position.height;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("turnDaration"));
            }
            else if (property.FindPropertyRelative("skillType").enumValueIndex == 3)
            {
                position.y += position.height;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("damageType"));
                position.y += position.height;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("critical"));
                position.y += position.height;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("dType"));
                position.y += position.height;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("penetrate"));
                position.y += position.height;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("turnDaration"));
                position.y += position.height;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("actionModifiers"), true);
            }
            return;
        }
        EditorGUILayout.LabelField("Damage Type");
        EditorGUILayout.PropertyField(property.FindPropertyRelative("damageType"), GUIContent.none);     
        EditorGUILayout.LabelField("Critical Change Type");
        EditorGUILayout.PropertyField(property.FindPropertyRelative("critical"), GUIContent.none);
        EditorGUILayout.PropertyField(property.FindPropertyRelative("isDrain"), new GUIContent("Drain"));
        EditorGUILayout.PropertyField(property.FindPropertyRelative("drainPercent"), GUIContent.none);
        EditorGUILayout.LabelField("Value");
        EditorGUILayout.PropertyField(property.FindPropertyRelative("value"), GUIContent.none);
        EditorGUILayout.PropertyField(property.FindPropertyRelative("actionModifiers"), new GUIContent("Modifier"));
        
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float x = 0;
        int y = 0;
        var obj = property.serializedObject.targetObject;
        if (!(obj as nHitSkill))
        {


            y = 0;
            switch (property.FindPropertyRelative("skillType").enumValueIndex)
            {
                case 0:
                    y = 7;
                    x = EditorGUI.GetPropertyHeight(property.FindPropertyRelative("actionModifiers"), true);
                    break;
                case 1:
                    y = 4;
                    x = EditorGUI.GetPropertyHeight(property.FindPropertyRelative("actionModifiers"), true);
                    break;
                case 2:
                    y = 8;
                    break;
                case 3:
                    y = 9;
                    x = EditorGUI.GetPropertyHeight(property.FindPropertyRelative("actionModifiers"), true);
                    break;
            }            
        }

        return EditorGUIUtility.singleLineHeight * y + x;
        //return base.GetPropertyHeight(property, label);
        //return EditorGUI.GetPropertyHeight(property, label,true);
    }
}

[CustomPropertyDrawer(typeof(BasicSkill.BasicAction.ActionModifier))]
public class BasicActionModifierDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        position.height = EditorGUIUtility.singleLineHeight;
        position.width /= 3;
        EditorGUI.PropertyField(position, property.FindPropertyRelative("statType"), GUIContent.none);
        var statTyper = property.FindPropertyRelative("statType");
        position.x += position.width;
        if (statTyper.enumValueIndex == 0)
        {
            EditorGUI.PropertyField(position, property.FindPropertyRelative("mainType"),GUIContent.none);
        }else if(statTyper.enumValueIndex == 1)         
        {
            EditorGUI.PropertyField(position, property.FindPropertyRelative("subType"), GUIContent.none);
        }
        position.x += position.width;
        EditorGUI.PropertyField(position, property.FindPropertyRelative("amount"), GUIContent.none);
    }


    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }
}
#endif