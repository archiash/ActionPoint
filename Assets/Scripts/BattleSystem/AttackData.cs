using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackData
{
    public Dictionary<string, float> overideData;
    public Status attacker;
    public float damage;
    public DamageType damageType;

    public AttackData(Status attacker, float damage, DamageType damageType = DamageType.Physic)
    {
        this.attacker = attacker;
        this.damage = damage;
        this.damageType = damageType;
    }
}
