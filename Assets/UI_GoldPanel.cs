using UnityEngine;
using TMPro;
public class UI_GoldPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI value;

    // Update is called once per frame
    void Update()
    {
        value.text = Inventory.instance.Money.ToString();
    }
}
