using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UpstatData : ScriptableObject
{
    public List<StatData> statData = new List<StatData>();
}

[System.Serializable]
public class StatData
{
    public int requireLevel;
    public int amount;
}

