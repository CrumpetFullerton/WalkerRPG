/* Inventory.cs
 * Phillip Buckreis 11/4/2017
 * 
 * This creates the backend of the inventory system
 * 
 * Last Modified: 11/25/17
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //initialize the item database
    ItemDatabase database;

    //store the player's gold
    public int gold = 0;

    //The number of items a player can hold at any given time
    private int slotAmount;

    //The inventory
    public List<Item> items = new List<Item>();

    public float PowerLevel;

    private void Awake()
    {
        //Make sure that the game object goes from one scene to the next
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        //get a reference to the item database
        database = GetComponent<ItemDatabase>();

        //sets amount of slots (5x6)
        slotAmount = 30;

        //creates "slotAmount" empty inventory slots
        for (int i = 0; i < slotAmount; i++)
        {
            items.Add(new Item());
        }

        //Loads data from the savefile. If the ID is -1, then there is nothing in the save file.
        for (int i = 0; i < GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems.Length; i++)
        {
            if (GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].ID != -1)
            {
                //create item from the savefile
                Item itemToAdd = new Item(
                    GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].ID,
                    GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].Title,
                    (ItemDatabase.ItemType)GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].ItemType,
                    GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].Power,
                    GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].Strength,
                    GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].Critical,
                    GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].APCost,
                    (ItemDatabase.Element)GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].Element1,
                    (ItemDatabase.Element)GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].Element2,
                    (ItemDatabase.Status)GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].Status,
                    GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].StatProc,
                    GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].Description,
                    GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].Value,
                    GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].Slug);

                itemToAdd.Rarity = GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].Rarity;

                items[GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].position] = itemToAdd;
            }
        }

        //Get gold from save file
        gold = GameObject.Find("SaveManager").GetComponent<SaveManager>().state.gold;

        //Get PowerLevel from save file
        getPowerLevel();
        //PowerLevel = GameObject.Find("SaveManager").GetComponent<SaveManager>().state.PowerLevel;
        Debug.Log(PowerLevel);
    }

    public void AddItem(int id)
    {
        //Attempt to get item by ID from item database
        BaseItem baseItem = new BaseItem();
        baseItem = database.FetchItemByID(id);

        Item itemToAdd = new Item(baseItem.ID, baseItem.Title, baseItem.ItemType, baseItem.PowerReq, baseItem.Strength, baseItem.Critical, baseItem.APCost, baseItem.Element1, baseItem.Element2, baseItem.Status, baseItem.StatProc, baseItem.Description, baseItem.Value, baseItem.Slug);

        itemToAdd.randomizeItem(PowerLevel);

        //search through inventory to see if we have room for item
        for (int i = 0; i < items.Count; i++)
        {
            //if we find an empty slot, then we have room for item
            if (items[i].ID == -1)
            {
                //back end representation
                items[i] = itemToAdd;
                break;
            }
        }
    }

    public void SaveItems()
    {
        //Zero out the savefile, so that we can replace it with the new savefile.
        for (int i = 0; i < items.Count; i++)
        {
            GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].ID = -1;
        }

        //Save inventory to savefile
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID != -1)
            {
                GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].ID = items[i].ID;
                GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].Title = items[i].Title;
                GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].ItemType = (int)items[i].ItemType;
                GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].Rarity = items[i].Rarity;
                GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].Power = items[i].Power;
                GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].Strength = items[i].Strength;
                GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].Critical = items[i].Critical;
                GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].APCost = items[i].APCost;
                GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].Element1 = (int)items[i].Element1;
                GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].Element2 = (int)items[i].Element2;
                GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].Status = (int)items[i].Status;
                GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].StatProc = items[i].StatProc;
                GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].Description = items[i].Description;
                GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].Value = items[i].Value;
                GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].Slug = items[i].Slug;
                GameObject.Find("SaveManager").GetComponent<SaveManager>().state.savedItems[i].position = i;
            }
        }

        GameObject.Find("SaveManager").GetComponent<SaveManager>().state.gold = gold;
        GameObject.Find("SaveManager").GetComponent<SaveManager>().state.PowerLevel = PowerLevel;
    }

    public void getPowerLevel()
    {
        int[] TopWeapon = { 1, 1, 1 };
        int TopHead = 1;
        int TopBody = 1;
        int TopLegs = 1;

        for (int i = 0; i < items.Count; i++)
        {
            switch (items[i].ItemType)
            {
                case ItemDatabase.ItemType.WEAPON:
                    for (int j = 0; j < 3; j++)
                    {
                        if (items[i].Power >= TopWeapon[j])
                        {
                            TopWeapon[j] = items[i].Power;
                            break;
                        }
                    }
                    break;

                case ItemDatabase.ItemType.BODY:
                    if (items[i].Power >= TopBody)
                    {
                        TopBody = items[i].Power;
                    }
                    break;

                case ItemDatabase.ItemType.HEAD:
                    if (items[i].Power >= TopHead)
                    {
                        TopHead = items[i].Power;
                    }
                    break;

                case ItemDatabase.ItemType.LEGS:
                    if (items[i].Power >= TopLegs)
                    {
                        TopLegs = items[i].Power;
                    }
                    break;
            }
        }

        if (PowerLevel < (TopLegs + TopHead + TopBody + TopWeapon[0] + TopWeapon[1] + TopWeapon[2]) / 6.0f)
            PowerLevel = (TopLegs + TopHead + TopBody + TopWeapon[0] + TopWeapon[1] + TopWeapon[2])/6.0f;

        GameObject.Find("SaveManager").GetComponent<SaveManager>().state.PowerLevel = PowerLevel;
    }
}

public class Item
{
    //simple set/get functions
    public int ID { get; set; }
    public string Title { get; set; }
    public ItemDatabase.ItemType ItemType { get; set; }
    public int Rarity { get; set; }
    public int Power { get; set; }
    public int Strength { get; set; }
    public int Critical { get; set; }
    public int APCost { get; set; }
    public ItemDatabase.Element Element1 { get; set; }
    public ItemDatabase.Element Element2 { get; set; }
    public ItemDatabase.Status Status { get; set; }
    public int StatProc { get; set; }
    public string Description { get; set; }
    public int Value { get; set; }
    public string Slug { get; set; }
    public Sprite Sprite { get; set; }
    public EquipmentHandler.Equipment Equipped { get; set; }

    //default constructor, give unqiue id
    public Item()
    {
        this.ID = -1;
    }

    //constructor
    public Item(int id, string title, ItemDatabase.ItemType itemType, int power, int strength, int critical, int apCost, ItemDatabase.Element element1, ItemDatabase.Element element2, ItemDatabase.Status status, int statProc, string description, int value, string slug)
    {
        this.ID = id;
        this.Title = title;
        this.ItemType = itemType;
        this.Rarity = 0;
        this.Power = power;
        this.Strength = strength;
        this.Critical = critical;
        this.APCost = apCost;
        this.Element1 = element1;
        this.Element2 = element2;
        this.Status = status;
        this.StatProc = statProc;
        this.Description = description;
        this.Value = value;
        this.Slug = slug;
        this.Sprite = Resources.Load<Sprite>("Sprites/Item Icons/" + slug);
        this.Equipped = EquipmentHandler.Equipment.NONE;
    }

    public void randomizeItem(float PowerLevel)
    {

        //First, we roll to find if it's a Common, Rare, Legendary
        float itemRoll = Random.Range(1.0f, 100.0f);

        //If the item is from 1 - 50, the item is common and it returns the base item with only Power Level modifiers.
        if (itemRoll < 51)
        {
            //This will cause the weapon title to be a different color in Tooltip, 0 means the item is common
            Rarity = 0;

            //This will change the power level of the item. The player's strength is determined by Power. Common items likely give crappy Power.
            //In the future, we will need to get the player's average power level so he can gradually increase power with equipment.
            Power += Mathf.CeilToInt(Random.Range(-5.0f, 1.0f)) + Mathf.FloorToInt(PowerLevel);
            if (Power < 1)
                Power = 1;

            //This is the actual damage of the item. Each weapon element: Slash, Pierce, Blunt, has three weapon types: light, medium, heavy. A light weapon only
            //spends 1AP, medium is 2AP, and heavy is 3AP. A light weapon can have the same "Power" as a heavy weapon, but it will still deal less damage. Light
            //weapons have a higher critical, medium weapons have the best status proc and heavy weapons deal the most raw, predictable damage.
            Strength = Mathf.CeilToInt(Strength * Random.Range(0.75f, 1.5f) * (PowerLevel * 0.2f));

        }
        //if the item is from 51 - 85, the item is rare and it returns the base with Power Level modifiers and one pre/post-fix.
        if (itemRoll >= 51 && itemRoll < 86)
        {
            //This will cause the weapon title to be a different color in Tooltip, 1 means item is rare
            Rarity = 1;

            //finds a value 0 - 1
            float roll = Random.value;

            //if the value is < 0.5, then set a prefix.
            if (roll < 0.5)
            {
                int roll2 = Mathf.CeilToInt(Random.Range(0.0f, 4.0f));

                switch (roll2)
                {
                    case 0:
                    case 1:
                        if (Element1 == ItemDatabase.Element.SLASH)
                            Title = "Sharp " + Title;
                        if (Element1 == ItemDatabase.Element.PIERCE)
                            Title = "Sharp " + Title;
                        if (Element1 == ItemDatabase.Element.BLUNT)
                            Title = "Heavy " + Title;

                        Strength *= Mathf.CeilToInt(Random.Range(1.5f, 2.0f));
                        break;

                    case 2:
                        Title = "Deft " + Title;

                        if (APCost > 1)
                            APCost -= 1;
                        else
                            Critical += 10;
                        break;

                    case 3:
                        Title = "Quality " + Title;

                        Strength *= Mathf.CeilToInt(Random.Range(1.0f, 1.5f));
                        break;

                    case 4:
                        Title = "Sloppy " + Title;

                        Strength *= Mathf.CeilToInt(0.5f);
                        Rarity -= 1;
                        break;

                }
            }
            else
            {
                int roll2 = Mathf.CeilToInt(Random.Range(0.0f, 5.0f));

                switch (roll2)
                {
                    case 0:
                    case 1:
                        Title = Title + " of Fire";

                        if (Element1 != ItemDatabase.Element.NONE)
                            Element2 = ItemDatabase.Element.FIRE;
                        else
                            Element1 = ItemDatabase.Element.FIRE;
                        break;

                    case 2:
                        Title = Title + " of Water";

                        if (Element1 != ItemDatabase.Element.NONE)
                            Element2 = ItemDatabase.Element.WATER;
                        else
                            Element1 = ItemDatabase.Element.WATER;
                        break;

                    case 3:
                        Title = Title + " of Earth";

                        if (Element1 != ItemDatabase.Element.NONE)
                            Element2 = ItemDatabase.Element.EARTH;
                        else
                            Element1 = ItemDatabase.Element.EARTH;
                        break;

                    case 4:
                        Title = Title + " of Evil";

                        if (Element1 != ItemDatabase.Element.NONE)
                            Element2 = ItemDatabase.Element.DARK;
                        else
                            Element1 = ItemDatabase.Element.DARK;
                        break;

                    case 5:
                        Title = Title + " of Bliss";

                        if (Element1 != ItemDatabase.Element.NONE)
                            Element2 = ItemDatabase.Element.LIGHT;
                        else
                            Element1 = ItemDatabase.Element.LIGHT;
                        break;

                }
            }

            //This will change the power level of the item. The player's strength is determined by Power. Rare items likely give okay Power.
            //In the future, we will need to get the player's average power level so he can gradually increase power with equipment.
            Power += Mathf.CeilToInt(Random.Range(-2.0f, 2.0f)) + Mathf.FloorToInt(PowerLevel);

            if (Power < 1)
                Power = 1;

            //This is the actual damage of the item. Each weapon element: Slash, Pierce, Blunt, has three weapon types: light, medium, heavy. A light weapon only
            //spends 1AP, medium is 2AP, and heavy is 3AP. A light weapon can have the same "Power" has a heavy weapon, but it will still deal less damage. Light
            //weapons have a higher critical, medium weapons have the best status proc and heavy weapons deal the most raw, predictable damage.
            Strength = Mathf.CeilToInt(Strength * Random.Range(1.0f, 1.25f) * (PowerLevel * 0.2f));
        }
        //if the item is from 86 - 100, the item is Legendary and it returns the base with Power Level modifiers, a prefix and a postfix.
        if (itemRoll >= 86)
        {
            //This will cause the weapon title to be a different color in Tooltip, 2 means item is legendary
            Rarity = 2;

            //finds a value 0 - 1
            float roll = Random.value;

                int roll2 = Mathf.CeilToInt(Random.Range(0.0f, 4.0f));

                switch (roll2)
                {
                    case 0:
                    case 1:
                        if (Element1 == ItemDatabase.Element.SLASH)
                            Title = "Sharp " + Title;
                        if (Element1 == ItemDatabase.Element.PIERCE)
                            Title = "Sharp " + Title;
                        if (Element1 == ItemDatabase.Element.BLUNT)
                            Title = "Heavy " + Title;

                        Strength *= Mathf.CeilToInt(Random.Range(1.5f, 2.0f));
                        break;

                    case 2:
                        Title = "Light " + Title;

                        if (APCost > 1)
                            APCost -= 1;
                        else
                            Critical += 10;
                        break;

                    case 3:
                        Title = "Quality " + Title;

                        Strength *= Mathf.CeilToInt(Random.Range(1.0f, 1.5f));
                        break;

                    case 4:
                        Title = "Sloppy " + Title;

                        Strength *= Mathf.CeilToInt(0.5f);
                        Rarity -= 1;
                        break;

                }

                roll2 = Mathf.CeilToInt(Random.Range(0.0f, 5.0f));

                switch (roll2)
                {
                    case 0:
                    case 1:
                        Title = Title + " of Fire";

                        if (Element1 != ItemDatabase.Element.NONE)
                            Element2 = ItemDatabase.Element.FIRE;
                        else
                            Element1 = ItemDatabase.Element.FIRE;
                        break;

                    case 2:
                        Title = Title + " of Water";

                        if (Element1 != ItemDatabase.Element.NONE)
                            Element2 = ItemDatabase.Element.WATER;
                        else
                            Element1 = ItemDatabase.Element.WATER;
                        break;

                    case 3:
                        Title = Title + " of Earth";

                        if (Element1 != ItemDatabase.Element.NONE)
                            Element2 = ItemDatabase.Element.EARTH;
                        else
                            Element1 = ItemDatabase.Element.EARTH;
                        break;

                    case 4:
                        Title = Title + " of Evil";

                        if (Element1 != ItemDatabase.Element.NONE)
                            Element2 = ItemDatabase.Element.DARK;
                        else
                            Element1 = ItemDatabase.Element.DARK;
                        break;

                    case 5:
                        Title = Title + " of Bliss";

                        if (Element1 != ItemDatabase.Element.NONE)
                            Element2 = ItemDatabase.Element.LIGHT;
                        else
                            Element1 = ItemDatabase.Element.LIGHT;
                        break;

                }

            //This will change the power level of the item. The player's strength is determined by Power. Rare items likely give okay Power.
            //In the future, we will need to get the player's average power level so he can gradually increase power with equipment.
            Power += Mathf.CeilToInt(Random.Range(1.0f, 4.0f)) + Mathf.FloorToInt(PowerLevel);

            //This is the actual damage of the item. Each weapon element: Slash, Pierce, Blunt, has three weapon types: light, medium, heavy. A light weapon only
            //spends 1AP, medium is 2AP, and heavy is 3AP. A light weapon can have the same "Power" has a heavy weapon, but it will still deal less damage. Light
            //weapons have a higher critical, medium weapons have the best status proc and heavy weapons deal the most raw, predictable damage.
            Strength = Mathf.CeilToInt(Strength * Random.Range(1.5f, 2.0f) * (PowerLevel * 0.2f)); 
        }
    }
}
