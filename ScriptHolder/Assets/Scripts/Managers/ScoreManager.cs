using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    Text text;
    GameObject[] Aitotal;

    void Awake()
    {
        text = GetComponent<Text>();
        Aitotal = GameObject.FindGameObjectsWithTag("AI");
        score = Aitotal.Length;
    }

    void Update()
    {
        Aitotal = GameObject.FindGameObjectsWithTag("AI");
        score = Aitotal.Length;
        text.text = "Enemies Left:" + score;
        if (score == 1)
        {
            //Change Navmesh location to player
            text.text = "One Enemy Remaining";
        }
        if (score == 0)
        {
            text.text = "Level Cleared";
        }
    }
}
    
