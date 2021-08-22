using Assets.GameManagement.UI.EndScreen;
using UnityEditor;
using UnityEngine;

namespace Assets
{
    [CustomEditor(typeof(EquivalentTextSet))]
    public class EquivalentTextSetEditor : Editor
    {
        private EquivalentTextSet textSet;
        //private SerializedObject serializedObject;
        private SerializedProperty texts_Prop;
        private SerializedProperty weights_Prop;

        public void OnEnable()
        {
            textSet = (EquivalentTextSet)target;
            //serializedObject = new SerializedObject(textSet);
            texts_Prop = serializedObject.FindProperty("texts");
            weights_Prop = serializedObject.FindProperty("weights");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Id", GUILayout.MaxWidth(20));
            EditorGUILayout.LabelField("Text", GUILayout.MinWidth(30));
            EditorGUILayout.LabelField("Weight", GUILayout.MaxWidth(100));
            EditorGUILayout.LabelField("", GUILayout.MaxWidth(40));
            EditorGUILayout.EndHorizontal();

            for(int i = 0; i < textSet.texts.Count; i++)
            {
                SerializedProperty text = texts_Prop.GetArrayElementAtIndex(i);
                SerializedProperty weight = weights_Prop.GetArrayElementAtIndex(i);

                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField(i.ToString(), GUILayout.MaxWidth(20));
                text.stringValue = EditorGUILayout.TextArea(text.stringValue, GUILayout.MinWidth(30), GUILayout.MaxHeight((new GUIStyle()).CalcHeight(new GUIContent(text.stringValue), 400) + 5));
                weight.floatValue = EditorGUILayout.FloatField(weight.floatValue, GUILayout.MaxWidth(100));

                if (GUILayout.Button("-", GUILayout.MaxWidth(40)))
                {
                    texts_Prop.DeleteArrayElementAtIndex(i);
                    weights_Prop.DeleteArrayElementAtIndex(i);

                    serializedObject.ApplyModifiedProperties();
                    serializedObject.Update();
                }

                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("+"))
            {
                texts_Prop.InsertArrayElementAtIndex(texts_Prop.arraySize);
                texts_Prop.GetArrayElementAtIndex(texts_Prop.arraySize - 1).stringValue = "";
                weights_Prop.InsertArrayElementAtIndex(weights_Prop.arraySize);
                weights_Prop.GetArrayElementAtIndex(weights_Prop.arraySize - 1).floatValue = 0;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}