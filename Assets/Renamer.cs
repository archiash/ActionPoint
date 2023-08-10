#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class ObjectRenamer : EditorWindow
{
    [MenuItem("Window/ObjectRenamer")]

    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ObjectRenamer));
    }

    public GameObject[] textObjectToRename;
    public string subfix;

    void OnGUI()
    {
        ScriptableObject scriptableObj = this;
        SerializedObject serialObj = new SerializedObject(scriptableObj);
        SerializedProperty serialProp = serialObj.FindProperty("textObjectToRename");
        subfix = EditorGUILayout.TextField("", subfix);

        EditorGUILayout.PropertyField(serialProp, true);
        serialObj.ApplyModifiedProperties();

        if (GUILayout.Button("Rename"))
        {
            for(int i = 0; i < textObjectToRename.Length; i++)
            {
                textObjectToRename[i].name = textObjectToRename[i].GetComponent<TextMeshProUGUI>().text + " " + subfix;
            }
        }
    }
}
#endif