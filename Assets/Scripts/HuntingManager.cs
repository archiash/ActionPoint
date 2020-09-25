﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEditor;
using System;

public class HuntingManager : MonoBehaviour
{
    public static HuntingManager instance;
    private void Start()
    {
        instance = this;
    }

    Character character;
    Monster monster;

    public Monster testMonster;

    float characterSpeed;
    float monsterSpeed;

    float characterNextTurn = 0f;
    float monsterNextTurn = 0f;

    float previousHp;

    public void Setup(Monster _monster = null, Character _character = null)
    {
        if (_character == null)
            character = Character.instance;
        else
            character = _character;
        if (_monster == null)
            monster = Instantiate(testMonster);
        else
            monster = Instantiate(_monster);

        monsterNextTurn = 0f;
        characterNextTurn = 0f;

        characterSkill = 0;
        monsterSkill = 0;

        characterSkillCooltime = 0;
        monsterSkillCooltime = 0;

        character.status.currentMP = character.status.MP.Value;
        previousHp = character.status.currentHP;
    }

    void CalculateSpeed()
    {
        if (character.status.Spd.Value < monster.status.Spd.Value)
        {
            characterSpeed = character.status.Spd.Value / character.status.Spd.Value;
            monsterSpeed = monster.status.Spd.Value / character.status.Spd.Value;
        }
        else
        {
            characterSpeed = character.status.Spd.Value / monster.status.Spd.Value;
            monsterSpeed = monster.status.Spd.Value / monster.status.Spd.Value;
        }
    }

    int characterSkill = 0;
    int monsterSkill = 0;

    int characterSkillCooltime = 0;
    int monsterSkillCooltime = 0;

    float debugValue;

