using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftBar_Material : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI amount;

    public void Init(StackItem item)
    {
        icon.sprite = item.item.icon;
        amount.text = item.amount.ToString();
    }
}
