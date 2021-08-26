using Assets.GameManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsMenu : MonoBehaviour
{
    public GameObject pagePrefab;
    private LevelsPage displayedPage;
    
    private int currentPage;

    void Start()
    {
        currentPage = (GameManager.nextLevelToLoad - 1) / LevelsPage.levelsPerPage + 1;

        LoadPage(LevelsPage.TransitionDirection.None);
    }

    public void Menu()
    {
        GameManager.LoadMenu();
    }

    public void NextPage()
    {
        if (currentPage >= (GameManager.unlockedLevel - 1) / LevelsPage.levelsPerPage + 1)
            throw new System.Exception("Unreachable levels page.");

        currentPage++;
        displayedPage.Unload(LevelsPage.TransitionDirection.Left);

        LoadPage(LevelsPage.TransitionDirection.Left);
    }

    public void PreviousPage()
    {
        if (currentPage <= 1)
            throw new System.Exception("Unreachable levels page.");

        currentPage--;
        displayedPage.Unload(LevelsPage.TransitionDirection.Right);

        LoadPage(LevelsPage.TransitionDirection.Right);
    }

    private void LoadPage(LevelsPage.TransitionDirection direction)
    {
        transform.Find("Left button").GetComponent<Button>().interactable = false;
        transform.Find("Right button").GetComponent<Button>().interactable = false;

        GameObject newPage = Instantiate(pagePrefab, transform);
        displayedPage = newPage.GetComponent<LevelsPage>();
        displayedPage.Load(direction,
            (currentPage - 1) * LevelsPage.levelsPerPage + 1,
            Math.Min(currentPage * LevelsPage.levelsPerPage, GameManager.unlockedLevel));
    }

    public void OnPageLoaded()
    {
        if(currentPage > 1)
            transform.Find("Left button").GetComponent<Button>().interactable = true;

        if (currentPage < (GameManager.unlockedLevel - 1) / LevelsPage.levelsPerPage + 1)
            transform.Find("Right button").GetComponent<Button>().interactable = true;
    }
}
