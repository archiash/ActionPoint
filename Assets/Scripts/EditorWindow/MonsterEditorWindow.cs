
#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MonsterEditorWindow : EditorWindow
{
    [MenuItem("Window/MonsterEditor")]

    public static void ShowMyEditor()
    {
        // This method is called when the user selects the menu item in the Editor
        EditorWindow wnd = GetWindow<MonsterEditorWindow>();
        wnd.titleContent = new GUIContent("Monster Editor");
    }

    void CreateGUI()
    {
        var monsterObjectGuids = AssetDatabase.FindAssets("t:Monster");
        var monsterObjects = new List<Monster>();
        var statusValue = new List<List<float>>();

        List<float> statusMeanValue = new List<float>();
        List<float> statusStandardDeviation = new List<float>();

        foreach (var guid in monsterObjectGuids)
        {
            Monster m = AssetDatabase.LoadAssetAtPath<Monster>(AssetDatabase.GUIDToAssetPath(guid));
            for(int i = 0; i < 14; i++)
            {
                statusValue.Add(new List<float>());
                statusValue[i].Add(m.status.GetStat((SubStatType)i).Value);
            }
            monsterObjects.Add(m);
        }

        // Create a two-pane view with the left pane being fixed with
        var splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);
        // Add the panel to the visual tree by adding it as a child to the root element
        rootVisualElement.Add(splitView);

        // A TwoPaneSplitView always needs exactly two child elements
        var leftPane = new ListView();
        splitView.Add(leftPane);
        var rightPane = new VisualElement();
        splitView.Add(rightPane);

        // Initialize the list view with all sprites' names
        leftPane.makeItem = () => new Label();
        leftPane.bindItem = (item, index) => { (item as Label).text = monsterObjects[index].name; };
        leftPane.itemsSource = monsterObjects;

    }
}
#endif