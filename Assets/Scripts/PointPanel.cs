using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointPanel : MonoBehaviour
{
    public TextMeshProUGUI point;
    public TextMeshProUGUI pointPerSec;

    // Update is called once per frame
    void Update()
    {
        point.text = ((int)PointManager.instance.GetActionPoint).ToString();
        pointPerSec.text = "+" + ((int)PointManager.instance.GetActionPerSec).ToString() + "/s";
    }

}
