using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
[System.Serializable]
public class DropItem
{
    public Item item;
    [Range(0,100)]
    public float rateDrop;
    [ShowOnly]public float trueRate;

    public int minDrop;
    public int maxDrop;
}

[System.Serializable]
public class DropTable
{
    public DropItem[] items;

    public StackItem DropLoot()
    {
        float random = Random.Range(1.0f, 100.0f);
        float currentRate = 0;
        for (int i = 0; i < items.Length; i++)
        {
            currentRate += items[i].trueRate;
            if (random <= currentRate)
            {
                int dropAmount = Random.Range(items[i].minDrop, items[i].maxDrop + 1);
                if(Character.instance.Class == Character.CharacterClass.Adventurer)
                {
                    if (Random.value <= 0.1f)
                    {
                        dropAmount++;
                        Debug.Log("Adventurer Extra Drop");
                    }

                }
                return new StackItem(items[i].item, dropAmount);
            }
        }

        return null;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(DropTable))]
public class DropTableDrawer : PropertyDrawer
{

    SerializedProperty items;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        property.isExpanded = EditorGUI.Foldout(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), property.isExpanded, label);


        items = property.FindPropertyRelative("items");
        float percent = 100f;
        for (int i = 0; i < items.arraySize; i++)
        {
            float rate = items.GetArrayElementAtIndex(i).FindPropertyRelative("rateDrop").floatValue;
            float trueValue = (float)System.Math.Round(percent * rate / 100, 1);
            items.GetArrayElementAtIndex(i).FindPropertyRelative("trueRate").floatValue = trueValue;
            percent -= trueValue;
        }


        if (property.isExpanded)
        {
            EditorGUI.PropertyField(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight), property.FindPropertyRelative("items"), true);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (property.isExpanded)
        {
            return EditorGUIUtility.singleLineHeight + EditorGUI.GetPropertyHeight(property.FindPropertyRelative("items"), true);
        }
        return EditorGUIUtility.singleLineHeight;
    }
}
# endif
