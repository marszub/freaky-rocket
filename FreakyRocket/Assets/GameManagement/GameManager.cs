using Assets.GameManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    private static int _currentLevel = 1;
    public static int currentLevel { get => _currentLevel; }

    private static int _nextLevelToLoad = 1;
    public static int nextLevelToLoad { get => _nextLevelToLoad; }

    private static int _unlockedLevel = 1;
    public static int unlockedLevel { get => _unlockedLevel; }

    static GameManager()
    {
        GameplayController.LevelPassed += LevelPassed;
    }

    public static void LoadLevel(int lvl)
    {
        if (lvl < 1 || lvl > GlobalSettings.levels + 1)
            throw new ArgumentException("Trying to load level out of range: " + lvl.ToString());
        _currentLevel = lvl;
        _nextLevelToLoad = lvl;
        SceneManager.LoadScene("LevelBase", LoadSceneMode.Single);
        SceneManager.LoadScene(GlobalSettings.firstLevelIdx + lvl - 1, LoadSceneMode.Additive);
    }

    public static void ReloadLevel()
    {
        LoadLevel(nextLevelToLoad);
    }

    public static void LevelPassed(int level)
    {
        _nextLevelToLoad++;
        if (level < GlobalSettings.levels && _unlockedLevel < nextLevelToLoad)
        {
            _unlockedLevel = nextLevelToLoad;
        }
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
        _nextLevelToLoad = 1;
    }
}
