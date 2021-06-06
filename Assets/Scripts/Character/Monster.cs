using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Create/Monster/Monster", fileName = "New Monster")]
public class Monster : ScriptableObject
{
    public Sprite sprite;
    public string Name;
    public string Desc;

    public Status status;

    public List<Skill> currentSkill = new List<Skill>();

    public int usePoint;
    public int expReward;
    public DropTable[] dropTables;

    public virtual string GetDesc()
    {
        string desc = Desc;
        desc += $"\nHP: {status.HP.Value}               \nMP: {status.MP.Value}" +
            $"\nAttack: {status.PAtk.Value}             \nDefense: {status.PDef.Value}" +
            $"\nMagic: {status.MAtk.Value}              \nMagicResist: {status.MDef.Value}" +
            $"\nHit: {status.Hit.Value}                 \nEva: {status.Eva.Value}" +
            $"\nCriticalRate: {status.Crate.Value}      \nCriticalDamage: {status.Cdmg.Value}" +
            $"\nSpeed: {status.Spd.Value}"; 
            
        return desc;
    }

    public virtual string GetStatus()
    {
        string stat = $"\nHP: {status.HP.Value}               \nMP: {status.MP.Value}" +
            $"\nAttack: {status.PAtk.Value}             \nDefense: {status.PDef.Value}" +
            $"\nMagic: {status.MAtk.Value}              \nMagicResist: {status.MDef.Value}" +
            $"\nHit: {status.Hit.Value}                 \nEva: {status.Eva.Value}" +
            $"\nCriticalRate: {status.Crate.Value}      \nCriticalDamage: {status.Cdmg.Value}" +
            $"\nSpeed: {status.Spd.Value}";

        return stat;
    }

    public virtual string GetSkillList()
    {
        string skilldesc = "";
        for(int i = 0;i< currentSkill.Count;i++)
        {
            skilldesc += string.Format($"{i + 1}. {currentSkill[i].skillName}\n" +
                $"{currentSkill[i].skillDesc}\n");
        }
        return skilldesc;
    }

    public virtual void SetSkill()
    {
        status.counterSkill = null;
        for(int i = 0;i < currentSkill.Count; i++)
        {
            if(currentSkill[i] is CounterSkill)
            {
                status.counterSkill += ((CounterSkill)currentSkill[i]).Use;           
            }
        }
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(Monster))]
public class MonsterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Monster t = (Monster)target;
        base.OnInspectorGUI();
        if(Application.isPlaying)
            if(GUILayout.Button("Hunt"))
            {
                HuntingManager.instance.Setup(t);
                HuntingManager.instance.Hunt();
            }
    }
}
#endif