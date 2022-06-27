using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
public class StatUIBuilder : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI statName;
    [SerializeField] GameObject value_text;

    public void SetName()
    {
        statName.gameObject.name = $"{statName.text}_Text";
        gameObject.name = $"{statName.text}_Stat";
        value_text.gameObject.name = $"{statName.text}_Value";
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(StatUIBuilder))]
public class StatUIBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Update"))
        {
            StatUIBuilder t = target as StatUIBuilder;
            t.SetName();
        }

    }
}
#endif
