using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuffDetail : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI detail;
    [SerializeField] GameObject panel;
    
    public void ShowDetail(string detail)
    {
        panel.SetActive(true);
        this.detail.text = detail;
    }
}
