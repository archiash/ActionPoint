using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatteredTimeStage : MonoBehaviour
{
    public string stageName;
    public string stageDescription;

    public Follower stageFollower;

    public CraftMaterial[] materials;

    public Monster stageEnermy;

    public void RefreshMaterial()
    {
        foreach(CraftMaterial m in materials)
        {
            m.Refrest();
        }
    }

    public void ShowThisStage(GameObject scatteredTimeMenu)
    {
        scatteredTimeMenu.GetComponent<StageDetail>().ShowStageDetail(this);
    }
}
