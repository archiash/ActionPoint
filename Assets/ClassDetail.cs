using UnityEngine;
using TMPro;
public class ClassDetail : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI className;

    void Update()
    {
        className.text = $"Class: {ClassUtility.GetClassName(Character.instance.Class)}"; 
    }
}
