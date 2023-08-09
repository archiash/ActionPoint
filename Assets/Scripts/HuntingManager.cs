using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;
using UnityEngine.Events;

public delegate void WinEvent();

public class HuntingManager : MonoBehaviour
{
    public bool raiding = false;

    public WinEvent winEvent;

    public List<Follower> playerFollower;
    public List<Follower> enermyFollower;

    public Image playerCharacterImage;
    public Animator playerCharacterAnimator;
    public Image[] playerFollowerImage;
    public Animator[] playerFollowerAnimators;

    public Image enermyCharacterImage;
    public Animator enermyCharacterAnimator;
    public Image[] enermyFollowerImage;

    public int[] playerFollowerSkillTimer;
    public int[] enermyFollowerSkillTimer;

    public Image playerHealthBar;
    public Image enermyHealthBar;

    public float timeScale;
    int STEP_PER_SECOND = 4; 

    Character character;
    Monster monster;

    public Monster testMonster;

    public List<string> huntReport = new List<string>();

    float characterSpeed;
    float monsterSpeed;

    float characterNextTurn = 0f;
    float monsterNextTurn = 0f;

    public List<DebuffDamage> characterDDPS = new List<DebuffDamage>();
    public List<DebuffDamage> characterDDPT = new List<DebuffDamage>();
    public List<DebuffDamage> monsterDDPS = new List<DebuffDamage>();
    public List<DebuffDamage> monsterDDPT = new List<DebuffDamage>();

    public int characterStun;
    public int monsterStun;

    private int powerlize;
    public void Setup(Monster _monster, List<Follower> playerFollower, List<Follower> enermyFollower = null, int powerlize = 1, bool fromTranslation = false)
    {
#if UNITY_EDITOR
        timeScale = 100;
#endif

        this.powerlize = powerlize;

        character = Character.instance;

        if (_monster == null)
            monster = Instantiate(testMonster);
        else
            monster = Instantiate(_monster);

        enermyCharacterImage.sprite = monster.sprite;
        if(fromTranslation) enermyCharacterImage.enabled = false;

        character.status.currentHP = character.status.HP.Value;
        character.status.currentMP = character.status.MP.Value;

        for (int i = 0; i < 3; i++)
        {
            if (i < playerFollower.Count)
            {
                if (playerFollower[i] != null)
                {
                    this.playerFollower.Add(playerFollower[i]);
                    playerFollowerImage[i].sprite = playerFollower[i].followerSquareImage;
                    playerFollowerImage[i].enabled = true;
                }
            }
            else playerFollowerImage[i].enabled = false;

            if (i < ((enermyFollower == null) ? 0 : enermyFollower.Count))
            {
                this.enermyFollower.Add(enermyFollower[i]);
                enermyFollowerImage[i].sprite = enermyFollower[i].followerSquareImage;
                enermyFollowerImage[i].enabled = true;
            }
            else enermyFollowerImage[i].enabled = false;
            
        }

        playerFollowerSkillTimer = new int[playerFollower.Count];
        enermyFollowerSkillTimer = new int[enermyFollower == null ? 0 : enermyFollower.Count];

        monsterNextTurn = 0f;
        characterNextTurn = 0f;

        characterCooldown = new int[character.currentSkill.Count];
        monsterCooldown = new int[monster.currentSkill.Count];

        character.status.currentMP = character.status.MP.Value;

        characterStun = 0;
        monsterStun = 0;

        characterDDPS.Clear();
        characterDDPT.Clear();
        monsterDDPS.Clear();
        monsterDDPT.Clear();

        monster.SetSkill();

        monster.status.stacks = new Dictionary<string, Stack>();
        character.status.stacks = new Dictionary<string, Stack>();
    }

    void CalculateSpeed()
    {
        //float highestValue = Mathf.Max(character.status.Spd.Value, monster.status.Spd.Value);
        characterSpeed = Formula.ActionPerStep(character.status.Spd.Value);
        monsterSpeed = Formula.ActionPerStep(monster.status.Spd.Value);
    }

    int[] characterCooldown;
    int[] monsterCooldown;

