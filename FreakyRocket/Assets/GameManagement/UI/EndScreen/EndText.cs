using UnityEditor;
using UnityEngine;

namespace Assets.GameManagement.UI.EndScreen
{
    public abstract class EndText : MonoBehaviour
    {
        public static EndText instance;
        public const string defaultWinText = "You won";
        public const string defaultLoseText = "You lost";

        [SerializeField]
        private SpecialEndText specialEndText;

        private void OnEnable()
        {
            instance = this;
        }

        private void OnDisable()
        {
            instance = null;
        }

        public string GetText(bool isWin)
        {
            string specialText = specialEndText.GetText(isWin);
            if (specialText != null)
                return PostprocessText.Process(specialText);
            return PostprocessText.Process(GetLocalText(isWin));
        }

        protected abstract string GetLocalText(bool isWin);
    }
}