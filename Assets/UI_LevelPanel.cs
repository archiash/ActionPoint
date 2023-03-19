using UnityEngine;
using TMPro;
public class UI_LevelPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI level; 

    // Update is called once per frame
    void Update()
    {
        level.text = Character.instance.Level.ToString();
    }
}
