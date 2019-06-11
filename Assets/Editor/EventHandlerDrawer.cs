
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(EventHandler),true)]
public class EventHandlerDrawer : PropertyDrawer
{
    SerializedProperty m_target;
    SerializedProperty m_call;

    private void Init(SerializedProperty property)
    {
        m_target = property.FindPropertyRelative("m_target");
        m_call = property.FindPropertyRelative("m_call");
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = 0.0f;
        height = EditorGUIUtility.singleLineHeight * 2;
        return height;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Init(property);
        Rect rect = position;
        EditorGUI.PrefixLabel(rect,new GUIContent("CallBackFunction"));
        EditorGUIUtility.labelWidth = 60;
        rect.y += EditorGUIUtility.singleLineHeight;
        rect.height = 16;
        rect.width = 200;
        EditorGUI.ObjectField(rect, m_target, new GUIContent("function"));
    }
}
