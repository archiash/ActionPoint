using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ClassChangeComfirm : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI classNameText;
    [SerializeField] TextMeshProUGUI classDetailText;
    [SerializeField] ClassPanel classPanel;

    Character.CharacterClass selectedClass;

    public void ShowComfirmDetail(Character.CharacterClass class2change,string className,string detail)
    {
        gameObject.SetActive(true);
        classNameText.text = $"เปลี่ยนคลาสเป็น: {className}";
        classDetailText.text = detail;
        selectedClass = class2change;

    }

    public void OnConfirmButton()
    {
        classPanel.ChangeClass(selectedClass);
        gameObject.SetActive(false);
    }
}
