using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RaidSelect : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public List<RaidBoss> raidList = new List<RaidBoss>();
    public CraftBar_Material prefab;
    public Transform parent;
    public Button summonButton;
    public Transform summonSection;
    public Transform raidSection;

    public Button raidButton;
    public TextMeshProUGUI healthValue;
    public Image healthBar;
    public TextMeshProUGUI usePoint;
    public TextMeshProUGUI round;
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

    public RaidBoss currentShow{
    get{ return raidList[Index];}    

    }

    private void Start()
    {
        raidManager = GetComponent<RaidManager>();
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
        }
    }

    public void ClearSlot()
    {
        Transform[] transforms = parent.GetComponentsInChildren<Transform>();
        foreach (Transform x in transforms)
        {
            if (x.GetComponent<CraftBar_Material>())
            {
                Destroy(x.gameObject);
            }
        }
    }

    public void Refresh()
    {
        summonSection.gameObject.SetActive(!raidManager.isRaiding);
        raidSection.gameObject.SetActive(raidManager.isRaiding);
        if (!raidManager.isRaiding)
        {
            image.sprite = raidList[indexOnSelect].sprite;

            ClearSlot();

            foreach (StackItem i in raidList[Index].summonMaterial)
            {
                CraftBar_Material slot = Instantiate(prefab, parent);
                slot.Init(i);
            }

            summonButton.interactable = CheckCondition();
        } else
        {
            ClearSlot();
            image.sprite = raidManager.raidBoss.sprite;
            healthValue.text = raidManager.currentHP.ToString();
            round.text = "Round " + raidManager.amountTime.ToString();
            usePoint.text = raidManager.raidBoss.usePoint.ToString() + " Points";
            
        }
    }

    void Update()
    {
        if (raidManager.isRaiding)
        {
            healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, raidManager.currentHP / raidManager.raidBoss.status.HP.Value, Time.deltaTime);
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
        PointManager.instance.UseAction(raidManager.raidBoss.usePoint);        
        raidManager.Raiding();
        Refresh();
    }
         
}
