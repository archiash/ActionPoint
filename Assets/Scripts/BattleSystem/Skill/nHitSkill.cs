using System;
using System.Reflection;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Skill/nHitSkill")]
public class nHitSkill : BasicSkill
{
    public string stackName;
    public int useStack;
    public int stackPerHit;

    public BasicAction.Target targetToCheckStack;

    public BasicAction normalAction;
    public BasicAction nHitAction;

    public override void Activate<U, T>(U user, T enermy, ArenaType arena = ArenaType.Hunting)
    {
        base.Activate(user, enermy, arena);
    }

    public override bool Use<U, T>(U user, T enermy, ArenaType arena = ArenaType.Hunting)
    {
        Type userType = user.GetType();
        FieldInfo userProp = userType.GetField("status");
        Status userStatus = (Status)userProp.GetValue(user);

        Type enermyType = enermy.GetType();
        FieldInfo enermyProp = enermyType.GetField("status");
        Status enermyStatus = (Status)enermyProp.GetValue(enermy);

        Status targetStackStatus = targetToCheckStack == BasicAction.Target.Self ? userStatus : enermyStatus;
        Stack targetStack = null;
        bool alreadyHaveStack = targetStackStatus.stacks.TryGetValue(stackName, out targetStack);
        if (alreadyHaveStack)
        {
            if(targetStack.currentStack >= useStack)
            {
                if (nHitAction.skillType == SkillType.Damage)
                {
                    Debug.Log("n Hit Effect!!");
                    DamageAction(userStatus, enermyStatus, nHitAction);
                    targetStackStatus.stacks.Remove(stackName);
                    return true;
                }
            }
        }
        Debug.Log("Normal Effect!");
        DamageAction(userStatus, enermyStatus, normalAction);
        if (alreadyHaveStack) targetStackStatus.stacks[stackName].currentStack++;
        else targetStackStatus.stacks.Add(stackName, new Stack(stackName, useStack, 1));
        return true;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(nHitSkill))]
public class nHitSkillEditor : Editor
{
    SerializedProperty skillNameProp;
    SerializedProperty skillDecsProp;
    SerializedProperty stackNameProp;
    SerializedProperty useStackProp;
    SerializedProperty stackPerHitProp;
    SerializedProperty stackFromTargerProp;

    SerializedProperty normalActionProp;
    SerializedProperty nHitActionProp;

    public void OnEnable()
    {        
        skillNameProp = serializedObject.FindProperty("skillName");
        skillDecsProp = serializedObject.FindProperty("skillDesc");
        stackNameProp = serializedObject.FindProperty("stackName");
        useStackProp = serializedObject.FindProperty("useStack");
        stackPerHitProp = serializedObject.FindProperty("stackPerHit");
        stackFromTargerProp = serializedObject.FindProperty("targetToCheckStack");
        normalActionProp = serializedObject.FindProperty("normalAction");
        nHitActionProp = serializedObject.FindProperty("nHitAction");
    }

    public override void OnInspectorGUI()
    {
        nHitSkill skill = (nHitSkill)target;
        EditorGUILayout.LabelField("Skill Name");
        EditorGUILayout.PropertyField(skillNameProp, GUIContent.none);

        EditorGUILayout.PropertyField(skillDecsProp, new GUIContent("Skill Description"));

        EditorGUILayout.LabelField("Stack Name");
        EditorGUILayout.PropertyField(stackNameProp, GUIContent.none);

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical();       
        EditorGUILayout.LabelField("Use Stack");
        EditorGUILayout.PropertyField(useStackProp, GUIContent.none);
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical();         
        EditorGUILayout.LabelField("Stack per Hit");
        EditorGUILayout.PropertyField(stackPerHitProp, GUIContent.none);
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();  
        EditorGUILayout.LabelField("Stack From Target");
        EditorGUILayout.PropertyField(stackFromTargerProp, GUIContent.none);

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Normal Action", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(normalActionProp, GUIContent.none);
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("n-Hit Action", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(nHitActionProp, GUIContent.none);
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }
}

#endif