using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    

    public void StoryMode()
    {
        SceneManager.LoadSceneAsync("StoryMode");
    }

    public void FreePlay()
    {
        SceneManager.LoadSceneAsync("FreePlayMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
