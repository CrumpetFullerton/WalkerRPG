/*ItemData.cs
 * Created By: Phillip Buckreis 11/4/17
 * 
 * Creates tooltip when the player has their finger on the item.
 * The tooltip displays the item's stats and desctiption.
 * 
 * 
 * Last Modified: 11/27/17
 */ 
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{ 
    public Item item;
    public int amount;
    public int slot;

    private Transform originalParent;
    private InventoryDisplay inv;
    private Tooltip tooltip;
    private Vector2 offset;
    private Vector2 currPos;

    void Start()
    {
        //Grab the player's inventory and the tooltip component.
        inv = GameObject.Find("InventoryDisplay").GetComponent<InventoryDisplay>();
        tooltip = inv.GetComponent<Tooltip>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //We place the tooltip right where the player places their finger.
        offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
        currPos = Input.mousePosition;
        tooltip.Activate(item);
        if (GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().Equipping)
            GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().SelectorSlot = slot;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        //When player lifts their finger, don't show tooltip
        tooltip.Deactivate();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().Equipping)
        {
            //When the player drags an item, follow the cursor.
            if (item != null)
            {
                originalParent = this.transform.parent;
                this.transform.position = eventData.position - offset;
                this.transform.SetParent(this.transform.parent.parent);

                //Make sure there is no collision between the actual image and the representation of the image.
                GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().Equipping)
        {
            if (item != null)
            {
                //Have the item follow the player's finger
                this.transform.position = eventData.position - offset;
            }
        }
        if (System.Math.Sqrt(System.Math.Pow(currPos.x - Input.mousePosition.x, 2.0) + System.Math.Pow(currPos.y - Input.mousePosition.y, 2.0)) > 64.0)
        {
            //If the player gets out of a certain range, deactivate the tooltip.
            tooltip.Deactivate();
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().Equipping)
        {
            //At the end of the drag, we check it's position and snap it to the grid. Whatever was in the new position (empty or item) will go to the old position

            if (inv.inv.items[slot].Equipped == EquipmentHandler.Equipment.NONE && (this.transform.position.x > 700 && this.transform.position.y < 115))
            {

                this.item = new Item();
                inv.inv.items[slot] = new Item();
            }
            else
            {
                this.transform.SetParent(inv.slots[slot].transform);
                this.transform.position = inv.slots[slot].transform.position;
                GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
        }
    }

}
