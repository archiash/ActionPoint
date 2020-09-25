using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using Ludiq;
using System;
using System.Collections;
using System.Collections.Generic;
using Bolt;
using UnityEngine.Android;

public class Gamemanager : MonoBehaviour
{
    [SerializeField]ItemSaveManager itemSaveManager;

    public static Gamemanager instance;

    public List<Camera> cameras;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
       
    }

    public void Start()
    {
        Load();
        ChangeToScene(0);

    }

   void Update()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            if(Input.GetKey(KeyCode.Home) || Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Menu))
            {
                Save();
                Application.Quit();
            }
        }
    }

    public void ChangeToScene(int x)
    {
        for (int i = 0;i < cameras.Count; i++)
        {
            if (i == x)
            {
                cameras[i].gameObject.SetActive(true);

            }
            else
                cameras[i].gameObject.SetActive(false);
        }

        if (x == 1)
            UI_Inventory.instance.UpdateSlot();
    }

    void OnApplicationQuit()
    {
        Save();
    }

    private void OnApplicationPause(bool isPause)
    {
        if (isPause)
        {
            Save();
        }
    }

    public  void Load()
    {
        itemSaveManager.LoadEquipment();
        itemSaveManager.LoadCharacterData();        
        itemSaveManager.LoadPoint();
        itemSaveManager.LoadInventory();
        
    }

    public void Save()
    {
        itemSaveManager.SaveEquipment();
        itemSaveManager.SaveInventory();
        itemSaveManager.SavePoint();
        itemSaveManager.SaveCharacterData();
    }


}
#if UNITY_EDITOR
[CustomEditor(typeof(Gamemanager))]
public class GamemangerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Gamemanager t = (Gamemanager)target;
        base.OnInspectorGUI();
        if (Application.isPlaying)
        {
            if(GUILayout.Button("Save"))
            {
                t.Save();
            }

            if(GUILayout.Button("Load"))
            {
                t.Load();
            }
        }
            

    }
}
#endif