using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.Sprites;
using UnityEditor;
#endif

[CreateAssetMenu]
public class Recipe : ScriptableObject
{
    public Item resulItem;
    public List<StackItem> material = new List<StackItem>();
    public int cost;
}

#if UNITY_EDITOR
[CustomEditor(typeof(Recipe))]
public class RecipeEditor : Editor
{
    int Cost;
    public override void OnInspectorGUI()
    {
        Recipe t = (Recipe)target;
        Cost = t.cost;
        
        
        Texture2D aTexture = t.resulItem ? SpriteUtility.GetSpriteTexture(t.resulItem.icon, false) : null;
        GUILayout.Label(aTexture);
        base.OnInspectorGUI();
        for(int i = 0;i<t.material.Count;i++)
        {
            Cost += t.material[i].item.sellPrice * t.material[i].amount;
        }
        GUILayout.Label("Cost: " + Cost.ToString() + " " + Mathf.Round(Cost * 80f / 100f));

    }
}
#endif
