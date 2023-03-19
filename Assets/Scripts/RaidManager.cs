using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidManager : MonoBehaviour
{ 
    public RaidBoss raidBoss;
    public float currentHP;
    public int amountTime;
    Raidfield raidfield;
    RaidSelect raidSelect;

    public bool isRaiding;

    bool isLoad = false;
    public void Load()
    {
        raidfield = GetComponent<Raidfield>();
        raidSelect = GetComponent<RaidSelect>();
        RaidSaveData data = Gamemanager.instance.itemSaveManager.LoadRaid();
        if (data != null)
        {
            if (data.isRaid)
            {
                foreach(RaidBoss raidBoss in raidSelect.raidList)
                {
                    if (raidBoss.ID == data.raid)
                        this.raidBoss = raidBoss;
                }
                currentHP = data.currentHP;
                amountTime = data.round;
                isRaiding = data.isRaid;
            }else
            {
                isRaiding = false;
                raidBoss = null;
                currentHP = 0;
                amountTime = 0;
            }
        }
        isLoad = true;
    }

    public void Save()
    {
        if(isLoad)
            Gamemanager.instance.itemSaveManager.SaveRaid(raidBoss.ID, currentHP, amountTime, isRaiding);
    }

    public void SummonRaid(RaidBoss rb)
    {
        isRaiding = true;
        raidBoss = rb;
        currentHP = rb.status.currentHP;
        amountTime = 0;
        Save();
    }

    public void Raiding()
    {
        Monster raid = Instantiate(raidBoss);
        raid.status.currentHP = currentHP;
        raidfield.Raiding(raid);
        amountTime++;
    }
    public void EndRaid(float hpLeft)
    {
        Character.instance.status.RemoveInHuntModifier();

        if (hpLeft <= 0)
        {
            isRaiding = false;
            GiveReward();
            UIManager.Instance.resultReport.ShowResult("Raid Clear");
            currentHP = 0;
            Save();
            raidSelect.Refresh();
            return;        
        }
        currentHP = hpLeft;
        Save();
    }
    void GiveReward()
    {
        for (int i = 0; i < raidBoss.dropTables.Length; i++)
        {
            StackItem dropItem = raidBoss.dropTables[i].DropLoot();
            if (dropItem == null)
                continue;
            Inventory.instance.GetItem(dropItem.item, dropItem.amount);
            UIManager.Instance.resultReport.AddDrop(dropItem.item, dropItem.amount);
        }
        Character.instance.GetExp(raidBoss.expReward);
    }

}
