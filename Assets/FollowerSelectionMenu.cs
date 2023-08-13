using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FollowerSelectionMenu : MonoBehaviour
{
    [SerializeField] FollowerSelectionCard followerSelectedCardPrefab;
    [SerializeField] Transform cardsContent;

    [SerializeField] TextMeshProUGUI followerName;
    [SerializeField] TextMeshProUGUI followerLevel;
    [SerializeField] TextMeshProUGUI followerDesc;
    [SerializeField] TextMeshProUGUI followerSkill;
    [SerializeField] Image followerImage;



    [SerializeField] Button changeButton;
    [SerializeField] Button removeButton;

    private Follower follower;
    private Follower followerInSlot;
    private int slotIndex;

    public void ShowFollowerDetail(Follower follower)
    {
        this.follower = follower;
        followerName.text = follower.followerName;
        followerLevel.text = $"Level {follower.followerLevel}";
        followerImage.sprite = follower.followerImage;
        followerDesc.text = follower.statusList;
        followerSkill.text = follower.folowerSkills[0].skill.skillName + " CD: " + follower.folowerSkills[0].interval + " steps";
        followerSkill.text += $"\n{follower.folowerSkills[0].skill.skillDesc}";
        changeButton.gameObject.SetActive(followerInSlot != follower);
        removeButton.gameObject.SetActive(followerInSlot == follower);
        followerName.enabled = true;
        followerLevel.enabled = true;
        followerImage.enabled = true;
        followerDesc.enabled = true;
        followerSkill.enabled = true;
    }

    public void ShowFollowersCardList(int slotIndex)
    {
        List<Follower> ft = FollowerTeam.instance.followerTeam;
        ClearCardsList();
        List<Follower> followers = FollowerTeam.instance.followerList;
        for(int i = 0; i < followers.Count; i++)
        {
            if (!ft.Contains(followers[i]))
            {
                Debug.Log(followers[i].followerName);
                FollowerSelectionCard card = Instantiate(followerSelectedCardPrefab, cardsContent);
                card.Init(followers[i]);
            }
        }

        this.slotIndex = slotIndex;
        followerInSlot = FollowerTeam.instance.GetFollowerAtIndex(slotIndex);

        if (followerInSlot != null)
        {
            ShowFollowerDetail(followerInSlot);
        }
        else ClearDetail();
        
    }

    private void ClearCardsList()
    {
        foreach(Transform obj in cardsContent)
        {
            Destroy(obj.gameObject);
        }
    }

    public void RemoveFollower()
    {
        FollowerTeam.instance.RemoveFollowerAtIndex(slotIndex);
        followerInSlot = null;
        ClearDetail();
    }

    private void ClearDetail()
    {
        followerName.enabled = false;
        followerLevel.enabled = false;
        followerImage.enabled = false;
        followerDesc.enabled = false;
        followerSkill.enabled = false;
        changeButton.gameObject.SetActive(false);
        removeButton.gameObject.SetActive(false);
    }

    public void ChangeFollower()
    {
        FollowerTeam.instance.ChangeFollower(follower, slotIndex);
    }
}
