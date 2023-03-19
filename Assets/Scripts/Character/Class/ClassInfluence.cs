using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Class/ClassInfluence")]
public class ClassInfluence : ScriptableObject
{
    public string className;

    public StatusUpgrade.SubModify[] strSub;
    public StatusUpgrade.SubModify[] dexSub;
    public StatusUpgrade.SubModify[] conSub;
    public StatusUpgrade.SubModify[] agiSub;
    public StatusUpgrade.SubModify[] intSub;

    public List<StatusUpgrade.SubModify[]> subModifies
    {
        get { return new List<StatusUpgrade.SubModify[]> { strSub, dexSub, agiSub, intSub ,conSub}; }
    }
        
}
