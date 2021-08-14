using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    private const int firstLevelIdx = 2;
    private const string menuScene = "MainMenu";
    private const string levelsScene = "Levels";
    private static int _unlockedLevel = 1;
    private static int _currentLevel = 1;
    public static int unlockedLevel { get => _unlockedLevel; }

    public static void LoadLevel(int lvl)
    {
        //_currentLevel = lvl;
        SceneManager.LoadScene("LevelBase", LoadSceneMode.Single);
        SceneManager.LoadScene(firstLevelIdx + lvl - 1, LoadSceneMode.Additive);
    }

    public static void ReloadLevel()
    {
        LoadLevel(_currentLevel);
    }

    public static void NextLevel()
    {
        _currentLevel++;
        ReloadLevel();
    }

    public static void LoadMenu()
    {
        SceneManager.LoadScene(menuScene);
    }

    public static void LoadLevels()
    {
        SceneManager.LoadScene(levelsScene);
    }

    public static void ResetCurrentLevel()
    {
        _currentLevel = 1;
    }
}
