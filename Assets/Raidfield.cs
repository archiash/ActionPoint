using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raidfield : MonoBehaviour
{
    public void Raiding(Monster monster)
    {
        UIManager.Instance.huntingManager.Setup(monster, FollowerTeam.instance.followerTeam);
        UIManager.Instance.huntingManager.StartBattle(true);
        
    }
}
