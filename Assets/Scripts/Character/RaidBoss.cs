using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Create/Monster/RaidBoss", fileName = "New RaidBoss")]
public class RaidBoss : Monster
{
    [SerializeField] string id;
    public string ID { get { return id; } }

    public List<StackItem> summonMaterial = new List<StackItem>();
    public List<StackItem> costPerRound = new List<StackItem>();

#if UNITY_EDITOR
    public void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        id = AssetDatabase.AssetPathToGUID(path);
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(RaidBoss))]
public class RaidBossEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Monster t = (Monster)target;
        GUILayout.Label(new GUIContent($"Offensive Power: {t.offensivePower}"));
        GUILayout.Label(new GUIContent($"Defensive Power: {t.defensivePower}"));
        base.OnInspectorGUI();
        
    }
}
#endif
