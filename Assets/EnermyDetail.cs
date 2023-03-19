using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class EnermyDetail : MonoBehaviour
{
    Monster monster;

    [SerializeField] Image monsterImage;
    [SerializeField] TextMeshProUGUI monsterName;
    [SerializeField] TextMeshProUGUI monsterSubName;
    [SerializeField] TextMeshProUGUI monsterStatus;
    [SerializeField] TextMeshProUGUI monsterSkillDesc;
    [SerializeField] Transform enermyDetailPanel;
    public void ShowEnermyDetail(Monster monster)
    {
        this.monster = monster;
        enermyDetailPanel.gameObject.SetActive(true);
        monsterImage.sprite = monster.sprite;
        monsterName.text = monster.name;
        monsterSubName.text = monster.Desc;
        monsterStatus.text = monster.GetStatus();
        monsterSkillDesc.text = monster.GetSkillList();
    }

    public void FightEnermy()
    {
        Monster monster = Instantiate(this.monster);
        UIManager.Instance.huntingManager.Setup(monster, FollowerTeam.instance.followerTeam);
        UIManager.Instance.huntingManager.StartBattle();
    }
}
