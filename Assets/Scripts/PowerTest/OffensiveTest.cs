using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffensiveTest : MonoBehaviour
{
    public List<Monster> monsters = new List<Monster>();

    public float timeScale = 0;

    public Monster testMonster;

    Status testStatus;
    Monster monster;

    float monsterSpeed;
    float monsterNextTurn = 0f;

    public List<DebuffDamage> testDDPS = new List<DebuffDamage>();

    int[] monsterCooldown;

    public int testTime;
    float offensivePoint = 0;

    public static OffensiveTest instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(RunTest());
    }

    IEnumerator RunTest()
    {
        foreach (Monster m in monsters)
        {
            testMonster = m;
            offensivePoint = 0;
            for (int i = 0; i < testTime; i++)
            {
                StartTest();
                yield return new WaitForSeconds(0.5f);        
            }
            int offensivePower = Mathf.RoundToInt(offensivePoint / testTime / 10);
            Debug.LogWarning($"{testMonster.Name} Offensive Mean: " + offensivePower);
            m.offensivePower = offensivePower;
            
        }
    }

    public void StartTest()
    {
        monster = Instantiate(testMonster);
        testStatus = new Status();
        testStatus.HP.baseValue = 10000000;
        testStatus.currentHP = 10000000;
        testStatus.PDef.baseValue = 1;
        testStatus.MDef.baseValue = 1;
        testStatus.Spd.baseValue = 2.71828f;
        testStatus.Eva.baseValue = 0f;

        monsterNextTurn = 0f;
        monsterCooldown = new int[monster.currentSkill.Count];

        testDDPS.Clear();
        monster.SetSkill();

        Hunt();         
    }

    void MonsterTurn()
    {
        bool isUsedSkill = false;
        monsterNextTurn -= 100;
        //print("Monster Turn");
        if (monster.status.currentHP <= 0)
            return;

        if (!monster.status.isFullMP)
            monster.status.currentMP += monster.status.MP.Value * 5 / 100f;

        //Debug.Log("MP: " + monster.status.currentMP);

        ReduceBuffTurn(monster.status);

        if (monster.currentSkill.Count > 0)
        {
            for (int i = 0; i < monster.currentSkill.Count; i++)
            {
                if (monsterCooldown[i] <= 0 && monster.status.currentMP >= monster.currentSkill[i].manaCost)
                {
                    if (monster.currentSkill[i] is CounterSkill)
                        continue;
                    isUsedSkill = monster.currentSkill[i].Use(monster, testStatus, ArenaType.OffensiveTest);
                    monsterCooldown[i] = monster.currentSkill[i].coolTime + 1;
                    monster.status.currentMP -= monster.currentSkill[i].manaCost;
                    break;
                }
            }
            if (!isUsedSkill)
                MonsterNormalAttack();
        }
        else
            MonsterNormalAttack();

    }

    void MonsterNormalAttack()
    {
        //Debug.Log($"{monster.Name} use Normal Attack");
        float damage = Formula.DamageFormula(monster.status, testStatus);
        if (Formula.CriticalFormula(monster.status, testStatus, ref damage))
            Debug.Log("Critical");
        if (testStatus.GetDamage(ref damage, testStatus))
            Debug.Log($"Deal {damage} Physic Damage to Character");

    }

    void ReduceBuffTurn(Status status)
    {
        foreach (var statName in Enum.GetValues(typeof(SubStatType)))
        {

            Stat stat = (Stat)(status.GetType().GetField(statName.ToString()).GetValue(status));
            float oldValue = stat.Value;
            bool isChange = false;
            for (int i = 0; i < stat.modifiers.Count; i++)
            {

                if (stat.modifiers[i].timeType == Modifier.ModifierTime.Turn)
                {
                    stat.modifiers[i].time--;
                    if (stat.modifiers[i].time < 0)
                    {
                        stat.RemoveModifier(stat.modifiers[i]);
                        isChange = true;
                    }

                }

            }
            if (isChange)
                Debug.Log($"{statName}: {oldValue} -> {stat.Value}");
        }
    }

    void ReduceBuffStep(Status status)
    {
        foreach (var statName in Enum.GetValues(typeof(SubStatType)))
        {

            Stat stat = (Stat)(status.GetType().GetField(statName.ToString()).GetValue(status));
            float oldValue = stat.Value;
            bool isChange = false;
            for (int i = 0; i < stat.modifiers.Count; i++)
            {

                if (stat.modifiers[i].timeType == Modifier.ModifierTime.Step)
                {
                    stat.modifiers[i].time--;
                    if (stat.modifiers[i].time < 0)
                    {
                        stat.RemoveModifier(stat.modifiers[i]);
                        isChange = true;
                    }

                }

            }
            if (isChange)
                Debug.Log($"{statName}: {oldValue} -> {stat.Value}");
        }
    }

    public void CalculateSpeed()
    {
        monsterSpeed = Formula.ActionPerStep(monster.status.Spd.Value);
    }


    void MonsterStep()
    {
        if (!monster.status.isFullMP)
            monster.status.currentMP += Mathf.Pow(Mathf.Log(monster.status.MP.Value), 1.5f) / 1.5f;
    }

    public void Hunt()
    {
        int turnCount = 1500;
        int stepCount = 0;

        int currentStep = 1;

        

        for (int i = 0; i < turnCount; i++)
        {
            stepCount++;
            print("Step: " + stepCount);
            CalculateSpeed();
            monsterNextTurn += monsterSpeed * 10;

            ReduceBuffStep(monster.status);
            DDPS(testStatus, testDDPS);

            MonsterStep();

            for (int z = 0; z < monster.currentSkill.Count; z++)
            {
                monsterCooldown[z] = monsterCooldown[z] > 0 ? monsterCooldown[z] - 1 : monsterCooldown[z];
            }

            while (monsterNextTurn >= 100)
            {
                if (monsterNextTurn >= 100)
                {
                    MonsterTurn();
                }
            }

            if(i + 1 == currentStep)
            {
                currentStep *= 2;
                testStatus.PDef.baseValue *= 2;
                testStatus.MDef.baseValue *= 2;
            }

            //yield return new WaitForSecondsRealtime(0.1f * timeScale);
            
        }
        //Debug.LogWarning("Result");
        offensivePoint += testStatus.HP.baseValue - testStatus.currentHP;
        //Debug.LogWarning(testStatus.HP.baseValue - testStatus.currentHP);
        Destroy(monster);
        
    }
    void DDPS(Status status, List<DebuffDamage> DDPS)
    {

        for (int i = 0; i < DDPS.Count; i++)
        {
            if (DDPS[i].turnDuration > 0)
            {
                float damage = Formula.DamageFormula(DDPS[i].userStat, status, DDPS[i].damageType, true, DDPS[i].dps, DDPS[i].penetrate, false);
                status.GetDamage(ref damage, DDPS[i].userStat, false);
                DDPS[i].turnDuration--;
                Debug.Log(DDPS[i].source.skillName + " Deal Damage " + damage + " Trun Left " + DDPS[i].turnDuration);

            }
            else
            {
                DDPS.Remove(DDPS[i]);
            }
        }
    }

}
