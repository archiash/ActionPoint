using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapSelectBar : MonoBehaviour
{
    Map map;

    public TextMeshProUGUI mapName;
    public TextMeshProUGUI mapDesc;

    public void CreateBar(Map _map)
    {
        map = _map;
        mapName.text = map.mapName;
        mapDesc.text = "Recommended Level: " + map.level.ToString();
    }

    public void SelectMap()
    {
        MapManager.instance.CreateMonsterList(map);
    }
}
