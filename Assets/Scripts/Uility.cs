using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Uility
{
    public static bool Hit(float hit, float eva)
    {
        float hitChange = 1 - Mathf.Clamp((eva - hit) / eva, 0f, .67f);
        return Random.value <= hitChange;
    }

    public static float CriticalDamage(float value, float damage)
    {
        value *= 1.5f + damage / 100;
        return value;
    }

    public static bool IsCritical(float rate)
    {
        rate = (20 + rate / 5) / 100;
        return Random.value <= rate;
    }

    public static float Damage(float value)
    {
        return Random.Range(value * .8f, value * 1.2f);
    }
}
