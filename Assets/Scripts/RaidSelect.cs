using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RaidSelect : MonoBehaviour,IDragHandler, IEndDragHandler
{
    public List<RaidBoss> raidList = new List<RaidBoss>();
    public CraftMaterial prefab;
    public Transform parent;
    public Button summonButton;
    public Transform summonSection;
    public Transform raidSection;

    public Button raidButton;
    public TextMeshProUGUI healthValue;
    public Image healthBar;
    //public TextMeshProUGUI usePoint;
    //public TextMeshProUGUI round;

    [SerializeField] TextMeshProUGUI raidBossName;
    [SerializeField] TextMeshProUGUI raidBossSubName;
    [SerializeField] TextMeshProUGUI swipeText;
    public int Index
    {
        get { return indexOnSelect; }
        set { indexOnSelect = value;
            Refresh();
        }
    }
    public int indexOnSelect;
    public Image image;

    public RaidManager raidManager;

    public RaidBoss currentShow
    {
        get { return raidList[Index]; }
    }

    public void OnDrag(PointerEventData data) 
    {
        
    }

    public void OnEndDrag(PointerEventData data)
    {
        if (!raidManager.isRaiding)
        {
            Debug.Log(raidList.Count);
            float difference = data.pressPosition.x - data.position.x;
            if (difference > 0 && Index < raidList.Count - 1)
                Index++;
            else if (difference < 0 && Index > 0)
                Index--;
            else if (difference < 0 && Index == 0)
                Index = raidList.Count - 1;
            else if (difference > 0 && Index == raidList.Count - 1)
                Index = 0;
        }
    }

    public void ClearSlot()
    {
        Transform[] transforms = parent.GetComponentsInChildren<Transform>();
        foreach (Transform x in transforms)
        {
            if (x.GetComponent<CraftMaterial>())
            {
                Destroy(x.gameObject);
            }
        }
    }

    public void Refresh()
    {
        if(raidManager == null) raidManager = GetComponent<RaidManager>();
        raidManager.Load();
        summonSection.gameObject.SetActive(!raidManager.isRaiding);
        raidSection.gameObject.SetActive(raidManager.isRaiding);
        if (!raidManager.isRaiding)
        {
            image.sprite = raidList[indexOnSelect].sprite;
            raidBossName.text = raidList[indexOnSelect].Name;
            raidBossSubName.text = raidList[indexOnSelect].Desc;
            swipeText.enabled = true;
            ClearSlot();

            foreach (StackItem i in raidList[Index].summonMaterial)
            {
                CraftMaterial slot = Instantiate(prefab, parent);
                slot.Init(i);
            }

            summonButton.interactable = CheckCondition();
        }
        else
        {
            ClearSlot();
            raidBossName.text = raidManager.raidBoss.Name;
            raidBossSubName.text = raidManager.raidBoss.Desc;
            image.sprite = raidManager.raidBoss.sprite; 
            //round.text = "Round " + raidManager.amountTime.ToString();
            //usePoint.text = raidManager.raidBoss.usePoint.ToString() + " Points";
            swipeText.enabled = false ;
        }
    }

    void Update()
    {
        if (raidManager == null) raidManager = GetComponent<RaidManager>();
        if (raidManager.isRaiding)
        {
            healthValue.text = $"{raidManager.currentHP}/{raidManager.raidBoss.status.HP.Value}";
            healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, raidManager.currentHP / raidManager.raidBoss.status.HP.Value, 0.5f);
            raidButton.interactable = PointManager.instance.GetActionPoint >= raidManager.raidBoss.usePoint;
        }
    }

    public bool CheckCondition()
    {
        if (Inventory.instance.items.Count < 1)
        {
            return false;
        }

        bool result = true;
        foreach (StackItem material in raidList[Index].summonMaterial)
        {
            Debug.Log("Check " + material.item.itemName);
            foreach (StackItem item in Inventory.instance.items)
            {
                Debug.Log("Check with " + item.item.itemName);

                if (item.item.ID == material.item.ID && item.amount >= material.amount)
                {
                    result = true;
                    Debug.Log("Have");
                    break;

                }
                result = false;
            }
            if (result == false)
                return false;
        }
        return true;
    }

    public void OnSummon()
    {
        raidManager.SummonRaid(raidList[Index]);

        foreach (StackItem item in raidList[Index].summonMaterial)
        {
            Inventory.instance.UseAsMaterial(item.item, item.amount);
        }
        Refresh();
    }

    public void OnRaid()
    {
        //PointManager.instance.UseAction(raidManager.raidBoss.usePoint);        
        raidManager.Raiding();
        Refresh();
    }
         
}
