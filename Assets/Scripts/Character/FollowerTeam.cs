using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowerTeam : MonoBehaviour
{
    public static FollowerTeam instance;

    public List<Follower> followerList;

    public Follower follower1;
    public Follower follower2;
    public Follower follower3;

    [SerializeField] Image followerImage1;
    [SerializeField] Image followerImage2;
    [SerializeField] Image followerImage3;

    [SerializeField] Sprite follwerPlusImage;

    [SerializeField] Follower testFollower;

    public List<Follower> followerTeam
    {
        get
        {
            List<Follower> team = new List <Follower>();
            if (follower1!= null) team.Add(follower1);
            if (follower2!= null) team.Add(follower2);
            if (follower3!= null) team.Add(follower3);
            return team;
        }
    }

    public Follower GetFollowerInListByID(int ID)
    {
        for(int i = 0; i < followerList.Count; i++)
        {
            if (followerList[i].followerID == ID)
            {
                return followerList[i];
            }
        }

        return null;
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (testFollower != null && follower1 == null)
        {
            //Debug.Log("Use Test Follower");
            //followerList.Add(testFollower.GetCopy());
            //ChangeFollower(followerList[0], 1);
        }
    }

    public void ChangeFollower(Follower newFollower, int index)
    {
        switch (index)
        {
            case 1:
                RemoveFollower(ref follower1);
                EquipFollower(ref follower1, newFollower);
                followerImage1.sprite = newFollower.followerImage;
                followerImage1.enabled = true;
                break;
            case 2:
                RemoveFollower(ref follower2);
                EquipFollower(ref follower2, newFollower);
                followerImage2.sprite = newFollower.followerImage;
                followerImage2.enabled = true;
                break;
            case 3:
                RemoveFollower(ref follower3);
                EquipFollower(ref follower3, newFollower);
                followerImage3.sprite = newFollower.followerImage;
                followerImage3.enabled = true;
                break;
        }
    }

    public void DisableFollowerImage(int index)
    {
        bool isPlus = true;//followerTeam.Count < followerList.Count;
        switch (index)
        {
            case 1:
                //followerImage1.enabled = false;
                if (isPlus)
                {
                    followerImage1.sprite = follwerPlusImage;
                    followerImage1.enabled = true;
                }
                break;
            case 2:
                //followerImage2.enabled = false;
                if (isPlus)
                {
                    followerImage2.sprite = follwerPlusImage;
                    followerImage2.enabled = true;
                }
                break;
            case 3:
                //followerImage3.enabled = false;
                if (isPlus)
                {
                    followerImage3.sprite = follwerPlusImage;
                    followerImage3.enabled = true;
                }
                break;
        }
    }

    public void RemoveFollowerAtIndex(int index)
    {      
        switch (index)
        {
            case 1:
                RemoveFollower(ref follower1);
                break;
            case 2:
                RemoveFollower(ref follower2);
                break;
            case 3:
                RemoveFollower(ref follower3);
                break;
        }
        DisableFollowerImage(index);
    }

    public void RemoveFollower(ref Follower follower)
    {      
        if (follower != null)
        {
            Character.instance.RemoveModifier(follower);
            follower = null;

        }
    }

    public void EquipFollower(ref Follower follower, Follower newFollower)
    {
        follower = newFollower;
        //followerList.Remove(newFollower);
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
    
    public Follower GetFollowerAtIndex(int index)
    {
        switch (index)
        {
            case 1:
                return follower1;
            case 2:
                return follower2;
            case 3:
                return follower3;
        }
        return null;
    }
}
