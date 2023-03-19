
using UnityEditor;

using UnityEngine;

public enum Rarity { Common, Uncommon, Rare }
public enum ItemType {Material, Useable, Equipment }
public class Item : ScriptableObject
{
    [SerializeField]string id;
    public string ID { get { return id; } }

    public Sprite icon;
    public string itemName;
    public Rarity rarity;

    [TextArea(2,3)]
    public string itemDes;
    public ItemType itemType;

    public int sellPrice;

    public virtual int price
    {
        get
        {
            return sellPrice;
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        id = AssetDatabase.AssetPathToGUID(path);
    }
#endif
    public virtual bool UseItem(Status user = null)
    {
        return false;
    }

    public virtual string GetDesc(bool fulldesc = true, bool isDownList = true)
    {
        return itemDes;
    }

    public virtual Item GetCopyItem(bool savePower = false)
    {
        return Instantiate(this);
    }
}

#if UNITY_EDITOR
public class ItemEditor
{
    SerializedObject m_Target;


    SerializedProperty m_NameProperty;
    SerializedProperty m_IconProperty;
    SerializedProperty m_DescriptionProperty;
    SerializedProperty m_RarityProperty;
    SerializedProperty m_TypeProperty;
    SerializedProperty m_PriceProperty;
    public void Init(SerializedObject target)
    {
        m_Target = target;

        m_NameProperty = m_Target.FindProperty(nameof(Item.itemName));
        m_IconProperty = m_Target.FindProperty(nameof(Item.icon));
        m_DescriptionProperty = m_Target.FindProperty(nameof(Item.itemDes));
        m_RarityProperty = m_Target.FindProperty(nameof(Item.rarity));
        m_TypeProperty = m_Target.FindProperty(nameof(Item.itemType));
        m_PriceProperty = m_Target.FindProperty(nameof(Item.price));
    }

    public void GUI()
    {
        
        EditorGUILayout.LabelField("ID: " + m_Target.FindProperty("id").stringValue);
        EditorGUILayout.PropertyField(m_IconProperty);
        EditorGUILayout.PropertyField(m_NameProperty);
        EditorGUILayout.PropertyField(m_DescriptionProperty, GUILayout.MinHeight(128));
        EditorGUILayout.PropertyField(m_RarityProperty);
        EditorGUILayout.PropertyField(m_TypeProperty);
        EditorGUILayout.PropertyField(m_PriceProperty);
    }
}
#endif