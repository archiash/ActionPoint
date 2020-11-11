using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_MenuBar : MonoBehaviour
{
    public Color color;
    [SerializeField] Image expBar;

    private void Update()
    {
        expBar.fillAmount = Mathf.Lerp(expBar.fillAmount,Character.instance.exp / Mathf.RoundToInt(30 * Mathf.Pow(Character.instance.Level, 2) + (50 * (Character.instance.Level - 1))), Time.deltaTime * .5f);
    }

    public void ChangeToScene(int index)
    {
        Gamemanager.instance.ChangeToScene(index);

    }

}
