using UnityEngine;

[CreateAssetMenu(menuName = "Skill/HookBlade")]
public class HookBlade : Skill
{
    public override bool Use<U, T>(U user, T enermy, ArenaType arena = ArenaType.Hunting)
    {
        Status userStat = new Status();
        if (user is Character)
            userStat = (user as Character).status;
        else if(user is Monster)
            userStat = (user as Monster).status;

        Status targetStatus = (enermy is Status) ? enermy as Status : new Status();
        if (enermy is Character)
            targetStatus = (enermy as Character).status;
        else if(enermy is Monster)
            targetStatus = (enermy as Monster).status;

        float damage = Formula.DamageFormula(userStat, targetStatus);
        if (Formula.CriticalFormula(userStat, targetStatus, ref damage)) 
        {
            Debug.Log($"Critical Damage");
        }
        AttackData attackData = new AttackData(userStat, damage);
        if (targetStatus.GetDamage(ref attackData)) 
        {
            Debug.Log($"Deal {damage}");
        }
        else
        {
            Debug.Log("HookBlade");
            damage = Formula.DamageFormula(userStat, targetStatus) / 2;
            if (Formula.CriticalFormula(userStat, targetStatus, ref damage))
            {
                Debug.Log($"Critical Damage");
            }
            attackData = new AttackData(userStat, damage);
            if (targetStatus.GetDamage(ref attackData, evadeReduce : 50))
            {
                Debug.Log($"Deal {damage}");
            }
        }

        return true;
    }

}
