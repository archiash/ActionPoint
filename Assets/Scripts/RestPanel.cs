using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RestPanel : MonoBehaviour
{
    [SerializeField] bool onQuickRest;

    //public TextMeshProUGUI levelText;
    public TextMeshProUGUI costText;
    //public TextMeshProUGUI descText;

    float cost;

    [SerializeField] TextMeshProUGUI detailText;
    [SerializeField] TextMeshProUGUI levelText;

    [SerializeField] Button button;

    [SerializeField] Button upgradeButton;
    [SerializeField] TextMeshProUGUI upgradeDetailText;

    [SerializeField] Item itemToUpgrade;

    Character character;
    PointManager pointManager;
    private void Start()
    {
        character = Character.instance;
        pointManager = PointManager.instance;
    }

    private void Update()
    {
        float perPointCost = 2f - (pointManager.restLevel - 1f) / 2f;

        cost = Mathf.RoundToInt(character.status.HP.Value - character.status.currentHP) * perPointCost;
        if (!onQuickRest)
        {
            detailText.text = $"ใช้ Point ในการฟื้นฟูพลังชีวิต {perPointCost} Point ต่อ พลังชีวิต 1 หน่วย";
            levelText.text = $"ระดับ {pointManager.restLevel}";
        }
        else
        {
            levelText.text = $"Level {pointManager.restLevel}";
        }
        if (!onQuickRest)
            costText.text = "จ่าย " + cost + " Point";
        else
            costText.text = $"ใช้ {cost} Point เพื่อฟื้นฟูพลังชีวิตจนเต็ม";

        if (!CheckCondition() || Character.instance.isFullHP)
        {
            button.interactable = false;
        }else
        {
            button.interactable = true;
        }

        if(!onQuickRest && CheckUpgradeCondition() && pointManager.restLevel < 2)
        {
            upgradeButton.gameObject.SetActive(true);
            upgradeDetailText.gameObject.SetActive(true);
        }else if(!onQuickRest)
        {
            
            upgradeButton.gameObject.SetActive(false);
            upgradeDetailText.gameObject.SetActive(false);
        }
    }

    public void OnRestButton()
    {
        if(CheckCondition())
        {
            pointManager.UseAction(cost);
            character.status.currentHP = character.status.HP.Value;
            if(!onQuickRest)
                gameObject.SetActive(false);
        }
    }

    bool CheckCondition()
    {
        if (pointManager.GetActionPoint >= cost)
            return true;

        return false;
    }


    public void OnUpgradeButton()
    {
        Inventory.instance.UseAsMaterial(itemToUpgrade,1);
        pointManager.restLevel++;
    }

    public bool CheckUpgradeCondition()
    {
        if (Inventory.instance.items.Count < 1)
        {
            return false;
        }

        bool result = true;
            foreach (StackItem item in Inventory.instance.items)
            {
                if (item.item.ID == itemToUpgrade.ID)
                {
                    result = true;
                    break;

                }
                result = false;
            }
            if (result == false)
                return false;
        
        return true;
    }
}
