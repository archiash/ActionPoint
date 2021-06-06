using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Maps", menuName = "Create/Maps")]
public class Maps : ScriptableObject
{
    public string mapName;
    public List<Map> maps = new List<Map>();
}
