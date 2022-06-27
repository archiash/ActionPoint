using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LvBonusType
{
    pointRate,
    healRate
}
[CreateAssetMenu(menuName = "LevelBonus")]
public class LevelBonus : ScriptableObject
{
    [System.Serializable]
    public class Bonus
    {
        public int requireLevel;
        public LvBonusType bonusType;
        public float value;
    }

    public List<Bonus> bonus = new List<Bonus>();
}
