using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class SaveData : MonoBehaviour
{
    public static SaveData instance;
    public UpgradeLevel upgradeData = new UpgradeLevel();

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    public void SaveUpgradeData()
    {
        string jsonSavePath = Application.persistentDataPath + "/upgradedata.json";
        string save = JsonUtility.ToJson(upgradeData);
        File.WriteAllText(jsonSavePath, save);
    }

    public void LoadUpgradeData()
    {
        if (File.Exists(Application.persistentDataPath
                   + "/upgradedata.json"))
        {
            upgradeData = JsonUtility.FromJson<UpgradeLevel>(File.ReadAllText(Application.persistentDataPath + "/upgradedata.json"));
        }

    }

    public void ResetData()
    {
        if (File.Exists(Application.persistentDataPath
                      + "/upgradedata.json"))
        {
            File.Delete(Application.persistentDataPath
                              + "/upgradedata.json");
            Debug.Log("Data reset complete!");
        }
        else
            Debug.LogError("No save data to delete.");
    }
}

[System.Serializable]
public class UpgradeLevel
{
    public int healthLevel;
    public int attackLevel;
    public int defenseLevel;
    public int speedLevel;
    public int regenLevel;
    public int pointLevel;
}

#if UNITY_EDITOR
[CustomEditor(typeof(SaveData))]
public class SaveDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SaveData save = (SaveData)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Reset"))
        {
            save.ResetData();
        }

    }
}
#endif