    void CharacterStep()
    {
        if (!character.status.isFullMP)
            character.status.currentMP += 0.5f + (character.level - 1) * 0.1f;
    }
    void MonsterStep()
    {
        if (!monster.status.isFullMP)
            monster.status.currentMP += Mathf.Pow(Mathf.Log(monster.status.MP.Value),1.5f) / 1.5f;
    }
    void CharacterTurn()
    {
        playerCharacterAnimator.SetTrigger("Attack");
        bool isUsedSkill = false;
        characterNextTurn -= STEP_PER_SECOND;
        print("Character Turn");
        if (character.status.currentHP <= 0)
            return;

        DDPT(character.status, characterDDPS);
        ReduceBuffTurn(character.status);

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
    void MonsterTurn()
    {
        enermyCharacterAnimator.SetTrigger("Attack");
        bool isUsedSkill = false;
        monsterNextTurn -= STEP_PER_SECOND;
        print("Monster Turn");
        if (monster.status.currentHP <= 0)
            return;

        DDPT(monster.status, monsterDDPS);
        ReduceBuffTurn(monster.status);

        if (monster.currentSkill.Count > 0)
        {
            for (int i = 0; i < monster.currentSkill.Count; i++)
            {
                if (monsterCooldown[i] <= 0 && monster.status.currentMP >= monster.currentSkill[i].manaCost)
                {
                    if (monster.currentSkill[i] is CounterSkill || monster.currentSkill[i] is OnGetHitEffectData)
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
    void CharacterNormalAttack()
    {
        (float pureDamage, DamageType damageType) = character.GetDamageAttack();
        Debug.Log("Character use Normal Attack");
        float damage = Formula.DamageFormula(character.status, monster.status, damageType,true,pureDamage,0,true);
        if (Formula.CriticalFormula(character.status, monster.status, ref damage))
            Debug.Log("Critical");
        AttackData attackData = new AttackData(character.status, damage);
        if (monster.status.GetDamage(ref attackData))
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
        AttackData attackData = new AttackData(monster.status, damage);
        if (character.status.GetDamage(ref attackData))
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
    public IEnumerator Hunt()
    {        
        int turnCount = 5000;
        int endturn = 0;
        int stepCount = 0;
        playerHealthBar.fillAmount = 1;
        if (raiding) enermyHealthBar.fillAmount = monster.status.currentHP / monster.status.HP.Value;
        else enermyHealthBar.fillAmount = 1;
        StartCoroutine(HealthBarUpdate());
        for(int i = 0; i < turnCount;)
        {
            stepCount++;
            print("Step: " + stepCount);
            CalculateSpeed();

            if(characterStun <= 0) characterNextTurn += characterSpeed;
            else
            {
                characterStun--;
                Debug.Log("STUN Step Left" + characterStun);              
            }
            if(monsterStun <= 0) monsterNextTurn += monsterSpeed;
            else
            {
                monsterStun--;
                Debug.Log("STUN Step Left" + monsterStun);
            }

            for (int z = 0; z < monster.currentSkill.Count; z++)
            {
                monsterCooldown[z] = monsterCooldown[z] > 0 ? monsterCooldown[z] - 1 : monsterCooldown[z];
            }

            for (int z = 0; z < character.currentSkill.Count; z++)
            {
                characterCooldown[z] = characterCooldown[z] > 0 ? characterCooldown[z] - 1 : characterCooldown[z];
            }


            CharacterStep();
            MonsterStep();

            ReduceBuffStep(character.status);
            ReduceBuffStep(monster.status);
            DDPS(character.status, characterDDPS);
            DDPS(monster.status, monsterDDPS);

            for (int f = 0; f < 3; f++)
            {
                if (f < playerFollowerSkillTimer.Length)
                {
                    playerFollowerSkillTimer[f] += 1;
                }
                if (f < enermyFollowerSkillTimer.Length)
                {
                    enermyFollowerSkillTimer[f] += 1;
                }
            }

            while (characterNextTurn >= STEP_PER_SECOND || monsterNextTurn >= STEP_PER_SECOND)
            {
                if (characterNextTurn >= STEP_PER_SECOND && monsterNextTurn >= STEP_PER_SECOND)
                {
                    if (characterNextTurn > monsterNextTurn)
                    {
                        CharacterTurn();
                        //yield return new WaitForSeconds(0.1f);
                        i++;
                    }else
                    if (characterNextTurn < monsterNextTurn)
                    {
                        MonsterTurn();
                        //yield return new WaitForSeconds(0.1f);
                        i++;
                    }else
                    if (characterNextTurn == monsterNextTurn)
                    {
                        if (character.status.Spd.Value < monster.status.Spd.Value)
                        {
                            MonsterTurn();
                            //yield return new WaitForSeconds(0.1f);
                            i++;
                        }
                        else
                        {
                            CharacterTurn();
                            //yield return new WaitForSeconds(0.1f);
                            i++;
                        }
                    }
                }
                else
                {
                    if (characterNextTurn >= STEP_PER_SECOND)
                    {
                        CharacterTurn();
                        //yield return new WaitForSeconds(0.1f);
                        i++;
                    }

                    if (monsterNextTurn >= STEP_PER_SECOND)
                    {
                        MonsterTurn();
                        //yield return new WaitForSeconds(0.1f);
                        i++;
                    }
                }

                
            }

            for (int f = 0; f < 3; f++)
            {
                //print(f);
                if (f < playerFollowerSkillTimer.Length)
                {
                    //print("PlayerFollower");
                    if (playerFollowerSkillTimer[f] >= playerFollower[f].folowerSkills[0].interval)
                    {
                        Debug.Log("Tareus!!!!");
                        playerFollower[f].folowerSkills[0].skill.Use(character, monster);
                        playerFollowerSkillTimer[f] = 0;
                        playerFollowerAnimators[f].SetTrigger("Attack");
                        //yield return new WaitForSeconds(0.1f);
                    }
                }

                if (f < enermyFollowerSkillTimer.Length)
                {
                    //print("EnermyFollower");
                    if (enermyFollowerSkillTimer[f] >= enermyFollower[f].folowerSkills[0].interval)
                    {
                        enermyFollower[f].folowerSkills[0].skill.Use(monster, character);
                        enermyFollowerSkillTimer[f] = 0;
                        //yield return new WaitForSeconds(0.1f);
                    }
                }
            }

            

            if (CheckResult(i))
            {
                endturn = i;
                i = 500;
                break;
            }

            yield return new WaitForSecondsRealtime(1.0f / STEP_PER_SECOND / timeScale);
        }
        EndBattle();
        Destroy(monster);
        Debug.Log(endturn);
    }

    IEnumerator HealthBarUpdate()
    {
        while (playerHealthBar.fillAmount > 0.0001f && enermyHealthBar.fillAmount > 0.0001f)
        {
            playerHealthBar.fillAmount = Mathf.Lerp(playerHealthBar.fillAmount, character.status.currentHP / character.status.HP.Value, 0.05f);
            enermyHealthBar.fillAmount = Mathf.Lerp(enermyHealthBar.fillAmount, monster.status.currentHP / monster.status.HP.Value, 0.05f);
            yield return new WaitForSeconds(0.01f);
        }
    }
   
    public bool CheckResult(int turn)
    {
        if(playerHealthBar.fillAmount < 0.001f)
        {
            print("Lose");
            print("Still Left :" + monster.status.currentHP);
            if (!raiding) UIManager.Instance.resultReport.ShowResult("เเพ้");
            if (raiding) gameObject.SetActive(false);
            return true;

        }
        else if (enermyHealthBar.fillAmount < 0.001f)
        {
            print("Win");
            if (!raiding) GiveReward();
            if (!raiding) UIManager.Instance.resultReport.ShowResult("ชนะ");
            if (raiding) gameObject.SetActive(false);
            return true;
        }else if(turn == 499)
        {
            print("Time Up");
            if (!raiding) UIManager.Instance.resultReport.ShowResult("เสมอ");
            if (raiding) gameObject.SetActive(false);
            return true;
        }

        return false;
    }

    void DDPT(Status status,List<DebuffDamage> DDPT)
    {
        
        for(int i = 0; i < DDPT.Count; i++)
        {
            if (DDPT[i].turnDuration > 0)
            {
                float damage = Formula.DamageFormula(DDPT[i].userStat, status, DDPT[i].damageType, true, DDPT[i].dps, DDPT[i].penetrate, false);
                AttackData attackData = new AttackData(DDPT[i].userStat, damage);
                status.GetDamage(ref attackData, false);
                DDPT[i].turnDuration--;
                Debug.Log(DDPT[i].source.skillName + " Deal Damage " + damage + " Trun Left " + DDPT[i].turnDuration);
                
            }
            else
            {
                DDPT.Remove(DDPT[i]);
            }                 
        }
    }

    void DDPS(Status status, List<DebuffDamage> DDPS)
    {

        for (int i = 0; i < DDPS.Count; i++)
        {
            if (DDPS[i].turnDuration > 0)
            {
                float damage = Formula.DamageFormula(DDPS[i].userStat, status, DDPS[i].damageType, true, DDPS[i].dps, DDPS[i].penetrate, false);
                AttackData attackData = new AttackData(DDPS[i].userStat, damage);
                status.GetDamage(ref attackData, false);
                DDPS[i].turnDuration--;
                Debug.Log(DDPS[i].source.skillName + " Deal Damage " + damage + " Step Left " + DDPS[i].turnDuration);

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
                if (stat.modifiers[i].timeType == Modifier.ModifierTime.Turn || stat.modifiers[i].timeType == Modifier.ModifierTime.Step)
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
        character.status.currentMP = character.status.MP.Value;
        if (raiding) UIManager.Instance.raidManager.EndRaid(monster.status.currentHP);
        //Debug.Log(previousHp - character.status.currentHP);
        //character.status.currentHP = character.status.HP.Value;
        RemoveInHuntBuff(character.status);
        ReduceHuntBuff(character.status);
    }

    public void StartBattle(bool raiding = false)
    {
        this.raiding = raiding;      
        gameObject.SetActive(true);
        StartCoroutine(Hunt());
        enermyCharacterImage.enabled = true;
    }

    public void StartHunt()
    {        
        Setup(null,new List<Follower> {FollowerTeam.instance.follower1});
        StartCoroutine(Hunt());       
    }

    void GiveReward()
    {
        Debug.Log("Before " + winEvent.Method);
        winEvent();
        winEvent = null;
        Debug.Log("After " + winEvent?.Method);
        for (int i = 0;i< monster.dropTables.Length;i++)
        {
            StackItem dropItem = monster.dropTables[i].DropLoot();
            if (dropItem == null)
                continue;
            Inventory.instance.GetItem(dropItem.item,dropItem.amount * powerlize);
            UIManager.Instance.resultReport.AddDrop(dropItem.item, dropItem.amount * powerlize);                     
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
                t.StartHunt();
            }
    }
}
#endif