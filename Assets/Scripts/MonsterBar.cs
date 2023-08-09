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
    //[SerializeField] TextMeshProUGUI monsterUsePoint;
    [SerializeField] TextMeshProUGUI monsterOffensivePower;
    [SerializeField] TextMeshProUGUI monsterDefensivePower;
    public void Create(Monster _monster)
    {
        monster = _monster;
        monsterImage.sprite = monster.sprite;
        monsterName.text = monster.Name;
        monsterSubName.text = monster.Desc;
        monsterOffensivePower.text = $"Offensive Power: {_monster.offensivePower}";
        monsterDefensivePower.text = $"Defensive Power: {_monster.defensivePower}";
        //monsterUsePoint.text = monster.usePoint.ToString() + " Point";
    }

    public void ShowDetail()
    {
        //    UIManager.Instance.enermyDetail.ShowEnermyDetail(monster);
        UIManager.Instance.stageDetail.ShowStageDetail(monster);
    }
}
