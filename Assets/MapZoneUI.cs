using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MapZoneUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI zoneName;
    
    public void Init(Map map,int i )
    {
        zoneName.text = map.difficult + " | Level: " + map.level;
        GetComponentInChildren<Image>().color = RarityColor.MapZoneColor(i);
    }
}
