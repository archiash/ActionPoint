using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TImeDebug : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

#if UNITY_ANDROID
    // Start is called before the first frame update
    void Start()
    {
        AndroidJavaClass androidClass = new AndroidJavaClass("com.archiash.datetime.AndroidDateTime");
        text.text = androidClass.Call<string>("timeZoneID");
        text.text += " " + androidClass.Call<string>("opera");
    }
#endif

}
