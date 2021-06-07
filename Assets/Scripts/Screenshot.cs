using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
public class Screenshot : MonoBehaviour
{


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
#endif
