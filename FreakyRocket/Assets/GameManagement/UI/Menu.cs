using Assets.GameManagement.UI;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        GameManager.LoadLevel(StatTracker.unlockedLevel);
    }

    public void Levels()
    {
        GameManager.LoadLevels();
    }
}
