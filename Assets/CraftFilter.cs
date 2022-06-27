using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class CraftFilter : MonoBehaviour
{
    public static CraftFilter instance;

    private void Start()
    {
        instance = this;
    }
    [SerializeField] bool tier1, tier2;
    public bool Tier1 { get { return tier1;} set { tier1 = value; } }
    public bool Tier2 { get { return tier2; } set { tier2 = value; } }

    [SerializeField] bool weapon, head, arms, legs, body, acc;

    public bool Weapon { get { return weapon; } set { weapon = value; } }
    public bool Head { get { return head; } set { head = value; } }
    public bool Arms { get { return arms; } set { arms = value; } }
    public bool Legs { get { return legs; } set { legs = value; } }
    public bool Body { get { return body; } set { body = value; } }
    public bool Accessory { get { return acc; } set { acc = value; } }

    public bool STR, INT, AGI, DEX, CON;

    public bool Str { get { return STR; } set { STR = value; } }
    public bool Int { get { return INT; } set { INT = value; } }
    public bool Agi { get { return AGI; } set { AGI = value; } }
    public bool Dex { get { return DEX; } set { DEX = value; } }
    public bool Con { get { return CON; } set { CON = value; } }

    [SerializeField] bool hp, mp, def, mag, res,pen,neu,spd,
        eva,hit,crc,cdm,crr,atk;

    public bool Hp { get { return hp; } set { hp = value; } }
    public bool Mp { get { return mp; } set { mp = value; } }
    public bool Def { get { return def; } set { def = value; } }
    public bool  Mag { get { return mag; } set { mag = value; } }
    public bool Res { get { return res; } set { res = value; } }
    public bool Pen { get { return pen; } set { pen = value; } }
    public bool Neu { get { return neu; } set { neu = value; } }
    public bool Spd { get { return spd; } set { spd = value; } }
    public bool Eva { get { return eva; } set { eva = value; } }
    public bool Hit { get { return hit; } set { hit = value; } }
    public bool Crc { get { return crc; } set { crc = value; } }
    public bool Cdm { get { return cdm; } set { cdm = value; } }
    public bool Crr { get { return crr; } set { crr = value; } }
    public bool Atk { get { return atk; } set { atk = value; } }

    [SerializeField] bool checkAll;
    public bool CheckAll { get { return checkAll; } set { checkAll = value;} }

    [SerializeField] CraftFilterOption[] buttons;

    public bool setFilter(string filterName)
    {
        if (filterName == "CheckAll")
        {
            bool newValue = !CheckAll;
            foreach (PropertyInfo propertyInfo in GetType().GetProperties())
            {

                if (propertyInfo.PropertyType == typeof(bool) && propertyInfo.CanWrite)
                {
                    Debug.Log(propertyInfo.Name);
                    GetType().GetProperty(propertyInfo.Name).SetValue(this, newValue);

                }
            }
            foreach (CraftFilterOption button in buttons)
            {
                button.SetButtonFiler(newValue);
            }
            return newValue;
        }
        else
        {

            bool isSet = (bool)GetType().GetProperty(filterName).GetValue(this);
            isSet = !isSet;
            if (!isSet)
            {
                buttons[buttons.Length - 1].SetButtonFiler(false);
                checkAll = false;
            }
            GetType().GetProperty(filterName).SetValue(this, isSet);
            return isSet;
        }
        
    }

    public bool GetFilter(string filterName)
    {
        return (bool)GetType().GetProperty(filterName).GetValue(this);
    }

    public bool GetFilter(SubStatType subStatType) => subStatType switch 
    {
        SubStatType.HP => Hp,
        SubStatType.MP => Mp,
        SubStatType.PAtk => Atk,
        SubStatType.PDef => Def,
        SubStatType.MAtk => Mag,
        SubStatType.MDef => Res,
        SubStatType.Pen => Pen,
        SubStatType.Neu => Neu,
        SubStatType.Spd => Spd,
        SubStatType.Hit => Hit,
        SubStatType.Eva => Eva,
        SubStatType.Cdmg => Cdm,
        SubStatType.Crate => Crc,
        SubStatType.Cres => Crr
        
    };
}
