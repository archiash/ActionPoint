using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    public List<Maps> maps = new List<Maps>();
    public Transform parent;

    int currentMapIndex = 0;
    [SerializeField] MonsterBar monsterBarPrefab;

    [SerializeField] TMP_Dropdown mapDropdown;

    [SerializeField] MapZoneUI zonePrefab;

    [SerializeField] Button leftChangeButton;
    [SerializeField] Button rightChangeButton;

    [SerializeField] TextMeshProUGUI mapName;

    private void Start()
    {
        ChangeMap(0);
    }

    public void ChangeMapButton(int dDir)
    {
        ChangeMap(currentMapIndex + dDir);
    } 

    void ChangeMap(int index)
    {
        
        currentMapIndex = index;
        leftChangeButton.interactable = index != 0;
        rightChangeButton.interactable = index != maps.Count - 1;
        
        foreach (Transform i in parent)
        {
            Destroy(i.gameObject);
        }

        Maps map = maps[index];
        mapName.text = map.mapName;
        for (int i = 0; i < map.maps.Count; i++)
        {
            MapZoneUI zone = Instantiate(zonePrefab, parent);
            zone.Init(map.maps[i], i);

            map.maps[i].monsters = map.maps[i].monsters.OrderBy(x => x.usePoint).ToList();
            foreach (Monster monster in map.maps[i].monsters)
            {
                MonsterBar monsterBar = Instantiate(monsterBarPrefab, zone.transform);
                monsterBar.Create(monster);
            }
        }
    }

    void Map2DropDownList()
    {
        mapDropdown.ClearOptions();
        foreach(Maps i in maps)
        {
            mapDropdown.AddOptions(new List<string>() { i.mapName});
        }
    }

    public void MapFromDropdown()
    {
        foreach (Transform i in parent)
        {
            Destroy(i.gameObject);
        }

        Maps map = maps[mapDropdown.value];
        
        for(int i = 0; i < map.maps.Count;i++)
        {
            MapZoneUI zone = Instantiate(zonePrefab, parent);
            zone.Init(map.maps[i],i);

            map.maps[i].monsters = map.maps[i].monsters.OrderBy(x => x.usePoint).ToList();
            foreach(Monster monster in map.maps[i].monsters)
            {
                MonsterBar monsterBar = Instantiate(monsterBarPrefab, zone.transform);
                monsterBar.Create(monster);
            }
        }

    }

}
