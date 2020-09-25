using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SkillSystem
{
    public class Skill : ScriptableObject
    {
        public string SkillName;
        public string Description;

        public virtual void LearnSkill()
        {

        }
    }

#if UNITY_EDITOR
    public class SkillEditor
    {
        SerializedObject m_Target;

        SerializedProperty m_NameProperty;
        SerializedProperty m_DescriptionProperty;

        public void Init(SerializedObject target)
        {
            m_Target = target;

            m_NameProperty = m_Target.FindProperty(nameof(Skill.SkillName));
            m_DescriptionProperty = m_Target.FindProperty(nameof(Skill.Description));
        }

        public void GUI()
        {
            EditorGUILayout.PropertyField(m_NameProperty);
            EditorGUILayout.PropertyField(m_DescriptionProperty, GUILayout.MinHeight(128));
        }
    }
#endif

}
