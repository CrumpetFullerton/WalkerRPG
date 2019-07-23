/*ZoneHandler.cs
 * Created By: Phillip Buckreis 11/11/17
 * 
 * This is the backend representation of the zones.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneHandler : MonoBehaviour
{
    public Zone CurrentZone;
    public List<Zone> Zones = new List<Zone>();
    public int unlockedZones = 0;
    private void Awake()
    {
        //When we create the object, make sure it isn't destroyed when we switch scenes.
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        unlockedZones = GameObject.Find("SaveManager").GetComponent<SaveManager>().state.completedLevel;

        //Create zones, to be replaced with a database and a loop
        Zone tempZone;
        
        tempZone= new Zone(0, 100.0f, 1.15f, "forest_zone", true);
        Zones.Add(tempZone);

        tempZone = new Zone(1, 200.0f, 1.12f, "graveyard_zone", false);
        Zones.Add(tempZone);

        tempZone = new Zone(2, 400.0f, 1.09f, "three_zone", false);
        Zones.Add(tempZone);

        tempZone = new Zone(3, 800.0f, 1.06f, "four_zone", false);
        Zones.Add(tempZone);

        tempZone = new Zone(4, 1600.0f, 1.03f, "five_zone", false);
        Zones.Add(tempZone);

        tempZone = new Zone(5, 3200.0f, 1.02f, "six_zone", false);
        Zones.Add(tempZone);

        tempZone = new Zone(6, 5000.0f, 1.005f, "seven_zone", false);
        Zones.Add(tempZone);

        tempZone = new Zone(7, 7500.0f, 1.003f, "eight_zone", false);
        Zones.Add(tempZone);

        tempZone = new Zone(8, 10000.0f, 1.002f, "nine_zone", false);
        Zones.Add(tempZone);

        for (int i = 0; i <= unlockedZones; i++)
        {
            Zones[i].Unlocked = true;

        }

        for (int i = 0; i < Zones.Count; i++)
        {
            Zones[i].Path = GameObject.Find("SaveManager").GetComponent<SaveManager>().state.zonePaths[i];

        }
        for (int i = 0; i < Zones.Count; i++)
        {
            Zones[i].StepsInCurrentPath = GameObject.Find("SaveManager").GetComponent<SaveManager>().state.zoneSteps[i];
        }

        //Starting zone
        changeZone(GameObject.Find("SaveManager").GetComponent<SaveManager>().state.currentZone);
    }

    public void changeZone(int changeTo)
    {
        //Change zone
        CurrentZone = Zones[changeTo];
    }

    public void unlockZone(int zone)
    {
        Zones[zone].Unlocked = true;
    }
}

public class Zone
{
    //simple set/get functions
    public int ID { get; set; }
    public int Path { get; set; }
    public float InitialStepRequirement { get; set; }
    public float StepsInCurrentPath { get; set; }
    public float StepsUntilNextPath { get; set; }
    public float Growth { get; set; }
    public string Slug { get; set; }
    public bool Unlocked { get; set; }
    public Sprite Sprite { get; set; }

    //default constructor, give strange id
    public Zone()
    {
        this.ID = -1;
        this.Path = 1;
        this.StepsInCurrentPath = 0;
    }

    //constructor
    public Zone(int id, float initialStepRequirement, float growth, string slug, bool unlocked)
    {
        this.ID = id;
        this.Path = GameObject.Find("SaveManager").GetComponent<SaveManager>().state.zonePaths[ID];
        this.InitialStepRequirement = initialStepRequirement;
        this.StepsInCurrentPath = GameObject.Find("SaveManager").GetComponent<SaveManager>().state.zoneSteps[ID];
        this.Growth = growth;
        this.StepsUntilNextPath = Mathf.Ceil(initialStepRequirement * Mathf.Pow(Growth, Path - 1));
        this.Slug = slug;
        this.Unlocked = unlocked;
        this.Sprite = Resources.Load<Sprite>("Sprites/Zone Icons/" + slug);
    }

    public void addStep()
    {
        StepsInCurrentPath++;

        if (StepsInCurrentPath > StepsUntilNextPath)
        {
            increasePath();
            GameObject.Find("SaveManager").GetComponent<SaveManager>().state.zonePaths[ID]++;
        }
    }
    public void maxStep()
    {
        StepsInCurrentPath = StepsUntilNextPath;

        if (StepsInCurrentPath > StepsUntilNextPath)
        {
            increasePath();
            GameObject.Find("SaveManager").GetComponent<SaveManager>().state.zonePaths[ID]++;
        }
    }

    public void nextPath()
    {
        increasePath();
    }

    public void increasePath()
    {
        StepsInCurrentPath = 0;
        GameObject.Find("SaveManager").GetComponent<SaveManager>().state.zoneSteps[ID] = 0;
        Path++;

        //Growth is expotential, beginning zones have a low initial step requirement, but faster growth,
        //while later zones have high initial step requirements, but slower growth
        StepsUntilNextPath = Mathf.Ceil(InitialStepRequirement * Mathf.Pow(Growth, Path - 1));
    }


    public float findPercent()
    {
        return (StepsInCurrentPath / StepsUntilNextPath) * 100;
    }

}