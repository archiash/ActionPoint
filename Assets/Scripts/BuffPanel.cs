using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffPanel : MonoBehaviour
{
    [SerializeField] BuffSlot slotPrefab;
    [SerializeField] Transform parent;

    private void Start()
    {
        UpdateUI(Character.instance.status);
    }

    public void UpdateUI(Status status)
    {
        ClearUI();
        foreach (var statName in Enum.GetValues(typeof(SubStatType)))
        {
            Stat stat = (Stat)(status.GetType().GetField(statName.ToString()).GetValue(status));
            for (int i = 0; i < stat.modifiers.Count; i++)
            {
                if (stat.modifiers[i].timeType == Modifier.ModifierTime.Hunt)
                {
                    BuffSlot newSlot = Instantiate(slotPrefab,parent);
                    newSlot.Init(stat.modifiers[i], statName.ToString());
                }
            }
        }
    }

    public void ClearUI()
    {
        for(int i = 0; i < parent.childCount;i++)
        {
            Destroy(parent.GetChild(i));
        }
        parent.DetachChildren();
    }
}
