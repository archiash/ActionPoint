using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FollowerSelectionCard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI followerName;
    [SerializeField] TextMeshProUGUI followerLevel;
    [SerializeField] Image followerImage;
    Follower follower;

    public void Init(Follower follower)
    {
        gameObject.GetComponent<Button>().onClick.AddListener(delegate { UIManager.Instance.followerSelectionMenu.ShowFollowerDetail(follower); });
        this.follower = follower;
        followerName.text = follower.followerName;
        followerLevel.text = $"Level {follower.followerLevel}";
        followerImage.sprite = follower.followerImage;
    }
}
