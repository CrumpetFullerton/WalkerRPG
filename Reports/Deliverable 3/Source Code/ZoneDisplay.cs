/*ZoneDisplay.cs
 * Created By: Phillip Buckreis 11/11/17
 * 
 * This is the front end representation of the zones
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ZoneDisplay : MonoBehaviour {

    public List<Zone> zones;

    public GameObject ZoneIcon;
    GameObject mainPanel;
    GameObject zonePanel;

    public List<GameObject> zoneSlots = new List<GameObject>();

    // Use this for initialization
    void Start ()
    {
        //grab the zone object in the scene
        zones = GameObject.Find("Zone").GetComponent<ZoneHandler>().Zones;

        //grab the panel objects in the scene
        mainPanel = GameObject.Find("MainPanel");
        zonePanel = mainPanel.transform.Find("ZonePanel").gameObject;

        for (int i = 0; i < zones.Count; i++)
        {
            zoneSlots.Add(Instantiate(ZoneIcon));

            //Get the gameobjects to adjust values based on the zone.
            GameObject center = zoneSlots[i].transform.Find("Center").gameObject;
            GameObject percentText = center.transform.Find("txt_percent").gameObject;
            GameObject zonePathText = zoneSlots[i].transform.Find("ZonePath").gameObject;
            GameObject untilNextPathText = center.transform.Find("txt_pathTotal").gameObject;
            GameObject loadingBar = zoneSlots[i].transform.Find("LoadingBar").gameObject;
            GameObject selector = zoneSlots[i].transform.Find("Selector").gameObject;

            zoneSlots[i].transform.SetParent(zonePanel.transform);
            zoneSlots[i].GetComponent<ZoneSlotInfo>().id = i;

            //Get the center picture right
            if (zones[i].Unlocked)
            {
                center.GetComponent<Image>().sprite = zones[i].Sprite;

                //Get the progress bar
                loadingBar.GetComponent<Image>().fillAmount = zones[i].findPercent() / 100.0f;

                //Get Percent text
                percentText.GetComponent<Text>().text = zones[i].findPercent().ToString("F1") + "%";

                //Get the steps until next path text 
                untilNextPathText.GetComponent<Text>().text = zones[i].StepsUntilNextPath.ToString();

                //Get text for the Zone - Path
                zonePathText.GetComponent<Text>().text = (zones[i].ID + 1) + " - " + zones[i].Path;

                //Is object selected? Activate Selector
                if (zones[i] == GameObject.Find("Zone").GetComponent<ZoneHandler>().CurrentZone)
                    selector.SetActive(true);
                else
                    selector.SetActive(false);
            }
            else
            {
                center.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Zone Icons/lockedzoneicon");

                //Get the progress bar
                loadingBar.GetComponent<Image>().fillAmount = 0;

                //Get Percent text
                percentText.GetComponent<Text>().text = " ";

                //Get the steps until next path text 
                untilNextPathText.GetComponent<Text>().text = " ";

                //Get text for the Zone - Path
                zonePathText.GetComponent<Text>().text = " ";
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Search through all the zones
        for (int i = 0; i < zones.Count; i++)
        {
            //Find the selector, selector highlights the zone you're currently in.
            GameObject selector = zoneSlots[i].transform.Find("Selector").gameObject;

            //Is zone selected? Activate Selector
            if (zones[i] == GameObject.Find("Zone").GetComponent<ZoneHandler>().CurrentZone)
                selector.SetActive(true);
            else
                selector.SetActive(false);
        }

    }
}
