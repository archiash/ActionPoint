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
    [SerializeField] TMP_Dropdown difficultDropdown;

    private void Start()
    {
        instance = this;
        OnMapChange();
    }

    public void MapFromDropdown()
    {
        CreateMonsterList(maps[mapDropdown.value].maps[difficultDropdown.value]);    
    }

    public void OnMapChange()
    {
        if (difficultDropdown.value > maps[mapDropdown.value].maps.Count - 1)
        {
            difficultDropdown.value = 0;
            difficultDropdown.RefreshShownValue();
        }

        difficultDropdown.ClearOptions();
        List<string> m_DropdownOption = new List<string>();
        foreach (Map map in maps[mapDropdown.value].maps)
        {
            m_DropdownOption.Add(map.difficult);
        }
        difficultDropdown.AddOptions(m_DropdownOption);

        MapFromDropdown();
    }

    public void OnDifficultChange()
    {
        MapFromDropdown();
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
