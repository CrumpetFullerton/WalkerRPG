/* Inventory.cs
 * Phillip Buckreis 11/4/2017
 * 
 * This creates the front end representation of the inventory system.
 * 
 * Last Modified 11/11/2017
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    ItemDatabase database;
    public Inventory inv;
    public Item itemToAdd;
    public bool equipmentSelect;

    //We need to find the panels to place our prefabs
    GameObject inventoryPanel;
    GameObject slotPanel;

    //Text to display the player's Power Level
    GameObject PowerLevelText;

    //We need to give this object prefabs for the slot and item
    public GameObject inventorySlot;
    public GameObject inventoryItem;

    //Amount of items
    private int slotAmount;

    //List of slots that will contain items
    public List<GameObject> slots = new List<GameObject>();


    void Start()
    {
        //find the player's inventory
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();

        //get a reference to the item database
        database = GetComponent<ItemDatabase>();

        //sets amount of slots (5x6)
        slotAmount = 30;

        //get the inventory and slot panels
        inventoryPanel = GameObject.Find("InventoryPanel");
        slotPanel = inventoryPanel.transform.Find("SlotPanel").gameObject;

        //creates slotAmount empty inventory slots
        for (int i = 0; i < slotAmount; i++)
        {
            //Add a new slot prefab
            slots.Add(Instantiate(inventorySlot));
            slots[i].GetComponent<ItemSlot>().id = i;
            slots[i].transform.SetParent(slotPanel.transform);
        }

        //search through inventory to add to front end representation
        for (int i = 0; i < inv.items.Count; i++)
        {
            if (inv.items[i].ID != -1)
            {
                //get the item information from the inventory
                itemToAdd = inv.items[i];

                //front end representation
                //

                //Create a new item prefab
                GameObject itemObj = Instantiate(inventoryItem);

                //Give this prefab information from the actual item
                itemObj.GetComponent<ItemData>().item = itemToAdd;

                //Make sure you store it's slot
                itemObj.GetComponent<ItemData>().slot = i;

                //orient the prefab into the panels
                itemObj.transform.SetParent(slots[i].transform);
                itemObj.transform.position = Vector2.zero;

                if (GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().SelectorSlot == i)
                    slots[i].GetComponent<Image>().color = new Color32(255, 0, 0, 255);

                //Give the item prefab a sprite from the actual item
                itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;

                if (GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().validEquip(inv.items[i].ItemType) && inv.items[i].Equipped == EquipmentHandler.Equipment.NONE)
                    itemObj.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                else
                    itemObj.GetComponent<Image>().color = new Color32(255, 255, 255, 75);

        //Give the item prefab the approperiate title from the actual item.
        itemObj.name = itemToAdd.Title;
            }
        }

        PowerLevelText = GameObject.Find("txt_PowerLevel");
        PowerLevelText.GetComponent<Text>().text = "◆" + Mathf.FloorToInt(inv.PowerLevel).ToString();
    }

    void Update()
    {
        if (GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().Equipping)
        {
            for (int i = 0; i < inv.items.Count; i++)
            {
                if (inv.items[i].ID != -1)
                {
                    if (GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().SelectorSlot == i)
                    {
                        slots[i].GetComponent<Image>().color = new Color32(255, 0, 0, 255);
                        
                        if (GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().validEquip(inv.items[i].ItemType) && inv.items[i].Equipped == EquipmentHandler.Equipment.NONE)
                            GameObject.Find("btn_Equip").GetComponent<Button>().interactable = true;
                        else
                            GameObject.Find("btn_Equip").GetComponent<Button>().interactable = false;

                    }
                    else
                        slots[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                }
                else
                    slots[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
        }
    }
}
