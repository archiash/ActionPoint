using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Skillbook", fileName = "New Skillbook")]
public class Skillbook : Item
{
    [SerializeField] Skill skill;

    public override bool UseItem(Status status = null)
    {
       return Character.instance.LearnSkill(skill);
    }

    public override string GetDesc(bool fulldesc = true)
    {
        return $"Use to learn Skill {skill.skillName}";
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Skillbook))]
public class SkillbookEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Skillbook t = (Skillbook)target;
        DrawDefaultInspector();
        if (GUILayout.Button("Get Item"))
        {
            Inventory.instance.GetItem(t);
        }
    }
}
#endif
