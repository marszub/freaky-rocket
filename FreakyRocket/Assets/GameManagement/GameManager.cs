using Assets.GameManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    private static int _currentLevel = 1;
    public static int currentLevel { get => _currentLevel; }

    public static void LoadLevel(int lvl)
    {
        //_currentLevel = lvl;
        SceneManager.LoadScene("LevelBase", LoadSceneMode.Single);
        SceneManager.LoadScene(GlobalSettings.firstLevelIdx + lvl - 1, LoadSceneMode.Additive);
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
        SceneManager.LoadScene(GlobalSettings.menuScene);
    }

    public static void LoadLevels()
    {
        SceneManager.LoadScene(GlobalSettings.levelsScene);
    }

    public static void ResetCurrentLevel()
    {
        _currentLevel = 1;
    }
}
