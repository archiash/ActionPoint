using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultReport : MonoBehaviour
{
    public static ResultReport instance;
    private void Start()
    {
        if (instance == null)
            instance = this;
    }

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    public List<StackItem> droplist = new List<StackItem>();
    public GameObject pf_dropslot;
    public Transform parent;

    public void AddDrop(Item newItem, int newAmount)
    {
        for(int i = 0; i <droplist.Count;i++)
        {
            if(newItem.ID == droplist[i].item.ID)
            {
                droplist[i].amount += newAmount;
                return;
            }
        }
        StackItem newSLot = new StackItem(newItem,newAmount);
        droplist.Add(newSLot);
    }

    public void UpdateDropList()
    {
        ClearList();
        if(droplist != null)
        foreach(StackItem drop in droplist)
        {
            GameObject slot = Instantiate(pf_dropslot, parent);
            slot.GetComponent<DropItemSlot>().OnCreate(drop);
        }
        droplist.Clear();
    }

    public void ClearList()
    {
        
        Transform[] transforms = parent.GetComponentsInChildren<Transform>();

        foreach (Transform x in transforms)
        {
            if (x.GetComponent<DropItemSlot>())
            {
                Destroy(x.gameObject);
            }
        }
    }

    public void ShowResult(string result)
    {
        UpdateDropList();
        resultText.text = result;
        resultPanel.SetActive(true);
    }

    public void BackButton()
    {
        resultPanel.SetActive(false);
        if(Character.instance.status.currentHP <= 0)
        {
            Gamemanager.instance.ChangeToScene(0);
        }
    }
}
