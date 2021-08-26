using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsPage : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> levelButtonPrefabs;
    [SerializeField]
    private List<GameObject> levelButtons;
    [SerializeField]
    private Text title;
    public const int levelsPerPage = 10;
    private int levelButtonsLoaded;

    public enum TransitionDirection { Right, Left, None}

    public void Load(TransitionDirection direction, int firstLevel, int lastLevel)
    {
        levelButtonsLoaded = lastLevel - firstLevel + 1;

        title.text = string.Format("Levels {0}-{1}", firstLevel, lastLevel);

        for(int i = 0; i < levelButtonsLoaded; i++)
        {
            int randomButton = Random.Range(0, levelButtonPrefabs.Count);
            GameObject addedButton = Instantiate(levelButtonPrefabs[randomButton], levelButtons[i].transform);
            addedButton.name = "Button";
            addedButton.GetComponent<LevelButton>().level = firstLevel + i;
            addedButton.GetComponent<Button>().interactable = false;
        }

        switch (direction)
        {
            case TransitionDirection.Left:
                GetComponent<Animator>().SetTrigger("LoadLeft");
                break;
            case TransitionDirection.Right:
                GetComponent<Animator>().SetTrigger("LoadRight");
                break;
            case TransitionDirection.None:
                OnLoaded();
                break;
        }
    }

    public void OnLoaded()
    {
        foreach (GameObject button in levelButtons)
            if(button.transform.Find("Button"))
                button.transform.Find("Button").GetComponent<Button>().interactable = true;

        transform.parent.GetComponent<LevelsMenu>().OnPageLoaded();
    }

    public void Unload(TransitionDirection direction)
    {
        foreach (GameObject button in levelButtons)
            if (button.transform.Find("Button"))
                button.transform.Find("Button").GetComponent<Button>().interactable = false;

        switch (direction)
        {
            case TransitionDirection.Left:
                GetComponent<Animator>().SetTrigger("UnloadLeft");
                break;
            case TransitionDirection.Right:
                GetComponent<Animator>().SetTrigger("UnloadRight");
                break;
            default:
                break;
        }
    }

    public void OnUnloaded()
    {
        Destroy(gameObject);
    }
}
