using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneManager : MonoBehaviour
{
    public Animator animator;
    public int levelToLoad;

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void FadeToLevel(int sceneIndex)
    {
        levelToLoad = sceneIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void OnStartButton(int sceneIndex)
    {
        FadeToLevel(sceneIndex);
    }
}
