using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Recipe : ScriptableObject
{
    public Item resulItem;
    public List<StackItem> material = new List<StackItem>();
    public int cost;
}
