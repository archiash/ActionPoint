using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatteredTimeStage : MonoBehaviour
{
    public string stageName;
    public string stageDescription;
    public string stageID;

    public Follower stageFollower;

    public StackItem[] materials;

    public Monster stageEnermy;
    [SerializeField] Transform materialsContent;
    [SerializeField] CraftMaterial materialSlot;

    [SerializeField] GameObject unlockDetail;
    [SerializeField] GameObject unlockedName;

    public bool isStageUnlocked;
    
    void ClearMaterialsContent()
    {
        foreach (Transform x in materialsContent)
        {
            if (x.GetComponent<CraftMaterial>())
            {
                Destroy(x.gameObject);
            }
        }
    }

    public void RefreshCard()
    {
        unlockDetail.SetActive(!isStageUnlocked);
        unlockedName.SetActive(isStageUnlocked);

        if (!isStageUnlocked)
        {
            if (materialsContent != null && materials.Length > 0)
            {
                ClearMaterialsContent();
                foreach (StackItem material in materials)
                {
                    CraftMaterial craftMaterial = Instantiate(materialSlot, materialsContent);
                    craftMaterial.Init(material);
                }
            }
        }

    }

    public void ShowThisStage(GameObject scatteredTimeMenu)
    {
        if(isStageUnlocked)
            scatteredTimeMenu.GetComponent<StageDetail>().ShowStageDetail(this);
        else
        {
            UIManager.Instance.timeStageManager.ShowUnlockStageMenu(this);
        }
    }

    public bool isUnlockable()
    {
        if (Inventory.instance.items.Count < 1)
        {
            return false;
        }

        bool result = true;
        foreach (StackItem material in materials)
        {
            foreach (StackItem item in Inventory.instance.items)
            {
                if (item.item.ID == material.item.ID && item.amount >= material.amount)
                {
                    result = true;
                    break;
                }
                result = false;
            }
            if (result == false)
                return false;
        }
        return true;
    }
}