    void CharacterTurn()
    {
        
        characterNextTurn -= 10;
        print("Character Turn");
        if (character.status.currentHP <= 0)
            return;

        ReduceBuffTurn(character.status);

        if (character.currentSkill.Count > 0 && characterSkillCooltime <= 0)
        {
            character.currentSkill[characterSkill].ActiveSkill(character.status, monster.status);

            characterSkill++;
            if (characterSkill >= character.currentSkill.Count)
                characterSkill = 0;

            characterSkillCooltime = character.currentSkill[characterSkill].coolTime;
        }
        else           
            CharacterNormalAttack();

        

    }
    void MonsterTurn()
    {
        
        monsterNextTurn -= 10;
        print("Monster Turn");
        if (monster.status.currentHP <= 0)
            return;


        ReduceBuffTurn(monster.status);

        if (monster.currentSkill.Count > 0 && monsterSkillCooltime <= 0)
        {                 
            monster.currentSkill[monsterSkill].ActiveSkill(monster.status, character.status);

            monsterSkill++;              
            if (monsterSkill >= monster.currentSkill.Count)                    
                monsterSkill = 0;              
            monsterSkillCooltime = monster.currentSkill[monsterSkill].coolTime;
        }
        else               
            MonsterNormalAttack();

        
    }
    void CharacterNormalAttack()
    {
        Debug.Log("Character use Normal Attack");

        float damage = character.status.PAtk.Value;
        if (Uility.IsCritical(character.status.Crate.Value))
            damage = Uility.CriticalDamage(damage, character.status.Cdmg.Value);
        
        monster.status.GetDamage(ref damage,DamageType.Physic,character.status);

        if(damage != -1)
            Debug.Log($"Deal {damage} Physic Damage to {monster.Name}");
                  
        characterSkillCooltime--;
    }
    void MonsterNormalAttack()
    {
        Debug.Log($"{monster.Name} use Normal Attack");

        float damage = monster.status.PAtk.Value;
        if (Uility.IsCritical(monster.status.Crate.Value))
            damage = Uility.CriticalDamage(damage, monster.status.Cdmg.Value);

        character.status.GetDamage(ref damage, DamageType.Physic, monster.status);
        if (damage != -1)
            Debug.Log($"Deal {damage} Physic Damage to Character");
        monsterSkillCooltime--;
    }
    void ReduceBuffTurn(Status status)
    {
        foreach(var statName in Enum.GetValues(typeof(SubStatType)))
        {
             
            Stat stat = (Stat)(status.GetType().GetField(statName.ToString()).GetValue(status));
            float oldValue = stat.Value;
            bool isChange = false;
            for (int i = 0;i<stat.modifiers.Count;i++)
            {
                
                if(stat.modifiers[i].timeType == Modifier.ModifierTime.Turn)
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

    public void Hunt()
   {
        int turnCount = 500;
        int endturn = 0;
        for(int i = 0;i<turnCount;)
        {
            CalculateSpeed();
            characterNextTurn += characterSpeed;
            monsterNextTurn += monsterSpeed;

            while (characterNextTurn >= 10 || monsterNextTurn >= 10)
            {
                if (characterNextTurn >= 10 && monsterNextTurn >= 10)
                {
                    if (characterNextTurn > monsterNextTurn)
                    {
                        CharacterTurn();
                        i++;
                    }else
                    if (characterNextTurn < monsterNextTurn)
                    {
                        MonsterTurn();
                        i++;
                    }else
                    if (characterNextTurn == monsterNextTurn)
                    {
                        if (character.status.Spd.Value < monster.status.Spd.Value)
                        {
                            MonsterTurn();
                            i++;
                        }
                        else
                        {
                            CharacterTurn();
                            i++;
                        }
                    }
                }
                else
                {
                    if (characterNextTurn >= 10)
                    {
                        CharacterTurn();
                        i++;
                    }

                    if (monsterNextTurn >= 10)
                    {
                        MonsterTurn();
                        i++;
                    }
                }
            }

            if (CheckResult(i))
            {
                endturn = i;
                i = 500;
            }
                

           
        }

        EndBattle();
        Destroy(monster);
        Debug.Log(endturn);
    }
   
    public bool CheckResult(int turn)
    {
        if(character.status.currentHP <= 0)
        {
            print("Lose");
            ResultReport.instance.ShowResult("เเพ้");
            return true;

        }
        else if (monster.status.currentHP <= 0)
        {
            print("Win");
            GiveReward();
            ResultReport.instance.ShowResult("ชนะ");
            return true;
        }else if(turn == 499)
        {
            print("Time Up");
            ResultReport.instance.ShowResult("เสมอ");
        }

        return false;
    }

    void RemoveInHuntBuff(Status status)
    {
        foreach (var statName in Enum.GetValues(typeof(SubStatType)))
        {
            Stat stat = (Stat)(status.GetType().GetField(statName.ToString()).GetValue(status));
            for (int i = 0; i < stat.modifiers.Count; i++)
            {
                if (stat.modifiers[i].timeType == Modifier.ModifierTime.Turn)
                {
                        stat.RemoveModifier(stat.modifiers[i]);
                }
            }
        }
    }
    void ReduceHuntBuff(Status status)
    {
        foreach (var statName in Enum.GetValues(typeof(SubStatType)))
        {
            Stat stat = (Stat)(status.GetType().GetField(statName.ToString()).GetValue(status));
            float oldValue = stat.Value;
            bool isChange = false;
            for (int i = 0; i < stat.modifiers.Count; i++)
            {

                if (stat.modifiers[i].timeType == Modifier.ModifierTime.Hunt)
                {
                    stat.modifiers[i].time--;
                    if (stat.modifiers[i].time <= 0)
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
    void EndBattle()
    {
        

        if (character.status.currentHP > previousHp)
            character.status.currentHP = previousHp;

        Debug.Log(previousHp - character.status.currentHP);
        //character.status.currentHP = character.status.HP.Value;
        

        RemoveInHuntBuff(character.status);
        ReduceHuntBuff(character.status);
    }

    public void StartBattle()
    {        
        Setup();
        Hunt();
    }

    void GiveReward()
    {       
        for (int i = 0;i< monster.dropItems.Length;i++)
        {      
           if( Random.value <= monster.dropItems[i].rateDrop/100f)
           {
                int amount = Random.Range(monster.dropItems[i].minDrop, monster.dropItems[i].maxDrop + 1);
                Inventory.instance.GetItem(monster.dropItems[i].item, amount);
                ResultReport.instance.AddDrop(monster.dropItems[i].item, amount);
                
           }
        }
    }

}
#if UNITY_EDITOR
[CustomEditor(typeof(HuntingManager))]
public class HuntingManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        HuntingManager t = (HuntingManager)target;

        base.OnInspectorGUI();
        if(Application.isPlaying && t.testMonster != null)
            if(GUILayout.Button("Test Hunt"))
            {
                t.StartBattle();
            }
    }
}
#endif