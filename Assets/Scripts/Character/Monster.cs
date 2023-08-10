using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Create/Monster/Monster", fileName = "New Monster")]
public class Monster : ScriptableObject
{
    public int offensivePower;

    public int defensivePower;

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
            $"\nPenetrage: {status.Pen.Value}" +
            $"\nMagic: {status.MAtk.Value}              \nMagicResist: {status.MDef.Value}" +
            $"\nNeutralize: {status.Neu.Value}" +
            $"\nHit: {status.Hit.Value}                 \nEva: {status.Eva.Value}" +
            $"\nCriticalRate: {status.Crate.Value}      \nCriticalDamage: {status.Cdmg.Value}" +
            $"\nCriticalResist: {status.Cres.Value}" +
            $"\nSpeed: {status.Spd.Value}";

        return desc;
    }

    public virtual string GetStatus()
    {
        string stat = $"HP: {status.HP.Value}               \nMP: {status.MP.Value}" +
            $"\nAttack: {status.PAtk.Value}             \nDefense: {status.PDef.Value}" + 
            $"\nPenetrage: {status.Pen.Value}" +
            $"\nMagic: {status.MAtk.Value}              \nMagicResist: {status.MDef.Value}" +
            $"\nNeutralize: {status.Neu.Value}" +
            $"\nHit: {status.Hit.Value}                 \nEva: {status.Eva.Value}" +
            $"\nCriticalRate: {status.Crate.Value}      \nCriticalDamage: {status.Cdmg.Value}" +
            $"\nCriticalResist: {status.Cres.Value}" +
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
<<<<<<< Updated upstream
=======

            if(currentSkill[i] is OnGetHitSkill)
            {
                ((OnGetHitSkill)currentSkill[i]).Activate<Status,object>(status,null);
            }
>>>>>>> Stashed changes
        }
    }

    public void Powerlize(float powerlizeValue)
    {
        status.HP.baseValue *= powerlizeValue;
        status.MP.baseValue *= powerlizeValue;
        status.PAtk.baseValue *= powerlizeValue;
        status.PDef.baseValue *= powerlizeValue;
        status.Pen.baseValue *= powerlizeValue;
        status.MAtk.baseValue *= powerlizeValue;
        status.MDef.baseValue *= powerlizeValue;
        status.Neu.baseValue *= powerlizeValue;
        status.Spd.baseValue *= powerlizeValue;
        status.Eva.baseValue *= powerlizeValue;
        status.Hit.baseValue *= powerlizeValue;
        status.Crate.baseValue *= powerlizeValue;
        status.Cres.baseValue *= powerlizeValue;
        status.Cdmg.baseValue *= powerlizeValue;
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(Monster))]
public class MonsterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Monster t = (Monster)target;
        GUILayout.Label(new GUIContent($"Offensive Power: {t.offensivePower}"));
        GUILayout.Label(new GUIContent($"Defensive Power: {t.defensivePower}"));
        base.OnInspectorGUI();
        if(Application.isPlaying)
            if(GUILayout.Button("Hunt"))
            {
                UIManager.Instance.huntingManager.Setup(t,null);
                UIManager.Instance.huntingManager.Hunt();
            }
    }
}
#endif