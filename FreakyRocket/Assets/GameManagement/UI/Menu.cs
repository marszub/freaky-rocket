using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        GameManager.LoadLevel(GameManager.unlockedLevel);
    }

    public void Levels()
    {
        GameManager.LoadLevels();
    }
}
