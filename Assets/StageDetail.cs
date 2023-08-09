using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageDetail : MonoBehaviour
{

    public GameObject dialogueBox;

    public static StageDetail instance;

    [SerializeField] Image enermyImage;

    [SerializeField] TextMeshProUGUI enermyName;
    [HideInInspector] public ScatteredTimeStage stage;

    [SerializeField] Button skillMenuButton;
    [SerializeField] Button statusMenuButton;

    [SerializeField] StatusGraph statusGraph;

    [SerializeField] GameObject skillDetailArea;
    [SerializeField] GameObject statusDetailArea;
    [SerializeField] SkillDetailBar[] skillList;

    Monster enermy;

    [SerializeField] Transform scatteredTimePanel;

    public void ShowStageDetail(ScatteredTimeStage stage)
    {
        gameObject.SetActive(true);
        dialogueBox.SetActive(true);
        this.stage = stage;

        enermyImage.enabled = true;
        enermyImage.sprite = stage.stageEnermy.sprite;

        enermyName.text = stage.stageName;

        skillMenuButton.interactable = false;
        skillDetailArea.SetActive(true);
        statusMenuButton.interactable = true;
        statusDetailArea.SetActive(false);

        enermy = stage.stageEnermy;
        
        for (int i = 0; i < 4; i++)
        {
            if(i < enermy.currentSkill.Count)
            {
                skillList[i].ShowSkillDetail(enermy.currentSkill[i]);
                continue;
            }

            skillList[i].ShowSkillDetail(null);
        }
    }

    public void WinStage()
    {
        if (stage != null)
        {
            FollowerTeam.instance.followerList.Add(stage.stageFollower.GetCopy());
        }
    }

    public void ShowStageDetail(Monster monster)
    {
        stage = null;
        gameObject.SetActive(true);
        dialogueBox.SetActive(false);
        enermy = monster;

        for (int i = 0; i < 4; i++)
        {
            if (i < enermy.currentSkill.Count)
            {
                skillList[i].ShowSkillDetail(enermy.currentSkill[i]);
                continue;
            }

            skillList[i].ShowSkillDetail(null);
        }

        
        enermyImage.enabled = true;
        enermyImage.sprite = monster.sprite;
        enermyName.text = monster.Name;

        skillDetailArea.SetActive(true);
        statusDetailArea.SetActive(false);
        skillMenuButton.interactable = false;
        statusMenuButton.interactable = enermy.currentSkill.Count != 0;

        if (!statusMenuButton.interactable)
        {
            skillDetailArea.SetActive(false);
            statusDetailArea.SetActive(true);
            OpenStatus();
        }

        GetComponent<Animator>().SetTrigger("NM");
    }

    public void OpenStatus()
    {
        statusGraph.UpdateGraph(Character.instance.status, enermy.status);
    }


    public void OpenBattleScene()
    {
        UIManager.Instance.huntingManager.gameObject.SetActive(true);
        UIManager.Instance.huntingManager.Setup(enermy, FollowerTeam.instance.followerTeam);      
    }

    public void StartBattleScene()
    {
        scatteredTimePanel.gameObject.SetActive(false);
        enermyImage.enabled = false;
        UIManager.Instance.huntingManager.winEvent = WinStage;
        UIManager.Instance.huntingManager.StartBattle();
        GetComponent<Animator>().SetTrigger("EndBattle");
        
        gameObject.SetActive(false);
    }
}
