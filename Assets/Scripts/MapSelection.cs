using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelection : MonoBehaviour
{
    public List<Monster> monsters = new List<Monster>();

    public void OnSelcetMap()
    {

    }

    public void AddHuntingToMap(Monster newMonster)
    {
        foreach (Monster monster in monsters)
        {
            if (newMonster == monster)
                return;
        }
        monsters.Add(newMonster);
    }
}
