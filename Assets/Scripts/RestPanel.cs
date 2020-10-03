using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RestPanel : MonoBehaviour
{
    [SerializeField] bool onQuickRest;

    public int level;
    //public TextMeshProUGUI levelText;
    public TextMeshProUGUI costText;
    //public TextMeshProUGUI descText;

    int cost;

    [SerializeField] Button button;

    Character character;
    PointManager pointManager;
    private void Start()
    {
        character = Character.instance;
        pointManager = PointManager.instance;
    }

    private void Update()
    {
        cost = Mathf.RoundToInt(character.status.HP.Value - character.status.currentHP) * 2;

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
}
