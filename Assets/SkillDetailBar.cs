using TMPro;
using UnityEngine;

public class SkillDetailBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI skillName;
    [SerializeField] TextMeshProUGUI skillDesc;

    [SerializeField] TextMeshProUGUI skillCooldown;

    public void ShowSkillDetail(Skill skill)
    {
        if(skill == null)
        {
            gameObject.SetActive(false);
            return ;
        }

        gameObject.SetActive(true);
        skillName.text = skill.skillName;
        skillDesc.text = skill.skillDesc;
        skillCooldown.text = skill.coolTime + " steps";
    }
}
