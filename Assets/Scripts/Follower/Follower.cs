using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Follower")]
public class Follower : ScriptableObject
{
    [System.Serializable]
    public class FollowerModifier
    {
        public StatType statType;
        public MainStatType mainStatType;
        public SubStatType subStatType;
        public float value;
        public float valuePerLevel;

        public float GetValue(int level)
        {
            return value + (level - 1) * valuePerLevel;
        }
    }

    [System.Serializable]
    public class FollowerSkill
    {
        public Skill skill;
        public int interval;
    }

    [System.Serializable]
    public class FollowerStatus
    {
        public MainStat StrV;
        public MainStat DexV;
        public MainStat AgiV;
        public MainStat IntV;
        public MainStat ConV;

        public float StrPLv;
        public float DexPLv;
        public float AgiPLv;
        public float IntPLv;
        public float ConPLv;

        public int[] MainStatArray(int level)
        {
            return new int[5] { STR(level), DEX(level), AGI(level), INT(level), CON(level) };
        }

        public int STR(int level)
        {
            return (int)StrV.Value + (int)((level - 1) * StrPLv);
        }

        public int DEX(int level)
        {
            return (int)DexV.Value + (int)((level - 1) * DexPLv);
        }

        public int AGI(int level)
        {
            return (int)AgiV.Value + (int)((level - 1) * AgiPLv);
        }

        public int INT(int level)
        {
            return (int)IntV.Value + (int)((level - 1) * IntPLv);
        }

        public int CON(int level)
        {
            return (int)ConV.Value + (int)((level - 1) * ConPLv);
        }

    }

    public Sprite followerSquareImage;
    public Sprite followerImage;

    public string followerName;
    public string followerDescription;
    
    public int followerLevel;
    public int followerID;

    public FollowerStatus followerStatus;

    public List<FollowerModifier> followerModifiers;

    public List<FollowerSkill> folowerSkills;

    public Follower GetCopy()
    {
        Follower newFollower = Instantiate(this);
        newFollower.followerLevel = 1;
        return newFollower;
    }

    public string statusList
    {
        get
        {
            string status = "";
            for(int i = 0; i < followerModifiers.Count; i++)
            {
                status += followerModifiers[i].statType == StatType.Main ?
                    followerModifiers[i].mainStatType.ToString()
                    : followerModifiers[i].subStatType.ToString();
                status += $" + {followerModifiers[i].GetValue(followerLevel)}\n";
            }
            return status;
        }
    }
}
