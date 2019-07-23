using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Step : MonoBehaviour
{
    public UnityEngine.UI.Text stepDisplay;
    public UnityEngine.UI.Text currencyDisplay;
    public static int steps = 0;
    private bool encounterActive = false;
    private PedometerPlugin pedometer;
    private SensorDelay sensor;

    void Start()
    {
        //Get steps from save file
        steps = GameObject.Find("SaveManager").GetComponent<SaveManager>().state.steps;

        //Gets instance
        pedometer = PedometerPlugin.GetInstance();

        //Hides debug messages when set to zero
        pedometer.SetDebug(0);

        //Initaialize
        pedometer.Init();
        pedometer.StartPedometerService(sensor);
        
    }

    void Update()
    {
        //If the player didn't run into an encounter, update the step text
        if (!encounterActive)
        {
            pedometer.LoadStepToday();
            int newSteps = pedometer.GetStepToday();
            if (steps < newSteps)
                steps += (newSteps - steps);
            stepDisplay.text = "" + steps;
            currencyDisplay.text = GameObject.Find("Inventory").GetComponent<Inventory>().gold.ToString();
            
        }   
    }

    //Function for step button, to be replaced with actual walking at a later date.
    public void Clicked()
    {
        encounterActive = Encounter();

        if (!encounterActive)
        {
            //Add step to the total step counter and save state.
            steps += 1;
            GameObject.Find("SaveManager").GetComponent<SaveManager>().state.steps++;
            GameObject.Find("SaveManager").GetComponent<SaveManager>().state.zoneSteps[GameObject.Find("Zone").GetComponent<ZoneHandler>().CurrentZone.ID]++;

            //Add step to the Zone and Path counter that's stored in another object
            GameObject.Find("Zone").GetComponent<ZoneHandler>().CurrentZone.addStep();

            //Unlock next zone and pet if you complete the 10th path
            if (GameObject.Find("Zone").GetComponent<ZoneHandler>().CurrentZone.Path == 11)
            {
                //bound check
                if (GameObject.Find("Zone").GetComponent<ZoneHandler>().CurrentZone.ID < GameObject.Find("Zone").GetComponent<ZoneHandler>().Zones.Count - 1)
                {
                    GameObject.Find("PetHandler").GetComponent<PetHandler>().unlockPet(GameObject.Find("Zone").GetComponent<ZoneHandler>().CurrentZone.ID);
                    GameObject.Find("Zone").GetComponent<ZoneHandler>().unlockZone((GameObject.Find("Zone").GetComponent<ZoneHandler>().CurrentZone.ID) + 1);
                    GameObject.Find("SaveManager").GetComponent<SaveManager>().state.completedLevel = GameObject.Find("Zone").GetComponent<ZoneHandler>().CurrentZone.ID + 1;
                }
            }

            //Every 50 steps will add one currency.
            if (steps % 50 == 0)
            {
                GameObject.Find("Inventory").GetComponent<Inventory>().gold++;
                GameObject.Find("SaveManager").GetComponent<SaveManager>().state.gold = GameObject.Find("Inventory").GetComponent<Inventory>().gold;
            }
        }
        else
            //Temporary. To be replaced with a function that goes to encounter screen
            Application.LoadLevel("graveyard");

    }
    
    public bool Encounter()
    {
        //The player as a 1/100 chance of encountering an enemy every step they take.
        if (Mathf.Ceil(UnityEngine.Random.Range(0.0f, 100.0f)) == 1)
            return true;
        else
            return false;
    }

}
