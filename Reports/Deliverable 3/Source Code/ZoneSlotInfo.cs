/*ZoneSlotInfo.cs
 * Created By: Phillip Buckreis 11/11/17
 * 
 * This script lets the player select which zone they're in.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZoneSlotInfo : MonoBehaviour, IPointerClickHandler
{
    public int id;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameObject.Find("Zone").GetComponent<ZoneHandler>().Zones[id].Unlocked)
        {
            GameObject.Find("Zone").GetComponent<ZoneHandler>().changeZone(id);
            GameObject.Find("SaveManager").GetComponent<SaveManager>().state.currentZone = id;
        }
    }
}