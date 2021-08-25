using Assets.GameManagement.UI.EndScreen;
using UnityEditor;
using UnityEngine;

namespace Assets
{
    [CustomEditor(typeof(AdvancedEndText))]
    public class AdvancedEndTextEditor : Editor
    {
        private SerializedProperty specialEndText_Prop;

        private SerializedProperty winByWinsWeight_Prop;
        private SerializedProperty winByWinsText_Prop;
        private SerializedProperty winByWinsThreshod_Prop;

        private SerializedProperty winByDeathsWeight_Prop;
        private SerializedProperty winByDeathsText_Prop;
        private SerializedProperty winByDeathsThreshod_Prop;

        private SerializedProperty loseByDeathsWeight_Prop;
        private SerializedProperty loseByDeathsText_Prop;
        private SerializedProperty loseByDeathsThreshod_Prop;

        public void OnEnable()
        {
            specialEndText_Prop = serializedObject.FindProperty("specialEndText");

            winByWinsWeight_Prop = serializedObject.FindProperty("winByWinsWeight");
            winByWinsText_Prop = serializedObject.FindProperty("winByWinsText");
            winByWinsThreshod_Prop = serializedObject.FindProperty("winByWinsThreshod");

            winByDeathsWeight_Prop = serializedObject.FindProperty("winByDeathsWeight");
            winByDeathsText_Prop = serializedObject.FindProperty("winByDeathsText");
            winByDeathsThreshod_Prop = serializedObject.FindProperty("winByDeathsThreshod");

            loseByDeathsWeight_Prop = serializedObject.FindProperty("loseByDeathsWeight");
            loseByDeathsText_Prop = serializedObject.FindProperty("loseByDeathsText");
            loseByDeathsThreshod_Prop = serializedObject.FindProperty("loseByDeathsThreshod");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(specialEndText_Prop);

            TextSetListDrawer.DrawList("Win text dependent on wins", "Wins", winByWinsWeight_Prop, winByWinsText_Prop, winByWinsThreshod_Prop, serializedObject);
            TextSetListDrawer.DrawList("Win text dependent on deaths", "Deaths", winByDeathsWeight_Prop, winByDeathsText_Prop, winByDeathsThreshod_Prop, serializedObject);
            TextSetListDrawer.DrawList("Lose text dependent on deaths", "Deaths", loseByDeathsWeight_Prop, loseByDeathsText_Prop, loseByDeathsThreshod_Prop, serializedObject);
            serializedObject.ApplyModifiedProperties();
        }
    }
}