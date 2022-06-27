using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/BlueDestiny")]

public class BlueDestiny : Skill
{
    public override bool Use<U, T>(U user, T enermy, ArenaType arena = ArenaType.Hunting)
    {
        Status userStat = new Status();
        if (user is Character)
            userStat = (user as Character).status;
        else
            userStat = (user as Monster).status;
        Status targetStatus = new Status();
        if (enermy is Character)
            targetStatus = (enermy as Character).status;
        else
            targetStatus = (enermy as Monster).status;

        for(int i = 0; i < 3; i++)
        {
            float damage = userStat.PAtk.Value * 1.20f;
            DamageType damageType = DamageType.Physic;

            if(Random.Range(1,101) > 80)
            {
                damageType = DamageType.Magic;
                damage += userStat.MAtk.Value;
                Debug.Log("Destiny!");
            }

            damage = Formula.DamageFormula(userStat, targetStatus,damageType,true,damage,isNormAddExPen:true);
            if (Formula.CriticalFormula(userStat, targetStatus, ref damage))
            {
                Debug.Log($"Critical Damage");
            }
            if (targetStatus.GetDamage(ref damage, userStat))
            {
                Debug.Log($"Deal {damage}");
            }
        }

        return true;
    }

}
