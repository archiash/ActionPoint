using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/BlueDestiny")]

public class BlueDestiny : Skill
{
    public float normalAttackValue = 1.2f;
    public float magicAttackValue = 1f;
    public int changeToDealExtraDamage = 20;
    public override bool Use<U, T>(U user, T enermy, ArenaType arena = ArenaType.Hunting)
    {
        Status userStat = new Status();
        if (user is Character)
            userStat = (user as Character).status;
        else if (user is Monster)
            userStat = (user as Monster).status;
        Status targetStatus = (enermy is Status) ? enermy as Status : new Status();
        if (enermy is Character)
            targetStatus = (enermy as Character).status;
        else if (enermy is Monster)
            targetStatus = (enermy as Monster).status;

        for(int i = 0; i < 3; i++)
        {
            float damage = userStat.PAtk.Value * normalAttackValue;
            DamageType damageType = DamageType.Physic;

            if(Random.Range(1,101) > 100 - changeToDealExtraDamage)
            {
                damageType = DamageType.Magic;
                damage += userStat.MAtk.Value * magicAttackValue;
                Debug.Log("Destiny!");
            }

            damage = Formula.DamageFormula(userStat, targetStatus,damageType,true,damage,isNormAddExPen:true);
            if (Formula.CriticalFormula(userStat, targetStatus, ref damage))
            {
                Debug.Log($"Critical Damage");
            }
            AttackData attackData = new AttackData(userStat, damage, damageType);
            if (targetStatus.GetDamage(ref attackData))
            {
                Debug.Log($"Deal {damage}");
            }
        }

        return true;
    }

}
