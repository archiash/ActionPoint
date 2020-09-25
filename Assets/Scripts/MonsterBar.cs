using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBar : MonoBehaviour
{
    Monster monster;
    [SerializeField] Image monsterImage;
    [SerializeField] TextMeshProUGUI monsterName;
    [SerializeField] TextMeshProUGUI monsterSubName;
    [SerializeField] TextMeshProUGUI monsterUsePoint;

    public void Create(Monster _monster)
    {
        monster = _monster;
        monsterImage.sprite = monster.sprite;
        monsterName.text = monster.Name;
        monsterSubName.text = monster.Desc;
        monsterUsePoint.text = monster.usePoint.ToString() + " Point";
    }

    public void ShowDetail()
    {
        UI_MonstarDetailPanel.insctance.ShowDetail(monster);
    }
}
