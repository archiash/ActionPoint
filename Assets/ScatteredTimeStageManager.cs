using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScatteredTimeStageManager : MonoBehaviour
{
    public Dictionary<string, int> stagesProgression;

    [SerializeField] Transform stagesContent;
    [SerializeField] Transform unlockScattered;
    [SerializeField] Button unlockButton;
    ScatteredTimeStage stageOnUnlockMenu;
    int defaultStageIndex;

    public void LoadSave(TimeStageSaveData saveData)
    {
        stagesProgression = new Dictionary<string, int>();
        if (saveData != null)
        {
            for (int i = 0; i < saveData.stagesID.Length; i++)
            {
                stagesProgression.Add(saveData.stagesID[i], saveData.stagesProgression[i]);
            }
        }

        foreach (Transform s in stagesContent)
        {
            ScatteredTimeStage stage = s.GetComponent<ScatteredTimeStage>();
            if (stage != null)
            {
                if (stagesProgression.ContainsKey(stage.stageID))
                {
                    stage.isStageUnlocked = stagesProgression[stage.stageID] > 0;
                }else
                {
                    stagesProgression.Add(stage.stageID, 0);
                }
            }
        }
    }

    public void CheckIsStageAvailable()
    {
        foreach (Transform s in stagesContent)
        {
            ScatteredTimeStage stage = s.GetComponent<ScatteredTimeStage>();
            if (stage != null)
            {
                if (stage.stageEnermy != null)
                {
                    stage.gameObject.SetActive(FollowerTeam.instance.GetFollowerInListByID(stage.stageFollower.followerID) == null);
                    if (!stage.isStageUnlocked)
                    {
                        stage.isStageUnlocked = stage.materials.Length == 0;
                        stagesProgression[stage.stageID] = stage.materials.Length == 0 ? 1 : 0;
                    }
                    stage.RefreshCard();
                }
            }
        }
    }

    public void CloseUnlockMenu()
    {
        if (stageOnUnlockMenu != null)
        {
            stageOnUnlockMenu.transform.SetParent(stagesContent);
            stageOnUnlockMenu.transform.SetSiblingIndex(defaultStageIndex);
            stageOnUnlockMenu = null;
            defaultStageIndex = -1;
            unlockScattered.gameObject.SetActive(false);
        }
    }

    public void ShowUnlockStageMenu(ScatteredTimeStage stage)
    {        
        if(stageOnUnlockMenu != null)
        {
            stageOnUnlockMenu.transform.SetParent(stagesContent);
            stageOnUnlockMenu.transform.SetSiblingIndex(defaultStageIndex);
        }
        if (stageOnUnlockMenu == stage)
        {
            stageOnUnlockMenu = null;
            defaultStageIndex = -1;
            unlockScattered.gameObject.SetActive(false);
            return;
        }

        stageOnUnlockMenu = stage;
        defaultStageIndex = stage.transform.GetSiblingIndex();

        int index = Mathf.Max(stage.transform.GetSiblingIndex(), 0);
        unlockScattered.SetSiblingIndex(index);
        stage.transform.SetParent(unlockScattered);
        stage.transform.SetSiblingIndex(0);     
        unlockButton.interactable = stage.isUnlockable();
        stage.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

        unlockScattered.gameObject.SetActive(true);
    }

    public void UnlockStage()
    {
        stageOnUnlockMenu.isStageUnlocked = true;
        foreach (StackItem material in stageOnUnlockMenu.materials)
        {
            foreach (StackItem item in Inventory.instance.items)
            {
                if (item.item.ID == material.item.ID && item.amount >= material.amount)
                {
                    Inventory.instance.UseAsMaterial(item.item, material.amount);
                    break;
                }
            }
        }
        stageOnUnlockMenu.RefreshCard();
        stagesProgression[stageOnUnlockMenu.stageID] = 1;
        ShowUnlockStageMenu(stageOnUnlockMenu);   
    }
}
