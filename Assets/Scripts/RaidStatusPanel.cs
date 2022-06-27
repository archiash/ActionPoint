using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaidStatusPanel : MonoBehaviour
{
    public RaidSelect raid;

    [SerializeField] TextMeshProUGUI healthText;
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI defText;
    public TextMeshProUGUI penText;
    public TextMeshProUGUI magText;
    public TextMeshProUGUI resText;
    public TextMeshProUGUI neuText;
    public TextMeshProUGUI spdText;
    public TextMeshProUGUI hitText;
    public TextMeshProUGUI evaText;
    public TextMeshProUGUI crateText;
    public TextMeshProUGUI cdmgText;
    public TextMeshProUGUI cresText;

    public TextMeshProUGUI skillText;

    public void OnShowPanel()
    {
        RaidBoss raidBoss;
        if (raid.raidManager.isRaiding)
            raidBoss = raid.raidManager.raidBoss;
        else
            raidBoss = raid.currentShow;

        healthText.text = "พลังชีวิต: " + raidBoss.status.HP.Value;
        manaText.text = "มานา: " + raidBoss.status.MP.Value;
        atkText.text = "พลังโจมตี: " + raidBoss.status.PAtk.Value;
        defText.text = "พลังป้องกัน: " + raidBoss.status.PDef.Value;
        penText.text = "เจาะเกราะกายภาพ: " + raidBoss.status.Pen.Value;
        magText.text = "พลังเวทย์: " + raidBoss.status.MAtk.Value;
        resText.text = "ต้านเวทย์: " + raidBoss.status.MDef.Value;
        neuText.text = "เจาะเกราะเวทย์: " + raidBoss.status.Neu.Value;
        spdText.text = "ความเร็ว: " + raidBoss.status.Spd.Value;
        hitText.text = "เเม่นยำ: " + raidBoss.status.Hit.Value;
        evaText.text = "หลบหลีก: " + raidBoss.status.Eva.Value;
        crateText.text = "อัตราคริ: " + raidBoss.status.Crate.Value;
        cdmgText.text = "ดาเมจคริ: " + raidBoss.status.Cdmg.Value;
        cresText.text = "ต้านทานคริ: " + raidBoss.status.Cres.Value;
    }

    public void OnShowSkill()
    {
        RaidBoss raidBoss;
        if (raid.raidManager.isRaiding)
            raidBoss = raid.raidManager.raidBoss;
        else
            raidBoss = raid.currentShow;

        skillText.text = raidBoss.GetSkillList();

    }
}
