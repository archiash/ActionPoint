using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    public List<Maps> maps = new List<Maps>();
    public Transform parent;

    [SerializeField] MonsterBar monsterBarPrefab;

    [SerializeField] TMP_Dropdown mapDropdown;

    [SerializeField] MapZoneUI zonePrefab;

    private void Start()
    {
        Map2DropDownList();
        instance = this;
        MapFromDropdown();
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
