using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastLevel : MonoBehaviour
{
    public void GoToMenu()
    {
        GameManager.ResetCurrentLevel();
        GameManager.LoadMenu();
    }
}
