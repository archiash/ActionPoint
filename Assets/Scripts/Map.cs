using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Map",menuName = "Create/Map")]
public class Map : ScriptableObject
{
    public string mapName;    
    public int level;
    public List<Monster> monsters = new List<Monster>();
}
