using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusGraph : MonoBehaviour
{
    // Arr Order by Sub Stat Enum Value
    [SerializeField] Image[] playerStatBars;
    [SerializeField] Image[] enermyStatBars;
    [SerializeField] TextMeshProUGUI[] statvalues;

    [SerializeField] Monster monster2Test;

    [SerializeField] Statistic statistic;

    public void UpdateGraph(Status player, Status enermy)
    {
        statistic.Recalculate();
        for(int i = 0; i < playerStatBars.Length; i++)
        {
            float playerValue = player.GetStat((SubStatType)i).Value;
            float enermyValue = enermy.GetStat((SubStatType)i).Value;
            //float maxValue = Mathf.Max(playerValue, enermyValue) * 1.5f;

            //float playerScale = playerValue / maxValue;
            //float enermyScale = enermyValue / maxValue;

            playerStatBars[i].fillAmount = statistic.TScore(playerValue, i) / statistic.MaxTScore(i);
            enermyStatBars[i].fillAmount = statistic.TScore(enermyValue, i) / statistic.MaxTScore(i);
            statvalues[i].text = $"<color=#FFFFFF>{playerValue}</color>" + " " + $"<color=#000000>{enermyValue}</color>";
        }
    }

}
