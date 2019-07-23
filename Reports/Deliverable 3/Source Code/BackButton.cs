/*BackButton.cs
 * Created By: Phillip Buckreis 11/5/17
 * 
 * Simple script that returns the player to the step screen when the button is pushed.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public void Clicked()
    {
        //Load the step screen
        SceneManager.LoadScene("StepScreen");
    }
}
