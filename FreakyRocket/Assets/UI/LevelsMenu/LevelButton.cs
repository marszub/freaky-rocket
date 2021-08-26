using Assets.GameManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int level;
    private const string levelImagesFolder = "UI/LevelImages/screen_";

    private void Start()
    {
        int buildIndex = GlobalSettings.firstLevelIdx + level - 1;

        string scenePath = SceneUtility.GetScenePathByBuildIndex(buildIndex);
        string sceneName = scenePath.Substring(0, scenePath.Length - 6).Substring(scenePath.LastIndexOf('/') + 1);
        string imagePath = levelImagesFolder + sceneName;

        Sprite levelSprite = Resources.Load<Sprite>(imagePath);
        transform.Find("Image").GetComponent<Image>().sprite = levelSprite;
    }

    public void OnClick()
    {
        GameManager.LoadLevel(level);
    }
}
