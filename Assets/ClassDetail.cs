using UnityEngine;
using TMPro;
public class ClassDetail : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI className;

    void Update()
    {
        className.text = $"�Ҫվ: {ClassUtility.GetClassName(Character.instance.Class)}"; 
    }
}
