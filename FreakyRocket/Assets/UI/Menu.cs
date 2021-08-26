using Assets.GameManagement.UI;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        GameManager.LoadLevel(GameManager.nextLevelToLoad);
    }

    public void Levels()
    {
        GameManager.LoadLevels();
    }
}
