using Assets.GameManagement.UI.EndScreen;
using UnityEditor;

namespace Assets
{
    [CustomEditor(typeof(SpecialEndText))]
    public class SpecialEndTextEditor : Editor
    {
        private SerializedProperty totalDeathsTexts_Prop;
        private SerializedProperty totalDeaths_Prop;

        public void OnEnable()
        {
            totalDeathsTexts_Prop = serializedObject.FindProperty("totalDeathsTexts");
            totalDeaths_Prop = serializedObject.FindProperty("totalDeaths");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            TextSetListDrawer.DrawList("Lose text dependent on total deaths", "Deaths", null, totalDeathsTexts_Prop, totalDeaths_Prop, serializedObject);
            serializedObject.ApplyModifiedProperties();
        }
    }
}