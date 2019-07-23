/* ItemDatabase.cs
 * Phillip Buckreis 11/4/2017
 * 
 * This creates an item database that we can use in our unity project.
 * We read in item.json to fill our database.
 * 
 * These base items are later modified by our item class.
 * 
 * Last Modified: 11/20/2017
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;



public class ItemDatabase : MonoBehaviour
{
    private List<BaseItem> database = new List<BaseItem>();
    private JsonData itemData;

    //The different types of weapons
    public enum ItemType
    {
        WEAPON,
        HEAD,
        BODY,
        LEGS
    }

    //These are the elements of our game. Weapon deals the damage. Armors protect from the damage.
    public enum Element
    {
        NONE,
        SLASH,
        PIERCE,
        BLUNT,
        FIRE,
        WATER,
        EARTH,
        LIGHT,
        DARK
    }

    //Status effects weapons deal or protect against
    public enum Status
    {
        NONE,
        BLEED,
        DISARM,
        CURSE,
        BURN,
        STUN
    }

    void Start()
    {
        //Find the json file
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/items.json"));

        //creaate database
        ConstructItemDatabase();
    }

    public BaseItem FetchItemByID(int id)
    {
        for (int i = 0; i < database.Count; i++)
        {
            if (database[i].ID == id)
                return database[i];
        }

        return null;
    }

    void ConstructItemDatabase()
    {
        //read in from the json database
        for (int i = 0; i < itemData.Count; i++)
        {
            database.Add(new BaseItem(
                (int)itemData[i]["id"],
                itemData[i]["title"].ToString(),
                Helper.ParseEnum<ItemDatabase.ItemType>((itemData[i]["type"]).ToString()),
                (int)itemData[i]["stats"]["powerReq"],
                (int)itemData[i]["stats"]["strength"],
                (int)itemData[i]["stats"]["critical"],
                (int)itemData[i]["stats"]["apCost"],
                Helper.ParseEnum<ItemDatabase.Element>((itemData[i]["stats"]["element1"]).ToString()),
                Helper.ParseEnum<ItemDatabase.Element>((itemData[i]["stats"]["element2"]).ToString()),
                Helper.ParseEnum<ItemDatabase.Status>((itemData[i]["stats"]["status"]).ToString()),
                (int)itemData[i]["stats"]["statProc"],
                itemData[i]["description"].ToString(),
                (int)itemData[i]["value"],
                itemData[i]["slug"].ToString()
                ));
        }
    }
}

public class BaseItem
{
    //simple set/get functions
    public int ID { get; set; }
    public string Title { get; set; }
    public ItemDatabase.ItemType ItemType { get; set; }
    public int PowerReq { get; set; }
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

    //default constructor, give unqiue id
    public BaseItem()
    {
        this.ID = -1;
    }

    //constructor
    public BaseItem(int id, string title, ItemDatabase.ItemType itemType, int powerReq, int strength, int critical, int apCost, ItemDatabase.Element element1, ItemDatabase.Element element2, ItemDatabase.Status status, int statProc, string description, int value, string slug)
    {
        this.ID = id;
        this.Title = title;
        this.ItemType = itemType;
        this.PowerReq = powerReq;
        this.Strength = strength;
        this.Critical = critical;
        this.APCost = apCost;
        this.Element1 = element1;
        this.Element2 = element2;
        this.Status= status;
        this.StatProc = statProc;
        this.Description = description;
        this.Value = value;
        this.Slug = slug;
        this.Sprite = Resources.Load<Sprite>("Sprites/Item Icons/" + slug);
    }
}
