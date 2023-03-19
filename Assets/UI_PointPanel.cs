using UnityEngine;
using TMPro;
public class UI_PointPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI value;

    // Update is called once per frame
    void Update()
    {
        value.text = PointManager.instance.Point.ToString();
    }
}
