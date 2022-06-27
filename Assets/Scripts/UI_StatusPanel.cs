using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_StatusPanel : MonoBehaviour
{
    Status status;

    [SerializeField] TextMeshProUGUI atk;
    [SerializeField] TextMeshProUGUI def;
    [SerializeField] TextMeshProUGUI pen;
    [SerializeField] TextMeshProUGUI mp;
    [SerializeField] TextMeshProUGUI mag;
    [SerializeField] TextMeshProUGUI res;
    [SerializeField] TextMeshProUGUI neu;
    [SerializeField] TextMeshProUGUI spd;
    [SerializeField] TextMeshProUGUI eva;
    [SerializeField] TextMeshProUGUI hit;
    [SerializeField] TextMeshProUGUI crate;
    [SerializeField] TextMeshProUGUI cdmg;
    [SerializeField] TextMeshProUGUI cres;

    void Start()
    {
        status = Character.instance.status;
    }

    void Update()
    {
        atk.text = ((float)Math.Round(status.PAtk.Value * 100f) / 100f).ToString();
        def.text = ((float)Math.Round(status.PDef.Value * 100f) / 100f).ToString();
        pen.text = ((float)Math.Round(status.Pen.Value * 100f) / 100f).ToString();
        mp.text = ((float)Math.Round(status.MP.Value * 100f) / 100f).ToString();
        mag.text = ((float)Math.Round(status.MAtk.Value * 100f) / 100f).ToString();
        res.text = ((float)Math.Round(status.MDef.Value * 100f) / 100f).ToString();
        neu.text = ((float)Math.Round(status.Neu.Value * 100f) / 100f).ToString();
        spd.text = ((float)Math.Round(status.Spd.Value * 100f) / 100f).ToString();
        eva.text = ((float)Math.Round(status.Eva.Value * 100f) / 100f).ToString();
        hit.text = ((float)Math.Round(status.Hit.Value * 100f) / 100f).ToString();
        crate.text = ((float)Math.Round(status.Crate.Value * 100f) / 100f).ToString(); //+ " | " + ((50 + (float)Math.Round(status.Crate.Value * 100f) / 100f) / 5) + "%";
        cdmg.text = ((float)Math.Round(status.Cdmg.Value * 100f) / 100f).ToString(); //+ " | " + (2f + ((float)Math.Round(status.Cdmg.Value * 100f) / 100f) / 100) * 100 + "%" ;
        cres.text = ((float)Math.Round(status.Cres.Value * 100f) / 100f).ToString();
    }
}
