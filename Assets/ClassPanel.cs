using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClassPanel : MonoBehaviour
{
    public Character.CharacterClass changedClass;
    public ClassChangeButton currentButton;

    [SerializeField] ClassChangeComfirm classChangeComfirm;

    [SerializeField] Button[] classButton;

    [SerializeField] TextMeshProUGUI currentClassText;
    [SerializeField] TextMeshProUGUI currentClassDetailText;

    void UpdatePanel()
    {
        (string className,string classDetail) = ClassUtility.GetClassNameAndDetail(Character.instance.Class);

        currentClassText.text = $"ปัจจุบัน: {className}";
        currentClassDetailText.text = $"{classDetail}";
        currentClassDetailText.fontSize = currentClassText.fontSize;

        currentButton = GameObject.Find(Character.instance.Class.ToString()).GetComponent<ClassChangeButton>();
        currentButton.ButtonPanel.color = Color.gray;

        foreach (Button button in classButton)
        {
            button.interactable = Inventory.instance.Money >= 300 && Character.instance.Class != button.GetComponent<ClassChangeButton>().characterClass;
        }      

    }

    public void SelectedChangeClass(Character.CharacterClass characterClass,ClassChangeButton button)
    {
        changedClass = characterClass;
        if (currentButton != null) currentButton.ResetSelected();
        currentButton = button;
        ChangeClass(characterClass);
    }

    public void OnPanelOpen()
    {
        this.gameObject.SetActive(true);
        UpdatePanel();
    }

    public void OpenClassChangeComfirm(Character.CharacterClass characterClass)
    {
        (string className, string classDetail) = ClassUtility.GetClassNameAndDetail(characterClass);
        classChangeComfirm.ShowComfirmDetail(characterClass,className,classDetail);
    }


    public void ChangeClass(Character.CharacterClass characterClass)
    {
        Inventory.instance.UseMoney(300);
        Character.instance.Class = characterClass;
        UpdatePanel();
    }
}
