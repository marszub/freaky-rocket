using UnityEditor;
using UnityEngine;

namespace Assets
{
    public static class TextSetListDrawer
    {
        public static void DrawList(string label, string threshodName, SerializedProperty weight, SerializedProperty texts, SerializedProperty threshods, SerializedObject serializedObject)
        {
            EditorGUILayout.LabelField(label);

            EditorGUILayout.BeginVertical("GroupBox");

            if (weight != null)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Weight");
                weight.floatValue = EditorGUILayout.FloatField(weight.floatValue);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Id", GUILayout.MaxWidth(20));
            EditorGUILayout.LabelField("Text set", GUILayout.MinWidth(30));
            EditorGUILayout.LabelField(threshodName, GUILayout.MaxWidth(100));
            EditorGUILayout.LabelField("", GUILayout.MaxWidth(40));
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < texts.arraySize; i++)
            {
                SerializedProperty text = texts.GetArrayElementAtIndex(i);
                SerializedProperty threshod = threshods.GetArrayElementAtIndex(i);

                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField(i.ToString(), GUILayout.MaxWidth(20));
                EditorGUILayout.PropertyField(text, GUIContent.none, true, GUILayout.MinWidth(30));
                threshod.intValue = EditorGUILayout.IntField(threshod.intValue, GUILayout.MaxWidth(100));

                if (GUILayout.Button("-", GUILayout.MaxWidth(40)))
                {
                    texts.GetArrayElementAtIndex(i).objectReferenceValue = null;
                    texts.DeleteArrayElementAtIndex(i);
                    threshods.DeleteArrayElementAtIndex(i);

                    serializedObject.ApplyModifiedProperties();
                    serializedObject.Update();
                }

                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("+"))
            {
                texts.InsertArrayElementAtIndex(texts.arraySize);
                threshods.InsertArrayElementAtIndex(threshods.arraySize);
            }

            EditorGUILayout.EndVertical();
        }
    }
}