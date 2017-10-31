using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryDeathManager : MonoBehaviour
{
    
    
    public string PreviousLevel;
    public void RetryLevel()
    {
        SceneManager.LoadSceneAsync(PreviousLevel);
    }
    
    public void ReturnToMenu()
    {
        SceneManager.LoadSceneAsync("TitleMenu");
    }
}
