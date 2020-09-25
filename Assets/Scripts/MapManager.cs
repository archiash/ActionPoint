using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    public List<Map> maps = new List<Map>();
    public Transform parent;

    [SerializeField] MonsterBar monsterBarPrefab;

    [SerializeField] TMP_Dropdown mapDropdown;
    [SerializeField] TMP_Dropdown difficultDropdown;

    private void Start()
    {
        instance = this;
        MapFromDropdown();
    }

    public void MapFromDropdown()
    {
        CreateMonsterList(maps[mapDropdown.value]);
    }

    public void CreateMonsterList(Map map)
    {
        foreach (Transform i in parent)
        {
            Destroy(i.gameObject);
        }
        map.monsters = map.monsters.OrderBy(x => x.usePoint).ToList();
        foreach (Monster monster in map.monsters)
        {
            MonsterBar monsterBar = Instantiate(monsterBarPrefab, parent);
            monsterBar.Create(monster);
        }
    }
}
