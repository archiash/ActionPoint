using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum DamageType
{
    Physic,Magic
}

public enum CriticalType
{
    Able,Unable,Sure
}

public enum ArenaType
{
    Hunting,Raiding,OffensiveTest, DefensiveTest
}

public enum DDType
{
    Poison,Stun,Stone
}

public class DebuffDamage
{
    public Skill source;
    public string userName;
    public Status userStat;
    public DDType ddType;
    public DamageType damageType;
    public float dps;
    public int turnDuration;
    public int penetrate;

    public DebuffDamage(Skill skill,string user,Status status,DDType dDType,DamageType damageType,float damage,int duration,int penetrate)
    {
        source = skill;
        userName = user;
        userStat = status;
        ddType = dDType;
        this.damageType = damageType;
        dps = damage;
        turnDuration = duration;
        this.penetrate = penetrate;
    }
}

public class Skill : ScriptableObject
{
    public Sprite skillIcon;
    public string skillName;
    [TextArea(1,5)]
    public string skillDesc;
    public float manaCost;
    public int coolTime;

    public virtual bool Use<U,T>(U user,T enermy,ArenaType arena = ArenaType.Hunting)
    {

        return false;
    }

    public virtual bool Condition()
    {
        return false;
    }

    public virtual void Activate<U,T>(U user, T enermy, ArenaType arena = ArenaType.Hunting)
    {

    }


}
