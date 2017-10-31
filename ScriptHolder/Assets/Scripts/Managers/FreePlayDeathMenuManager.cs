using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FreePlayDeathMenuManager : MonoBehaviour
{
    public void ReturnToMenu()
    {


        SceneManager.LoadSceneAsync("FreePlayMenu");


    }
}
