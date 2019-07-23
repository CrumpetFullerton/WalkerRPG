/*PetHandler.cs
 * Created By: Phillip Buckreis 11/25/17
 * 
 * This is the back-end representation of the pet system.
 * 
 * Pets give gold to the player after a certain amount of time
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetHandler : MonoBehaviour
{
    public List<Pet> Pets = new List<Pet>();
    public int unlockedPets = 0;
    private void Awake()
    {
        //When we create the object, make sure it isn't destroyed when we switch scenes.
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        unlockedPets = GameObject.Find("SaveManager").GetComponent<SaveManager>().state.completedLevel;

        //Create pets, to be replaced with a database
        Pet tempPet;

        tempPet = new Pet(0, "Fenrir", 1, 3600, 50, "wolf", false);
        Pets.Add(tempPet);

        tempPet = new Pet(1, "Banshee", 1, 3600, 100, "wolf", false);
        Pets.Add(tempPet);

        tempPet = new Pet(2, "Gobin", 1, 3600, 150, "wolf", false);
        Pets.Add(tempPet);

        tempPet = new Pet(3, "Fairy", 1, 3600, 200, "wolf", false);
        Pets.Add(tempPet);

        tempPet = new Pet(4, "Will-o'-wisp", 1, 7200, 400, "wolf", false);
        Pets.Add(tempPet);

        tempPet = new Pet(5, "Cockatrice", 1, 7200, 450, "wolf", false);
        Pets.Add(tempPet);

        tempPet = new Pet(6, "Lich", 1, 7200, 500, "wolf", false);
        Pets.Add(tempPet);

        tempPet = new Pet(7, "Basilisk", 1, 14400, 1200, "wolf", false);
        Pets.Add(tempPet);

        for (int i = 0; GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedPets[i].Unlocked; i++)
        {
            Pets[i].Unlocked = true;
            Pets[i].Level = GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedPets[i].Level;
            Pets[i].StartSavedTime = GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedPets[i].StartSavedTime;
            Pets[i].NextPayout = GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedPets[i].NextPayout;

            Pets[i].LastSavedTime = System.DateTime.Now.Ticks - Pets[i].StartSavedTime;
            Pets[i].CurrentGain = Mathf.Floor(Pets[i].BaseGain * Mathf.Log10(Pets[i].Level));

            if (Pets[i].LastSavedTime > Pets[i].NextPayout)
            {
                for (; Pets[i].LastSavedTime / Pets[i].NextPayout >= 1; i++)
                {
                    Pets[i].NextPayout = Pets[i].NextPayout + (Pets[i].Timer * Mathf.Pow(10f, 7f));
                    Pets[i].payout();
                    Pets[i].CurrentGain = Mathf.Floor(Pets[i].BaseGain * Mathf.Log10(Pets[i].Level));
                }
            }
        }

        InvokeRepeating("savePets", 1.0f, 10.0f);
    }

    public void unlockPet(int pet)
    {
        //Unlocks pet based on value of pet, make sure pet is never greater than the amount of pets we have.
        if (pet < Pets.Count)
            Pets[pet].Unlocked = true;
    }

    public void Update()
    {
        //Pets are always active, so they need to be checked every frame
        for (int i = 0; i < Pets.Count; i++)
            Pets[i].step();
    }

    public void savePets ()
    {
        for (int i = 0; i < Pets.Count; i++)
        {
            GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedPets[i].Unlocked = Pets[i].Unlocked;
            GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedPets[i].Level = Pets[i].Level;
            GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedPets[i].StartSavedTime = Pets[i].StartSavedTime;
            GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedPets[i].NextPayout = Pets[i].NextPayout;
        }
    }
}

public class Pet
{
    //simple set/get functions
    public int ID { get; set; }
    public string Name { get; set; }
    public float Level { get; set; }
    public float BaseGain { get; set; }
    public float CurrentGain { get; set; }
    public float Timer { get; set; }
    public string Slug { get; set; }
    public bool Unlocked { get; set; }
    public Sprite Sprite { get; set; }
    public double LastSavedTime { get; set; }
    public double StartSavedTime { get; set; }
    public double NextPayout { get; set; }

    //default constructor
    public Pet()
    {
        this.ID = -1;
        this.Level = 1;
    }

    //constructor
    public Pet(int id, string name, float level, float timer, float basegain, string slug, bool unlocked)
    {
        this.ID = id;
        this.Name = name;
        this.Level = level;
        this.Timer = timer;
        this.BaseGain = basegain;
        this.CurrentGain = Mathf.Floor(BaseGain * Mathf.Log10(1.5f)); 
        this.Slug = slug;
        this.Unlocked = unlocked;
        this.Sprite = Resources.Load<Sprite>("Sprites/Pet Icons/" + slug + "_icon");
        this.LastSavedTime = System.DateTime.Now.Ticks;
        this.StartSavedTime = System.DateTime.Now.Ticks;
        this.NextPayout = System.DateTime.Now.AddSeconds(Timer).Ticks;
    }

    public void step()
    {
        //If the Pet is Unlocked, then they either take another step or they level up because they reached their
        //payout time
        if (LastSavedTime <= NextPayout && Unlocked)
        {
            //Take a step
            LastSavedTime = System.DateTime.Now.Ticks;
        }
        else if (Unlocked)
        {
            //New Level
            LastSavedTime = System.DateTime.Now.Ticks;
            StartSavedTime = System.DateTime.Now.Ticks;
            NextPayout = System.DateTime.Now.AddSeconds(Timer).Ticks;

            //Max level is 9999, which will take around 400 - 1200 days depending on the pet's payout rate.
            if (Level < 9999)
                Level += 1;

            payout();

            //Increase the amount of money based on the pet's level
            CurrentGain = Mathf.Floor(BaseGain * Mathf.Log10(Level));
        }
    }


    public void payout()
    {
        //Give the player gold
        GameObject.Find("Inventory").GetComponent<Inventory>().gold += (int)CurrentGain;
        GameObject.Find("SaveManager").GetComponent<SaveManager>().state.gold = GameObject.Find("Inventory").GetComponent<Inventory>().gold;
    }

    public double findPercent()
    {
        //Get percent for front-end representation
        return ((LastSavedTime - StartSavedTime) / (NextPayout - StartSavedTime));
    }

    public string timeLeft()
    {
        //Displays the time left as a string
        float totalSeconds = (float)(NextPayout - LastSavedTime) / 10000000f;

        float hours = Mathf.Floor(totalSeconds / 3600f);
        float minutes = Mathf.Floor((totalSeconds/60f) % 60);
        float seconds = Mathf.Floor(totalSeconds % 60);

        if (hours < 0)
            hours = 0;
        if (minutes < 0)
            minutes = 0;
        if (seconds < 0)
            seconds = 0;

        return (hours).ToString("f0") + ":" + (minutes).ToString("f0") + ":" + (seconds).ToString("f0");
    }
}