using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatInfluencePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] statTexts;

    public void ShowInfluence(ClassInfluence classInfluence)
    {
        gameObject.SetActive(true);
        List<StatusUpgrade.SubModify[]> statDatas =  classInfluence.subModifies;
        for(int i = 0; i < 5; i++)
        {
            StatusUpgrade.SubModify[] statData = statDatas[i];
            statTexts[i].text = "";
            foreach (StatusUpgrade.SubModify m in statData)
            {
                statTexts[i].text += $"{m.subType} +{m.value}\n";
            }
            //statTexts[i].text.Remove(statTexts[i].text.Length - 1);
        }
    }
}
