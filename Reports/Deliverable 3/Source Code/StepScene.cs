using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepScene : MonoBehaviour
{
    private CanvasGroup fadeGroup;
    private float fadeInSpeed = 0.75f;

    private void Start()
    {
        //get the fading graphic
        fadeGroup = FindObjectOfType<CanvasGroup>();
        fadeGroup.alpha = 1.0f;
    }

    private void Update()
    {
        //fade in
        fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;
    }
}
