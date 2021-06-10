﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointManager : MonoBehaviour
{
    public static PointManager instance;

    private void Awake()
    {
        if (instance != null)
            return;

        instance = this;
    }

    [SerializeField] private double actionPoint;
    [SerializeField] private float actionPersec;
    public float actionPerSecLvBonus;

    private void FixedUpdate()
    {
        actionPoint += (actionPersec + actionPerSecLvBonus) * Time.fixedDeltaTime;
    }

    public void UseAction(int point)
    {
        actionPoint -= point;
    }

    public double GetActionPoint { get { return actionPoint; } set { actionPoint = value; } }
    public double GetActionPerSec {get { return actionPersec + actionPerSecLvBonus; } set { actionPersec = (float)value; } }
 
    public void AddPointPerSec(float amount)
    {
        actionPersec += amount;
    }

}
