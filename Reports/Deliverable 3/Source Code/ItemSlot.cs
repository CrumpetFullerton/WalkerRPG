/*ItemData.cs
 * Created By: Phillip Buckreis 11/4/17
 * 
 * This script is for manipulating and storing data from the front-end
 * representation of our inventory and translating it to the back-end
 * representation.
 * 
 * For example, a player can drag and drop an item in the inventory, we
 * need to keep track of that change in the back-end.
 * 
 * 
 * Last Modified: 11/27/17
 */


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public int id;
    private InventoryDisplay inv;

    public void OnDrop(PointerEventData eventData)
    {
        if (!GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().Equipping)
        {
            //Get the item's data from the item prefab that is currently on the cursor
            ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();

            //When the player drops on an empty slot, we just zero out the item prefab's previous slot
            //and store the item prefab into the new slot.
            if (inv.inv.items[id].ID == -1)
            {
                inv.inv.items[droppedItem.slot] = new Item();
                inv.inv.items[id] = droppedItem.item;
                droppedItem.slot = id;
            }
            //if the item is dropped on a slot that already contains an item (and not it's own slot), we need to switch the two
            else if (droppedItem.slot != id)
            {
                //First we find out what item we're dropping on
                Transform item = this.transform.GetChild(0);
                item.GetComponent<ItemData>().slot = droppedItem.slot;
                item.transform.SetParent(inv.slots[droppedItem.slot].transform);
                item.transform.position = inv.slots[droppedItem.slot].transform.position;

                //Then we place our dropped item in the new slot
                droppedItem.slot = id;
                droppedItem.transform.SetParent(this.transform);
                droppedItem.transform.position = this.transform.position;

                //And put the dropped-on item into the dropped item's old slot
                inv.inv.items[item.GetComponent<ItemData>().slot] = item.GetComponent<ItemData>().item;
                inv.inv.items[id] = droppedItem.item;
            }
            //Save the inventory when the player moves stuff around
            inv.inv.SaveItems();
        }
    }

    // Use this for initialization
    void Start ()
    {
        //Get the front end representation
        inv = GameObject.Find("InventoryDisplay").GetComponent<InventoryDisplay>();
	}
}
