using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkillUI : MonoBehaviour
{
    public TextMeshProUGUI upgradePoint;

    private void Update()
    {

    }

    public void BackButton()
    {
        SceneManager.LoadScene("Main");
    }
}
