﻿using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public interface IInitInstance
{
    void InitInstance();
}

public class Gamemanager : MonoBehaviour
{
    public ItemSaveManager itemSaveManager;

    public static Gamemanager instance;

    public List<Camera> cameras;

    bool isLoad;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        var scripts = gameObject.GetComponentsInChildren<IInitInstance>(true);

        foreach (var script in scripts)
            script.InitInstance();
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
        /*
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
        if (x == 2)
        {
            if (Enchantment.instance != null)
                Enchantment.instance.ResetSelect();
            if (EnchantmentTranfer.instance != null)
                EnchantmentTranfer.instance.ResetTranfer();
            
        }*/
     
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
        if (loadable)
        {
            itemSaveManager.LoadEquipment();
            itemSaveManager.LoadCharacterData();
            itemSaveManager.LoadPoint();
            itemSaveManager.LoadInventory();
            itemSaveManager.LoadFollwer();
            isLoad = true;
        }
        
    }

    [SerializeField] bool saveable;
    [SerializeField] bool loadable;
    public void Save()
    {
        if (isLoad && saveable)
        {
            itemSaveManager.SaveEquipment();
            itemSaveManager.SaveInventory();
            itemSaveManager.SavePoint();
            itemSaveManager.SaveCharacterData();
            itemSaveManager.SaveFollower();
        }
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