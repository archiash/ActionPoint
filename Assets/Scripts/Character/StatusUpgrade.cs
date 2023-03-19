using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StatusUpgrade : MonoBehaviour
{
    Character character;

    public static StatusUpgrade instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        character = Character.instance;
    }

    [System.Serializable]
    public class SubModify
    {
        public float value;
        public SubStatType subType;
    }

    public int STR
    {
        get { return strLevel; }
        set
        {
            if (character == null)
                character = Character.instance;
            strLevel = value;
            character.status.STR.baseValue = strLevel;
        }
    }

    public int DEX
    {
        get { return dexLevel; }
        set
        {
            if (character == null)
                character = Character.instance;
            dexLevel = value;
            character.status.DEX.baseValue = dexLevel;
        }
    }

    public int CON
    {
        get { return conLevel; }
        set
        {
            if (character == null)
                character = Character.instance;
            
            conLevel = value;
            character.status.CON.baseValue = conLevel;
        }
    }

    public int AGI
    {
        get { return agiLevel; }
        set
        {
            if (character == null)
                character = Character.instance;
            agiLevel = value;
            character.status.AGI.baseValue = agiLevel;
        }
    }

    public int INT
    {
        get { return intLevel; }
        set
        {
            if (character == null)
                character = Character.instance;
            intLevel = value;
            character.status.INT.baseValue = intLevel;
        }
    }

    [SerializeField] int strLevel;
    [SerializeField] int dexLevel;
    [SerializeField] int conLevel;
    [SerializeField] int agiLevel;
    [SerializeField] int intLevel;

    public void ResetPoint()
    {
        STR = 0;
        INT = 0;
        CON = 0;
        AGI = 0;
        DEX = 0;
        Character.instance.statusPoint = Character.instance.Level - 1;
    }

}
#if UNITY_EDITOR

[CustomEditor(typeof(StatusUpgrade))]
public class StatusUpgradeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        StatusUpgrade t = (StatusUpgrade)target;
        
        base.OnInspectorGUI();
        if(GUILayout.Button("Reset"))
        {
            t.STR = 0;
            t.INT = 0;
            t.CON = 0;
            t.AGI = 0;
            t.DEX = 0;
            Character.instance.statusPoint = Character.instance.Level - 1;
        }
    }


}
#endif
