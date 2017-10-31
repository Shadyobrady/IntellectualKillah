using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FreePlayMenuManager : MonoBehaviour {

	// Use this for initialization
    public void BeachLevel()
    {SceneManager.LoadSceneAsync("ContainerLevel");}
    public void ForestLevel()
    {SceneManager.LoadSceneAsync("MountainVillageLevel");}
    public void DesertLevel ()
    { SceneManager.LoadSceneAsync("DesertLevel"); }
    public void TownLevel ()
    { SceneManager.LoadSceneAsync("TownLevel"); }
    public void BackToMenu()
    { SceneManager.LoadSceneAsync("TitleMenu"); }
}
