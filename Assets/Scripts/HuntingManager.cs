using System.Collections;
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

    public List<string> huntReport = new List<string>();

    float characterSpeed;
    float monsterSpeed;

    float characterNextTurn = 0f;
    float monsterNextTurn = 0f;

    float previousHp;

    public List<DebuffDamage> characterDDPS = new List<DebuffDamage>();
    public List<DebuffDamage> monsterDDPS = new List<DebuffDamage>();

    public int characterStun;
    public int monsterStun;

    private int powerlize;
    public void Setup(Monster _monster = null, Character _character = null,int powerlize = 1)
    {
        this.powerlize = powerlize;

        if (_character == null)
            character = Character.instance;
        else
            character = _character;
        if (_monster == null)
            monster = Instantiate(testMonster);
        else
            monster = Instantiate(_monster);

        character.status.currentMP = character.status.MP.Value;

        monsterNextTurn = 0f;
        characterNextTurn = 0f;

        characterCooldown = new int[character.currentSkill.Count];
        monsterCooldown = new int[monster.currentSkill.Count];

        character.status.currentMP = character.status.MP.Value;
        previousHp = character.status.currentHP;

        characterStun = 0;
        monsterStun = 0;

        characterDDPS.Clear();
        monsterDDPS.Clear();
        monster.SetSkill();
    }

    void CalculateSpeed()
    {
        if (character.status.Spd.Value < monster.status.Spd.Value)
        {
            characterSpeed = (10 + character.status.Spd.Value) * 100f / (100 + character.status.Spd.Value);
            monsterSpeed = (10 + monster.status.Spd.Value) * 100f / (100 + character.status.Spd.Value);
        }
        else
        {
            characterSpeed = (10 + character.status.Spd.Value) * 100f / ( 100 +monster.status.Spd.Value);
            monsterSpeed = (10 + monster.status.Spd.Value) * 100f / (100 + monster.status.Spd.Value);
        }
    }

    int[] characterCooldown;
    int[] monsterCooldown;

    void CharacterTurn()
    {
        bool isUsedSkill = false;
        characterNextTurn -= 100;
        print("Character Turn");
        if (character.status.currentHP <= 0)
            return;

        if (!character.status.isFullMP)
            character.status.currentMP += character.status.MP.Value * 5 / 100f;

        DDPS(character.status, characterDDPS);
        ReduceBuffTurn(character.status);

        if (characterStun <= 0)
        {
            if (character.currentSkill.Count > 0)
            {
                for (int i = 0; i < character.currentSkill.Count; i++)
                {
                    if (characterCooldown[i] <= 0 && character.status.currentMP >= character.currentSkill[i].manaCost)
                    {
                        if (character.currentSkill[i] is CounterSkill)
                            continue;
                        isUsedSkill = character.currentSkill[i].Use(character, monster, ArenaType.Hunting);
                        characterCooldown[i] = character.currentSkill[i].coolTime + 1;
                        character.status.currentMP -= character.currentSkill[i].manaCost;
                        break;
                    }
                }
                if (!isUsedSkill)
                    CharacterNormalAttack();
            }
            else
                CharacterNormalAttack();
        }

        for(int i = 0; i< character.currentSkill.Count;i++)
        {
            characterCooldown[i] = characterCooldown[i] > 0 ? characterCooldown[i] - 1 : characterCooldown[i];
        }

        if (characterStun > 0)
        {
            characterStun--;
            Debug.Log("STUN Turn Left" + characterStun);
        }

    }
    void MonsterTurn()
    {
        bool isUsedSkill = false;
        monsterNextTurn -= 100;
        print("Monster Turn");
        if (monster.status.currentHP <= 0)
            return;

        if (!monster.status.isFullMP)
            monster.status.currentMP += monster.status.MP.Value * 5 / 100f;

        Debug.Log("MP: " + monster.status.currentMP);

        DDPS(monster.status, monsterDDPS);
        ReduceBuffTurn(monster.status);



        if (monsterStun <= 0)
        {
            if (monster.currentSkill.Count > 0)
            {
                for (int i = 0; i < monster.currentSkill.Count; i++)
                {
                    if (monsterCooldown[i] <= 0 && monster.status.currentMP >= monster.currentSkill[i].manaCost)
                    {
                        if (monster.currentSkill[i] is CounterSkill)
                            continue;
                        isUsedSkill = monster.currentSkill[i].Use(monster, character, ArenaType.Hunting);
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
        for (int i = 0; i < monster.currentSkill.Count; i++)
        {
            monsterCooldown[i] = monsterCooldown[i] > 0 ? monsterCooldown[i] - 1 : monsterCooldown[i];
        }

        if (monsterStun > 0)
        {
            monsterStun--;
            Debug.Log("STUN Turn Left" + monsterStun);
        }
    }
    void CharacterNormalAttack()
    {
        (float pureDamage, DamageType damageType) = character.GetDamageAttack();
        Debug.Log("Character use Normal Attack");
        float damage = Formula.DamageFormula(character.status, monster.status, damageType,true,pureDamage,0,true);
        if (Formula.CriticalFormula(character.status, monster.status, ref damage))
            Debug.Log("Critical");
        if (monster.status.GetDamage(ref damage, character.status))
        {
            Debug.Log($"Deal {damage} {damageType} Damage to {monster.Name}");
        }
    }
    void MonsterNormalAttack()
    {
        Debug.Log($"{monster.Name} use Normal Attack");
        float damage = Formula.DamageFormula(monster.status, character.status);
        if(Formula.CriticalFormula(monster.status, character.status,ref damage))
            Debug.Log("Critical");
        if (character.status.GetDamage(ref damage, character.status))
            Debug.Log($"Deal {damage} Physic Damage to Character");
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

            while (characterNextTurn >= 100 || monsterNextTurn >= 100)
            {
                if (characterNextTurn >= 100 && monsterNextTurn >= 100)
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
                    if (characterNextTurn >= 100)
                    {
                        CharacterTurn();
                        i++;
                    }

                    if (monsterNextTurn >= 100)
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
            print("Still Left :" + monster.status.currentHP);
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

    void DDPS(Status status,List<DebuffDamage> DDPS)
    {
        
        for(int i = 0; i < DDPS.Count; i++)
        {
            if (DDPS[i].turnDuration > 0)
            {
                float damage = Formula.DamageFormula(DDPS[i].userStat, status, DDPS[i].damageType, true, DDPS[i].dps, DDPS[i].penetrate, false);
                status.GetDamage(ref damage,DDPS[i].userStat,false);
                DDPS[i].turnDuration--;
                Debug.Log(DDPS[i].source.skillName + " Deal Damage " + damage + " Trun Left " + DDPS[i].turnDuration);
                
            }
            else
            {
                DDPS.Remove(DDPS[i]);
            }                 
        }
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

        character.status.currentMP = character.status.MP.Value;
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
        for (int i = 0;i< monster.dropTables.Length;i++)
        {
            StackItem dropItem = monster.dropTables[i].DropLoot();
            if (dropItem == null)
                continue;
            Inventory.instance.GetItem(dropItem.item,dropItem.amount * powerlize);               
            ResultReport.instance.AddDrop(dropItem.item, dropItem.amount * powerlize);                     
        }
        character.GetExp(monster.expReward);
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