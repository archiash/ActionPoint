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
#if UNITY_EDITOR
    public void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        id = AssetDatabase.AssetPathToGUID(path);
    }
#endif
}
