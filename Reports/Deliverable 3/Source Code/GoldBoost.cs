/*GoldBoost.cs
 * Created By: Phillip Buckreis 11/27/17
 * 
 * A cheat to get a ton of gold
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBoost : MonoBehaviour
{

    public void Clicked()
    {
        GameObject.Find("Inventory").GetComponent<Inventory>().gold = 99999999;
    }

}