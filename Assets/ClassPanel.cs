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

        currentClassText.text = $"ปัจจุบัน: {className}";
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
                className = "นักผจญภัย";
                classDetail = "โจมตีด้วยพลังโจมตี 100% เป็นความเสียหายกายภาพ\n" +
                    "มีโอกาส 10% ที่จะได้ไอเท็มจากการล่าเพิ่มขึ้น 1 ชิ้น";
                break;
            case Character.CharacterClass.Magician:
                className = "นักเวทย์";
                classDetail = "ได้รับ INT 5 หน่วย + 0.5 หน่วยต่อเลเวล" +
                    "\nการโจมตีจะใช้มานา 10 หน่วย (มานาจะฟื้นฟูในการต่อสู้ 5% ของมานาสูงสุดต่อเทิร์น) โจมตีด้วยพลังเวทย์ 100% เป็นความเสียหายเวทย์ ถ้าหากมานาไม่เพียงพอจะโจมตีด้วยพลังโจมตี" +
                    " 50% เป็นความเสียหายกายภาพแทน";
                break;
            case Character.CharacterClass.Rogue:
                className = "(จอม)โจร";
                classDetail = "โจมตีด้วยพลังโจมตี 75% + ความเร็ว 35% เป็นความเสียหายกายภาพ\n" +
                    "เพิ่มโอกาสในการหลบการโจมตี 10%\n" +
                    "แต่จะได้รับความเสียหายเพิ่มขึ้น 20%";
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
