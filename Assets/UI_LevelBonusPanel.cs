using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_LevelBonusPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    public void ShowText(string _text)
    {
        Transform a = Camera.main.transform;
        Transform b = a.GetComponentInChildren<Canvas>().transform;
        transform.parent = b;
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        text.text = _text;  
    }

    public void OnClose()
    {
        Destroy(this.gameObject);
    }

}
