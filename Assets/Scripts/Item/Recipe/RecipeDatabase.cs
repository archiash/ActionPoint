using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu]
public class RecipeDatabase : ScriptableObject
{
    [SerializeField] public Recipe[] recipes;

#if UNITY_EDITOR
    private void OnValidate()
    {
        LoadItems();
    }

    private void OnEnable()
    {
        EditorApplication.projectChanged += LoadItems;
    }

    private void OnDisable()
    {
        EditorApplication.projectChanged -= LoadItems;
    }

    private void LoadItems()
    {
        recipes = ItemDatabase.FindAssetsByType<Recipe>("Assets/Recipe");
    }

#endif
}
