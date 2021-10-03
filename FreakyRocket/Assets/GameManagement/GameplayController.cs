using Assets.GameManagement.UI;
using Assets.GameManagement.UI.EndScreen;
using Assets.MapObject.TouchHandling;
using Assets.PlayerControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.GameManagement
{
    [RequireComponent(typeof(StatTracker))]
    public class GameplayController : MonoBehaviour
    {
        [SerializeField] private float SegmentTime;
        [SerializeField] private Controlls controlls;
        public static GameplayController instance;
        [SerializeField] private Text endPhrase;
        [SerializeField] private Animator endScreenAnimator;
        [SerializeField] private GameObject startButton;
        [SerializeField] private AudioClip winSound;
        [SerializeField] private AudioClip loseSound;

        private float mapTime;
        private bool isMapMoving;
        private new AudioSource audio;

        private enum GameState { Preparing, Playing, WinScreen, LoseScreen};
        private GameState gameState;

        public delegate void EventHandler();
        public static event EventHandler Play;
        public static event EventHandler Stop;

        public delegate void LevelEventHandler(int level);
        public static event LevelEventHandler LevelPassed;
        public static event LevelEventHandler Death;

        private void Awake()
        {
            instance = this;
        }

        private void OnEnable()
        {
            Play += GameplayController_Play;
            LevelPassed += GameplayController_LevelPassed;
            Death += GameplayController_Death;
        }

        private void GameplayController_Play()
        {
            gameState = GameState.Playing;
            isMapMoving = true;
            startButton.SetActive(false);
        }

        private void GameplayController_LevelPassed(int _)
        {
            gameState = GameState.WinScreen;

            audio.clip = winSound;
            audio.Play();
        }

        private void GameplayController_Death(int _)
        {
            gameState = GameState.LoseScreen;

            audio.clip = loseSound;
            audio.Play();
        }

        private void Start()
        {
            gameState = GameState.Preparing;
            isMapMoving = false;
            mapTime = 0;
            audio = GetComponent<AudioSource>();

            GenerateBorders();
        }

        private void OnDisable()
        {
            Play -= GameplayController_Play;
            LevelPassed -= GameplayController_LevelPassed;
            Death -= GameplayController_Death;
        }

        private void GenerateBorders()
        {
            float yExtent = GetComponent<Camera>().orthographicSize * 1.01f;
            float xExtent = yExtent * Screen.width / Screen.height * 1.01f;
            Vector2 cameraPosition = GetComponent<Camera>().transform.position;

            GameObject topBorder = new GameObject("Top border");
            topBorder.transform.parent = transform;
            topBorder.AddComponent<DieOnTouch>();
            topBorder.layer = GlobalSettings.LAYER_TRANSPARENT_OBJECTS;
            BoxCollider2D topCollider = topBorder.AddComponent<BoxCollider2D>();
            topCollider.size = new Vector2(3 * xExtent, yExtent * 0.6f);
            topCollider.transform.position = cameraPosition + new Vector2(0, yExtent * 1.5f);

            GameObject botBorder = new GameObject("Bot border");
            botBorder.transform.parent = transform;
            botBorder.AddComponent<DieOnTouch>();
            botBorder.layer = GlobalSettings.LAYER_TRANSPARENT_OBJECTS;
            BoxCollider2D botCollider = botBorder.AddComponent<BoxCollider2D>();
            botCollider.size = new Vector2(3 * xExtent, yExtent * 0.6f);
            botCollider.transform.position = cameraPosition - new Vector2(0, yExtent * 1.5f);

            GameObject rightBorder = new GameObject("Right border");
            rightBorder.transform.parent = transform;
            rightBorder.AddComponent<DieOnTouch>();
            rightBorder.layer = GlobalSettings.LAYER_TRANSPARENT_OBJECTS;
            BoxCollider2D rightCollider = rightBorder.AddComponent<BoxCollider2D>();
            rightCollider.size = new Vector2(xExtent * 0.6f, 3 * yExtent);
            rightCollider.transform.position = cameraPosition + new Vector2(xExtent * 1.4f, 0);

            GameObject leftBorder = new GameObject("Left border");
            leftBorder.transform.parent = transform;
            leftBorder.AddComponent<DieOnTouch>();
            leftBorder.layer = GlobalSettings.LAYER_TRANSPARENT_OBJECTS;
            BoxCollider2D leftCollider = leftBorder.AddComponent<BoxCollider2D>();
            leftCollider.size = new Vector2(xExtent * 0.6f, 3 * yExtent);
            leftCollider.transform.position = cameraPosition - new Vector2(xExtent * 1.4f, 0);
        }

        public void PlayerOff()
        {
            startButton.SetActive(true);
        }

        private void FixedUpdate()
        {
            if (isMapMoving)
            {
                mapTime += Time.fixedDeltaTime;
            } 
        }

        public void StartGame()
        {
            Play?.Invoke();
        }

        public void EndScreenSubmit()
        {
            switch (gameState)
            {
                case GameState.LoseScreen:
                    GameManager.ReloadLevel();
                    break;
                case GameState.WinScreen:
                    GameManager.ReloadLevel();
                    break;
                default:
                    break;
            }
        }

        public Vector2 GetMoveDirection(Vector2 currentPosition)
        {
            return controlls.GetMoveDirection(currentPosition);
        }

        public float GetMapTime() => mapTime / SegmentTime;

        public void EndScreen(bool success)
        {
            if (gameState != GameState.Playing)
                return;
            startButton.SetActive(false);

            Stop?.Invoke();

            int deaths;
            if (success)
            {
                deaths = StatTracker.stats.currentDeathsEachLevel[GameManager.currentLevel];
                LevelPassed?.Invoke(GameManager.currentLevel);
            }
            else
            {
                Death?.Invoke(GameManager.currentLevel);
                deaths = StatTracker.stats.currentDeathsEachLevel[GameManager.currentLevel];
            }
            SetDeathCounter(deaths);

            endPhrase.text = EndText.instance ? EndText.instance.GetText(success) : (success ? "You won" : "You lost");
            endScreenAnimator.SetTrigger("FadeIn");
            isMapMoving = false;
        }

        private void SetDeathCounter(int deaths)
        {
            if (deaths == 0)
                SetDeathCounterText("Finished", "Crashless");
            else if(deaths == 1)
                SetDeathCounterText("Crashed", "Once");
            else
                SetDeathCounterText("Crashed", deaths.ToString() + " times");
        }

        private void SetDeathCounterText(string top, string bot)
        {
            Text crashedBot = transform.Find("UI").Find("Crashed").GetComponent<Text>();
            Text crashedTop = crashedBot.transform.Find("Top").GetComponent<Text>();

            crashedBot.text = top;
            crashedTop.text = top;

            Text counterBot = transform.Find("UI").Find("Counter").GetComponent<Text>();
            Text counterTop = counterBot.transform.Find("Top").GetComponent<Text>();

            counterBot.text = bot;
            counterTop.text = bot;
        }

        public void GoToMenu() => GameManager.LoadMenu();
    }
}