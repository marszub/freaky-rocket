using Assets.Input;
using UnityEngine;

namespace Assets.GameManagement
{
    public class GameplayController : MonoBehaviour
    {
        private static GameplayController instance;
        public Controlls controllsSetup;
        private static Controlls controlls;

        public delegate void EventHandler();
        public static event EventHandler Play;

        private void Awake()
        {
            if (instance is null)
                instance = this;
            else
            {
                Debug.Log("GameController: Could not create an instance in GameObject: '" + gameObject.name + "', because one already exists in GameObject: '" + instance.gameObject.name + "'.");
                Destroy(this);
            }
            if (controlls is null)
                controlls = controllsSetup;
        }

        public void OnSubmit()
        {
            Play?.Invoke();
        }

        public static Vector2 GetMoveDirection(Vector2 currentPosition) => controlls.GetMoveDirection(currentPosition);
    }
}