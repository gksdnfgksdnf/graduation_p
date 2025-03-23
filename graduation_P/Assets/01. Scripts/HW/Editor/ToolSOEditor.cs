using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ToolSO))]
public class ToolSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // ToolSO 참조
        ToolSO toolSO = (ToolSO)target;

        // 기본 속성 그리기
        DrawDefaultInspector();

        // isContainer가 true일 때만 ingredients 표시
        if (toolSO.isContainer)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Ingredients", EditorStyles.boldLabel);
            SerializedProperty ingredientsProperty = serializedObject.FindProperty("ingredients");
            EditorGUILayout.PropertyField(ingredientsProperty, true);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
