/*MaxStep.cs
 * Created By: Phillip Buckreis 11/27/17
 * 
 * A cheat to get the maximum amount of steps in the path
 * 
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxStep : MonoBehaviour
{

    public void Clicked()
    {
        GameObject.Find("Zone").GetComponent<ZoneHandler>().CurrentZone.maxStep();
    }

}
