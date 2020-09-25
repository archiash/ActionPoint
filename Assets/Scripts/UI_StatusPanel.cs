using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_StatusPanel : MonoBehaviour
{
    Status status;

    public TextMeshProUGUI atk;
    public TextMeshProUGUI def;
    public TextMeshProUGUI mp;
    public TextMeshProUGUI mag;
    public TextMeshProUGUI res;
    public TextMeshProUGUI spd;
    public TextMeshProUGUI eva;
    public TextMeshProUGUI hit;
    public TextMeshProUGUI crate;
    public TextMeshProUGUI cdmg;

    void Start()
    {
        status = Character.instance.status;
    }

    void Update()
    {
        atk.text = ((float)Math.Round(status.PAtk.Value * 100f) / 100f).ToString();
        def.text = ((float)Math.Round(status.PDef.Value * 100f) / 100f).ToString();
        mp.text = ((float)Math.Round(status.MP.Value * 100f) / 100f).ToString();
        mag.text = ((float)Math.Round(status.MAtk.Value * 100f) / 100f).ToString();
        res.text = ((float)Math.Round(status.MDef.Value * 100f) / 100f).ToString();
        spd.text = ((float)Math.Round(status.Spd.Value * 100f) / 100f).ToString();
        eva.text = ((float)Math.Round(status.Eva.Value * 100f) / 100f).ToString();
        hit.text = ((float)Math.Round(status.Hit.Value * 100f) / 100f).ToString();
        crate.text = ((float)Math.Round(status.Crate.Value * 100f) / 100f).ToString();
        cdmg.text = ((float)Math.Round(status.Cdmg.Value * 100f) / 100f).ToString();
    }
}
