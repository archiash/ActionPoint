using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Screenshot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            ScreenCapture.CaptureScreenshot(Application.dataPath + "/Scr/" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".png");
            UnityEditor.AssetDatabase.Refresh();
        }
    }
}
