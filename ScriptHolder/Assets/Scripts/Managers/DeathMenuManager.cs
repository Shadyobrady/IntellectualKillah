using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuManager : MonoBehaviour
{
    private string LastScene;

    public void ReturnToMenu(bool FreePlay,string name)
    {
        if (FreePlay == true)
        {
            SceneManager.LoadSceneAsync("FreePlayMenu");
        }
        else if(FreePlay == false)
        {
            SceneManager.LoadSceneAsync(name);
        }
    }
}
