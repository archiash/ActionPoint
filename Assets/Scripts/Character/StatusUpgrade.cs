using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StatusUpgrade : MonoBehaviour
{
  public SubModify[] strSub;
    public SubModify[] dexSub;
    public SubModify[] conSub;
    public SubModify[] agiSub;
    public SubModify[] intSub;

    Character character;

    public float scale;

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
            foreach(SubModify mod in strSub)
            {


                Stat a = (Stat)(character.status.GetType().GetField(mod.subType.ToString()).GetValue(character.status));

                foreach(Stat.MainModifier mainModifier in a.mainModifiers)
                {
                    if (mainModifier.type == MainStatType.STR)
                        mainModifier.modifier = mod.value * (1 + strLevel / scale);
                }
            }
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
            foreach (SubModify mod in dexSub)
            {

                Stat a = (Stat)(character.status.GetType().GetField(mod.subType.ToString()).GetValue(character.status));

                foreach (Stat.MainModifier mainModifier in a.mainModifiers)
                {
                    if (mainModifier.type == MainStatType.DEX)
                        mainModifier.modifier = mod.value * (1 + dexLevel / scale);
                }
            }
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
            foreach (SubModify mod in conSub)
            {
                Stat a = (Stat)(character.status.GetType().GetField(mod.subType.ToString()).GetValue(character.status));

                foreach (Stat.MainModifier mainModifier in a.mainModifiers)
                {
                    if (mainModifier.type == MainStatType.CON)
                        mainModifier.modifier = mod.value * (1 + conLevel / scale);
                }
            }
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
            foreach (SubModify mod in agiSub)
            {
                Stat a = (Stat)(character.status.GetType().GetField(mod.subType.ToString()).GetValue(character.status));

                foreach (Stat.MainModifier mainModifier in a.mainModifiers)
                {
                    if (mainModifier.type == MainStatType.AGI)
                        mainModifier.modifier = mod.value * (1 + agiLevel / scale);
                }
            }
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
            foreach (SubModify mod in intSub)
            {
                Stat a = (Stat)(character.status.GetType().GetField(mod.subType.ToString()).GetValue(character.status));

                foreach (Stat.MainModifier mainModifier in a.mainModifiers)
                {
                    if (mainModifier.type == MainStatType.INT)
                        mainModifier.modifier = mod.value * (1 + intLevel / scale);
                }
            }
        }
    }

    [SerializeField] int strLevel;
    [SerializeField] int dexLevel;
    [SerializeField] int conLevel;
    [SerializeField] int agiLevel;
    [SerializeField] int intLevel;

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
