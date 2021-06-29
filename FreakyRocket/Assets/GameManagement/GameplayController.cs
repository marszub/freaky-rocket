using Assets.Input;
using Assets.MapObject.TouchHandling;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.GameManagement
{
    public class GameplayController : MonoBehaviour
    {
        public float SegmentTime;
        public Controlls controlls;
        [Scene] public string nextScene;
        [Scene] public string menuScene;
        public static GameplayController instance;
        [HideInInspector] public Text endPhrase;
        [HideInInspector] public Animator endScreenAnimator;

        private float mapTime;
        private bool isMapMoving;

        private enum GameState { Preparing, Playing, WinScreen, LoseScreen};
        private GameState gameState;

        public delegate void EventHandler();
        public static event EventHandler Play;

        private void Start()
        {
            gameState = GameState.Preparing;
            isMapMoving = false;
            mapTime = 0;

            if (instance == null)
                instance = this;
            else
            {
                Debug.Log("GameController: Could not create an instance in GameObject: '" + gameObject.name + "', because one already exists in GameObject: '" + instance.gameObject.name + "'.");
                Destroy(this);
            }
            Play += GameplayController_Play;
            GenerateBorders();
        }

        private void GenerateBorders()
        {
            float yExtent = GetComponent<Camera>().orthographicSize * 1.01f;
            float xExtent = yExtent * Screen.width / Screen.height * 1.01f;
            Vector2 cameraPosition = GetComponent<Camera>().transform.position;

            GameObject topBorder = new GameObject("Top border");
            topBorder.transform.parent = transform;
            topBorder.AddComponent<DieOnTouch>();
            BoxCollider2D topCollider = topBorder.AddComponent<BoxCollider2D>();
            topCollider.size = new Vector2(3 * xExtent, yExtent * 0.6f);
            topCollider.transform.position = cameraPosition + new Vector2(0, yExtent * 1.5f);

            GameObject botBorder = new GameObject("Bot border");
            botBorder.transform.parent = transform;
            botBorder.AddComponent<DieOnTouch>();
            BoxCollider2D botCollider = botBorder.AddComponent<BoxCollider2D>();
            botCollider.size = new Vector2(3 * xExtent, yExtent * 0.6f);
            botCollider.transform.position = cameraPosition - new Vector2(0, yExtent * 1.5f);

            GameObject rightBorder = new GameObject("Right border");
            rightBorder.transform.parent = transform;
            rightBorder.AddComponent<DieOnTouch>();
            BoxCollider2D rightCollider = rightBorder.AddComponent<BoxCollider2D>();
            rightCollider.size = new Vector2(xExtent * 0.6f, 3 * yExtent);
            rightCollider.transform.position = cameraPosition + new Vector2(xExtent * 1.4f, 0);

            GameObject leftBorder = new GameObject("Left border");
            leftBorder.transform.parent = transform;
            leftBorder.AddComponent<DieOnTouch>();
            BoxCollider2D leftCollider = leftBorder.AddComponent<BoxCollider2D>();
            leftCollider.size = new Vector2(xExtent * 0.6f, 3 * yExtent);
            leftCollider.transform.position = cameraPosition - new Vector2(xExtent * 1.4f, 0);
        }

        private void GameplayController_Play()
        {
            isMapMoving = true;
        }

        private void FixedUpdate()
        {
            if (isMapMoving)
            {
                mapTime += Time.fixedDeltaTime;
            } 
        }

        public void OnSubmit()
        {
            switch (gameState)
            {
                case GameState.Preparing:
                    Play?.Invoke();
                    break;
                case GameState.LoseScreen:
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    break;
                case GameState.WinScreen:
                    SceneManager.LoadScene(nextScene);
                    break;
                default:
                    break;
            }
        }

        public Vector2 GetMoveDirection(Vector2 currentPosition) => controlls.GetMoveDirection(currentPosition);

        public float GetMapTime() => mapTime / SegmentTime;

        public void EndScreen(bool success)
        {
            if (success)
            {
                endPhrase.text = "You won!";
                gameState = GameState.WinScreen;
            }
            else
            {
                endPhrase.text = "You lost";
                gameState = GameState.LoseScreen;
            }
            endScreenAnimator.SetTrigger("FadeIn");
            isMapMoving = false;
        }

        public void GoToMenu()
        {
            SceneManager.LoadScene(menuScene);
        }
    }
}