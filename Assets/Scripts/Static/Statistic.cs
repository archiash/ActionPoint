using System.Collections;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

public class Statistic : MonoBehaviour
{
    public List<float> statusMeanValue = new List<float>();
    public List<float> statusStandardDeviation = new List<float>();
    public List<List<float>> statusValue = new List<List<float>>();

    public List<Monster> monsters = new List<Monster>();

#if UNITY_EDITOR
    private void OnValidate()
    {
        LoadItems();
        WriteCSV();
    }

    private void LoadItems()
    {
        monsters = FindAssetsByType<Monster>("Assets").ToList();
        monsters = monsters.OrderBy(x => x.usePoint).ToList();
    }

    private void WriteCSV()
    {
        var csv = new StringBuilder("Monster Name, HP, MP, Attack, Defense, Magic, Resist, Speed, Accurate, Evade, Cri Rate, Cri DMG, Penetrate, Neutralize, Cri Resist");
        foreach(Monster m in monsters)
        {
            csv.Append("\n").Append($"{m.Name},{m.status.HP.Value},{m.status.MP.Value},{m.status.PAtk.Value},{m.status.PDef.Value},{m.status.MAtk.Value},{m.status.MDef.Value},{m.status.Spd.Value},{m.status.Hit.Value},{m.status.Eva.Value},{m.status.Crate.Value},{m.status.Cdmg.Value},{m.status.Pen.Value},{m.status.Neu.Value},{m.status.Cres.Value}");
        }
        var folder = Application.streamingAssetsPath;
        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
        var filePath = Path.Combine(folder, "monster.csv");
        using (var writer = new StreamWriter(filePath, false))
        {
            writer.Write(csv);
        }
    }

    public static T[] FindAssetsByType<T>(params string[] folders) where T : Object
    {
        string type = typeof(T).ToString().Replace("UnityEngine.", "");
        string[] guids;
        if (folders == null || folders.Length == 0)
        {
            guids = AssetDatabase.FindAssets("t:" + type);
        }
        else
        {
            guids = AssetDatabase.FindAssets("t:" + type, folders);
        }


        T[] assets = new T[guids.Length];

        for (int i = 0; i < guids.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
            assets[i] = AssetDatabase.LoadAssetAtPath<T>(assetPath);
        }
        return assets;
    }

#endif

    public void Start()
    {
        Recalculate();
    }

    public void Recalculate()
    {
        statusValue.Clear();
        statusMeanValue.Clear();
        statusStandardDeviation.Clear();

        for (int i = 0; i < 14; i++)
        {
            statusValue.Add(new List<float>() { Character.instance.status.GetStat((SubStatType)i).Value });
            foreach (Monster m in monsters)
            {
                statusValue[i].Add(m.status.GetStat((SubStatType)i).Value);
            }
        }

        for (int i = 0; i < 14; i++)
        {
            statusMeanValue.Add(statusValue[i].Average());
        }

        for (int i = 0; i < 14; i++)
        {
            float UpperEquationSum = 0;
            foreach (float v in statusValue[i])
            {
                UpperEquationSum += Mathf.Pow(v - statusMeanValue[i], 2);
            }
            statusStandardDeviation.Add(Mathf.Sqrt(UpperEquationSum / (monsters.Count + 1)));
        }
    }

    public float TScore(float value, int i)
    {
        float offset = value > 0 ? 1 : 0;
        return offset + (value - Mathf.Min(statusValue[i].ToArray())) / statusStandardDeviation[i];
    }

    public float MaxTScore(int i)
    {
        return TScore(Mathf.Max(statusValue[i].ToArray()),i);
    }

}
