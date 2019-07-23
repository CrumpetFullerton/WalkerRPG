/*Preloader.cs
 * Created By: Phillip Buckreis 10/4/17 
 *
 * This script fades in and out the our game logo
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preloader : MonoBehaviour
{
    private CanvasGroup fadeGroup;
    private float loadTime;
    private float minimumLogoTime = 3.0f;

    private void Start()
    {

        fadeGroup = FindObjectOfType<CanvasGroup>();
        fadeGroup.alpha = 1.0f;

        //Preload the game//

        //Get timestamp of completion time
        //if load time is fast, give it a small buffer time.

        if (Time.time < minimumLogoTime)
            loadTime = minimumLogoTime;
        else
            loadTime = Time.time;
    }

    private void Update()
    {
        //fade in
        if (Time.time < minimumLogoTime)
        {
            fadeGroup.alpha = 1 - Time.time;
        }

        //fade out
        if (Time.time > minimumLogoTime && loadTime != 0)
        {
            fadeGroup.alpha = Time.time - minimumLogoTime;
            if (fadeGroup.alpha >= 1)
            {
                SceneManager.LoadScene("StepScreen");
            }
        }
    }
}
