using System.Collections;
using System.Collections.Generic;
using UnityEditor;
#if UNITY_EDITOR
using UnityEditor.Sprites;
#endif
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Material", fileName = "New Material")]
public class Material : Item
{
    
}
#if UNITY_EDITOR
[CustomEditor(typeof(Material))]
public class MaterialEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Material t = (Material)target;
        Texture2D aTexture = SpriteUtility.GetSpriteTexture(t.icon, false);
        GUILayout.Label(aTexture);
        DrawDefaultInspector();
        if (GUILayout.Button("Get Item"))
        {
            Inventory.instance.GetItem(t.GetCopyItem());
        }
    }
}
#endif