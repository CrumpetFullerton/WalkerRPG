/*Tooltip.cs
 * Created By: Phillip Buckreis 11/4/17
 * 
 * This script lets the player select which zone they're in.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private Item item;
    private string data;
    private GameObject tooltip;
    private Vector3 toolTipLoc = new Vector3(0, 0, 0);
    private bool show = false;
    private bool frameDelay = false;

    void Start()
    {
        tooltip = GameObject.Find("Tooltip");
        tooltip.SetActive(false);
    }

    public void Activate(Item item)
    {
        this.item = item;
        show = true;
    }

    public void Update()
    {
        if (show)
        {
            ConstructDataString();

            if (frameDelay)
            {
                tooltip.SetActive(true);

                if (Input.mousePosition.x > 550.0f)
                    toolTipLoc.x = 550.0f;
                else
                    toolTipLoc.x = Input.mousePosition.x;

                if (Input.mousePosition.y < tooltip.GetComponent<RectTransform>().rect.height)
                    toolTipLoc.y = tooltip.GetComponent<RectTransform>().rect.height;
                else
                    toolTipLoc.y = Input.mousePosition.y;

                toolTipLoc.z = Input.mousePosition.z;

                tooltip.transform.position = toolTipLoc;
            }
            frameDelay = true;
        }
    }

    public void Deactivate()
    {
        tooltip.SetActive(false);
        show = false;
    }


    private void ConstructDataString()
    {
        if (item.Rarity == 0)
            data = "<color=#FFFFFF><b>" + item.Title + "</b></color>";
        else if (item.Rarity == 1)
            data = "<color=#60C468><b>" + item.Title + "</b></color>";
        else if (item.Rarity == 2)
            data = "<color=#BF48AF><b>" + item.Title + "</b></color>";

        data = data + "\n<color=#FFFFFF><size=18>Power: " + item.Power + "</size>\n\nAP Cost: " + item.APCost + "\nStrength: " + item.Strength;

        if (item.Element1 != ItemDatabase.Element.NONE)
            data = data + "\n\nElement:\n" + item.Element1;
        if (item.Element1 != ItemDatabase.Element.NONE)
            data = data + " - " + item.Element2;

        data = data + "\n\n\"" + item.Description + "\"</color>";

        tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
    }
}
