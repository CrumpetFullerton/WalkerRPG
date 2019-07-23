/*NextPath.cs
 * Created By: Phillip Buckreis 11/27/17
 * 
 * A cheat to go to the next path
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPath : MonoBehaviour
{

    public void Clicked()
    {
        GameObject.Find("Zone").GetComponent<ZoneHandler>().CurrentZone.nextPath();
    }

}