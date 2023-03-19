using UnityEngine;
using TMPro;

public class HexPanel : MonoBehaviour
{
    public TextMeshProUGUI hexText;

    private void Update()
    {
        hexText.text = Inventory.instance.Money.ToString();
    }
}
