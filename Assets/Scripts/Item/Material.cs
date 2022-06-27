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
        Texture2D aTexture = t.icon? SpriteUtility.GetSpriteTexture(t.icon, false):null;
        GUILayout.Label(aTexture);
        DrawDefaultInspector();
        if (GUILayout.Button("Get Item"))
        {
            Inventory.instance.GetItem(t.GetCopyItem());
        }
    }

    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        Material t = (Material)target;

        if (t != null && t.icon != null)
        {

            // example.PreviewIcon must be a supported format: ARGB32, RGBA32, RGB24,
            // Alpha8 or one of float formats
            Texture2D tex = new Texture2D(width, height);
            EditorUtility.CopySerialized(source: t.icon.texture, dest: tex);

            return tex;
        }
        return null;
    }
}
#endif