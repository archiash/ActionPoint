using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassChangeButton : MonoBehaviour
{
    public Character.CharacterClass characterClass;
    [SerializeField] ClassPanel classPanel;
    public void OnChangeClassButton()
    {
        classPanel.OpenClassChangeComfirm(characterClass);
    }
}
