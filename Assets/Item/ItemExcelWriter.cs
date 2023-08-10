#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class ItemExcelWriter : MonoBehaviour
{
    string filename;

    [SerializeField] Equipment[] itemData;
    [SerializeField] ItemDatabase itemDatabase;
    [SerializeField] RecipeDatabase recipeDatabase;

    public void WriteToExcel()
    {
        filename = Application.dataPath + "/itemData.csv";
        TextWriter tw = new StreamWriter(filename, false);
        tw.WriteLine("ItemName,Slot,STR, DEX, AGI, INT, CON, HP, MP, ATK, DEF, PEN, MAG, RES, NEU, SPD, HIT, EVA, CRC, CRD, CRR");
        tw.Close();

        tw = new StreamWriter(filename, true);

        foreach(Equipment i in itemData)
        {
            string STR = "", DEX = "", AGI = "", INT = "", CON = "", HP = "", MP = ""
                , ATK = "", DEF = "", PEN = "", MAG = "", RES = "", NEU = ""
                , SPD = "", HIT = "", EVA = "", CRC = "", CRD = "", CRR = "";

            foreach(EquipmentModifier mod in i.modifiers)
            {
                if (mod.modifierType == StatType.Main)
                {
                    switch (mod.mainType)
                    {
                        case MainStatType.STR:
                            STR += stringModValue(mod);
                            break;
                        case MainStatType.DEX:
                            DEX += stringModValue(mod);
                            break;
                        case MainStatType.AGI:
                            AGI += stringModValue(mod);
                            break;
                        case MainStatType.INT:
                            INT += stringModValue(mod);
                            break;
                        case MainStatType.CON:
                            CON += stringModValue(mod);
                            break;
                    }
                }
                if (mod.modifierType == StatType.Sub)
                {
                    switch (mod.statType)
                    {
                        case SubStatType.HP:
                            HP += stringModValue(mod);
                            break;
                        case SubStatType.MP:
                            MP += stringModValue(mod);
                            break;
                        case SubStatType.PAtk:
                            ATK += stringModValue(mod);
                            break;
                        case SubStatType.PDef:
                            DEF += stringModValue(mod);
                            break;
                        case SubStatType.Pen:
                            PEN += stringModValue(mod);
                            break;
                        case SubStatType.MAtk:
                            MAG += stringModValue(mod);
                            break;
                        case SubStatType.MDef:
                            RES += stringModValue(mod);
                            break;
                        case SubStatType.Neu:
                            NEU += stringModValue(mod);
                            break;
                        case SubStatType.Spd:
                            SPD += stringModValue(mod);
                            break;
                        case SubStatType.Eva:
                            EVA += stringModValue(mod);
                            break;
                        case SubStatType.Hit:
                            HIT += stringModValue(mod);
                            break;
                        case SubStatType.Cdmg:
                            CRD += stringModValue(mod);
                            break;
                        case SubStatType.Crate:
                            CRC += stringModValue(mod);
                            break;
                        case SubStatType.Cres:
                            CRR += stringModValue(mod);
                            break;
                    }
                }
            }

            tw.WriteLine(i.itemName + "," + i.part + "," + STR + "," + DEX + "," + AGI + "," + INT + "," + CON + "," +
                HP + "," + MP + "," + ATK + "," + DEF + "," + PEN + "," + MAG + "," + RES + "," +
                NEU + "," + SPD + "," + HIT + "," + EVA + "," + CRC + "," + CRD + "," + CRR);
        }

        tw.Close();
    }

    public void WriteMaterialToExcel()
    {
        List<Item> materialsList = new List<Item>();
        filename = Application.dataPath + "/itemMaterialUseData.csv";
        TextWriter tw = new StreamWriter(filename, false);
        tw.Write("Item Name");
        tw.Close();

        tw = new StreamWriter(filename, true);

        foreach (Item i in itemDatabase.items)
        {
            if(i.itemType == ItemType.Material)
            {
                materialsList.Add(i);
                tw.Write("," + i.itemName);
            }
        }

        tw.WriteLine();

        foreach(Recipe r in recipeDatabase.recipes)
        {
            tw.Write(r.resulItem.itemName + ",");
            foreach (Item m in materialsList)
            {
                bool isCheck = true;
                foreach(StackItem i in r.material)
                {
                    if (i.item == m)
                    {
                        tw.Write("/,");
                        isCheck = false;
                        break;
                    }
                }
                if(isCheck)
                    tw.Write(",");
            }
            tw.WriteLine();
        }

        tw.Close();
    }

    public string stringModValue(EquipmentModifier mod)
    {
        string s = "";
        if (mod.amount >= 0) s += "+" + mod.amount;
        else s += mod.amount;
        if (mod.type == ModifierType.Percentage) s += "%";
        if (!mod.isPowerEffect)
        {
            s += "ss";
        }
        else
        {
            if (!mod.isEnchantEffect)
            {
                s += "s";
            }
        }
        return s;
    }
}


[CustomEditor(typeof(ItemExcelWriter))]
public class ItemExcelWriterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ItemExcelWriter t = (ItemExcelWriter)target;
        base.OnInspectorGUI();
        if (GUILayout.Button("Write"))
        {
            t.WriteMaterialToExcel();
        }
    }
}
#endif