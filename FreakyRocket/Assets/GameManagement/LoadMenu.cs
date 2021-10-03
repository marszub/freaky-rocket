using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.GameManagement
{
    public class LoadMenu : MonoBehaviour
    {
        void Start()
        {
            SceneManager.LoadScene(GlobalSettings.menuScene);
        }
    }
}