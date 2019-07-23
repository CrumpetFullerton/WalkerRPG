using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScreen : MonoBehaviour
{
     public void ChangeScene(string SceneName)
     {
         Application.LoadLevel(SceneName);
     }
}