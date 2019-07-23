/*PetDisplay.cs
 * Created By: Phillip Buckreis 11/25/17
 * 
 * Creates the front-end representation of our pet system.
 * 
 * Pets give the player gold after a certain amount of time.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PetDisplay : MonoBehaviour {

    //Create the pet list.
    public List<Pet> pets;

    public GameObject PetIcon;
    GameObject mainPanel;
    GameObject petPanel;

    public List<GameObject> petSlots = new List<GameObject>();

    // Use this for initialization
    void Start ()
    {
        //grab the pet object in the scene
        pets = GameObject.Find("PetHandler").GetComponent<PetHandler>().Pets;

        //grab the panel objects in the scene
        mainPanel = GameObject.Find("MainPanel");
        petPanel = mainPanel.transform.Find("PetPanel").gameObject;

        for (int i = 0; i < pets.Count; i++)
        {
            if (pets[i].Unlocked)
            {
                //Add a new prefab
                petSlots.Add(Instantiate(PetIcon));

                petSlots[i].transform.SetParent(petPanel.transform);

                //Get the gameobjects to adjust values based on the pet.
                GameObject slotImage = petSlots[i].transform.Find("img_Slot").gameObject;
                GameObject petIcon = slotImage.transform.Find("img_PetIcon").gameObject;
                GameObject loadingBar = slotImage.transform.Find("img_LoadingBar").gameObject;
                GameObject name = slotImage.transform.Find("txt_Name").gameObject;

                //Get the pet picture
                petIcon.GetComponent<Image>().sprite = pets[i].Sprite;

                //Get the progress bar
                loadingBar.GetComponent<Image>().fillAmount = (float)pets[i].findPercent();

                //get the name
                name.GetComponent<Text>().text = pets[i].Name;
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        //We need to be sure to update certain values that might change while the player is on
        //this screen. Values that can change are the loading bar, the pet's level, and the pet's
        //payout
        for (int i = 0; i < pets.Count; i++)
        {
            if (pets[i].Unlocked)
            {
                //Loading bar
                GameObject slotImage = petSlots[i].transform.Find("img_Slot").gameObject;
                GameObject loadingBar = slotImage.transform.Find("img_LoadingBar").gameObject;

                loadingBar.GetComponent<Image>().fillAmount = (float)pets[i].findPercent();

                //get the level
                GameObject level = slotImage.transform.Find("txt_Level").gameObject;
                level.GetComponent<Text>().text = "Level: " + pets[i].Level.ToString();

                //get the gold amount per competion
                GameObject gold = slotImage.transform.Find("txt_Coins").gameObject;
                gold.GetComponent<Text>().text = pets[i].CurrentGain.ToString();

                //get time left until payout
                GameObject time = slotImage.transform.Find("txt_Time").gameObject;
                time.GetComponent<Text>().text = pets[i].timeLeft();
            }

        }
            
    }
}
