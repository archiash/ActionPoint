using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatteredTimeStageManager : MonoBehaviour
{
    [SerializeField] Transform stagesContent;

    public void CheckIsStageAvailable()
    {
        foreach(Transform s in stagesContent)
        {
            ScatteredTimeStage stage = s.GetComponent<ScatteredTimeStage>();
            stage?.gameObject.SetActive(FollowerTeam.instance.GetFollowerInListByID(stage.stageFollower.followerID) == null);
        }
    }
}
