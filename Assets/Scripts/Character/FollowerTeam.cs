using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerTeam : MonoBehaviour
{
    public static FollowerTeam instance;

    public List<Follower> followerList;

    public Follower follower1;
    public Follower follower2;
    public Follower follower3;

    [SerializeField] Follower testFollower;

    public List<Follower> followerTeam
    {
        get
        {
            List<Follower> team = new List <Follower>();
            if (follower1 != null) team.Add(follower1);
            if (follower2!= null) team.Add(follower2);
            if (follower3!= null) team.Add(follower3);
            return team;
        }
    }

    private void Start()
    {
        instance= this;
        if (testFollower != null)
        {
            //followerList.Add(testFollower.GetCopy());
            //ChangeFollower(followerList[0], 1);
        }
    }

    public void ChangeFollower(Follower newFollower, int index)
    {
        switch(index)
        {
            case 1:
                RemoveFollower(ref follower1);
                EquipFollower(ref follower1, newFollower);
                break;
            case 2:
                RemoveFollower(ref follower2);
                EquipFollower(ref follower2, newFollower);
                break;
            case 3:
                RemoveFollower(ref follower3);
                EquipFollower(ref follower3, newFollower);
                break;
        }
    }

    public void RemoveFollower(ref Follower follower)
    {
        if (follower != null)
        {
            Character.instance.RemoveModifier(follower);
            followerList.Add(follower);
            follower = null;
        }
    }

    public void EquipFollower(ref Follower follower, Follower newFollower)
    {
        follower = newFollower;
        followerList.Remove(newFollower);
        for(int i = 0; i < follower.followerModifiers.Count; i++)
        {
            Follower.FollowerModifier followerModifier = follower.followerModifiers[i];
            float value = followerModifier.GetValue(follower.followerLevel);
            Modifier modifier = new Modifier(value, follower);
            if (followerModifier.statType == StatType.Main)
            {
                Character.instance.AddModifier(followerModifier.mainStatType, modifier);
            }
            else
            {
                Character.instance.AddModifier(followerModifier.subStatType, modifier);
            }
        }
    }

}
