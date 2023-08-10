using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
[CreateAssetMenu]
public class FollowerDatabase : ScriptableObject
{
    [SerializeField] Follower[] followers;

    public Follower GetFollowerReference(int ID)
    {
        foreach (Follower follower in followers)
        {
            if (follower.followerID == ID)
            {
                return follower;
            }
        }
        return null;
    }

    public Follower GetFollowerCopy(int ID)
    {
        Follower follower = GetFollowerReference(ID);
        if (follower == null) return null;
        return follower.GetCopy();
    }

#if UNITY_EDITOR

    private void LoadFollowerList()
    {
        followers = ItemDatabase.FindAssetsByType<Follower>("Assets/Follower");
    }

    private void OnValidate()
    {
        LoadFollowerList();
    }

    private void OnEnable()
    {
        EditorApplication.projectChanged += LoadFollowerList;
    }

    private void OnDisable()
    {
        EditorApplication.projectChanged -= LoadFollowerList;
    }

#endif
}
