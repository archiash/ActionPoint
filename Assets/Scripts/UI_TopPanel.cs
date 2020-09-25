using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_TopPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI storageText;
    [SerializeField] TextMeshProUGUI pointText;
    [SerializeField] TextMeshProUGUI ppsText;

    private void Update()
    {
        levelText.text = Character.instance.Level.ToString();
        moneyText.text = "$ " + ((int)Inventory.instance.getMoney).ToString();
        pointText.text = "Point: " + ((int)PointManager.instance.GetActionPoint).ToString();
        ppsText.text = string.Format($"+{ PointManager.instance.GetActionPerSec}/วิ");
        storageText.text = string.Format($"คลัง: {Inventory.instance.getStorageCurr}/{Inventory.instance.getStorageSize}");
    }
}
