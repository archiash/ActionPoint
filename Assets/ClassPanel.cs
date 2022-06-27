using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClassPanel : MonoBehaviour
{
    [SerializeField] ClassChangeComfirm classChangeComfirm;

    [SerializeField] Button[] classButton;

    [SerializeField] TextMeshProUGUI currentClassText;
    [SerializeField] TextMeshProUGUI currentClassDetailText;
    void UpdatePanel()
    {
        (string className,string classDetail) = GetClassNameAndDetail(Character.instance.Class);

        currentClassText.text = $"�Ѩ�غѹ: {className}";
        currentClassDetailText.text = $"{classDetail}";

        foreach(Button button in classButton)
        {
            button.interactable = Inventory.instance.getMoney >= 300 && Character.instance.Class != button.GetComponent<ClassChangeButton>().characterClass;
        }      

    }

    public (string,string) GetClassNameAndDetail(Character.CharacterClass characterClass)
    {
        string className = "";
        string classDetail = "";
        switch (characterClass)
        {
            case Character.CharacterClass.Adventurer:
                className = "�ѡ������";
                classDetail = "���մ��¾�ѧ���� 100% �繤���������¡���Ҿ\n" +
                    "���͡�� 10% ������������ҡ������������� 1 ���";
                break;
            case Character.CharacterClass.Magician:
                className = "�ѡ�Ƿ��";
                classDetail = "���Ѻ INT 5 ˹��� + 0.5 ˹��µ�������" +
                    "\n������ը����ҹ� 10 ˹��� (�ҹҨп�鹿�㹡�õ����� 5% �ͧ�ҹ��٧�ش�������) ���մ��¾�ѧ�Ƿ�� 100% �繤�����������Ƿ�� ����ҡ�ҹ������§�ͨ����մ��¾�ѧ����" +
                    " 50% �繤���������¡���Ҿ᷹";
                break;
            case Character.CharacterClass.Rogue:
                className = "(���)��";
                classDetail = "���մ��¾�ѧ���� 75% + �������� 35% �繤���������¡���Ҿ\n" +
                    "�����͡��㹡���ź������� 10%\n" +
                    "������Ѻ������������������ 20%";
                break;
        }
        return (className, classDetail);
    }

    public void OnPanelOpen()
    {
        this.gameObject.SetActive(true);
        UpdatePanel();
    }

    public void OpenClassChangeComfirm(Character.CharacterClass characterClass)
    {
        (string className, string classDetail) = GetClassNameAndDetail(characterClass);
        classChangeComfirm.ShowComfirmDetail(characterClass,className,classDetail);
    }


    public void ChangeClass(Character.CharacterClass characterClass)
    {
        Inventory.instance.UseMoney(300);
        Character.instance.Class = characterClass;
        UpdatePanel();
    }
}
