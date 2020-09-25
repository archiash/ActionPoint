using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MenuBar : MonoBehaviour
{
    public Color color;
    
    public void ChangeToScene(int index)
    {
        Gamemanager.instance.ChangeToScene(index);

    }

}
