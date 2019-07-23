/*AddItem.cs
 * Created By: Phillip Buckreis 11/5/17
 * 
 * This script simply adds an item to the player's inventory when the player
 * clicks a button
 * 
 * Last Modified: 11/25/17
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddItem : MonoBehaviour
{
    public int cost;
    public Inventory inv;
    public GameObject Button;


	// Use this for initialization
    public void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        cost = Mathf.FloorToInt(inv.PowerLevel) * 100;
        Button = GameObject.Find("btn_AddItem");

        Button.transform.Find("txt_AddItem").GetComponent<Text>().text = "Buy Item\n" + cost;
    }
	public void Clicked()
    {
        if (inv.gold >= cost)
        {
            //Add an item. The range represents the types of items the player can recieve
            inv.AddItem(Random.Range(0, 12));

            //Get the new power level of the player
            inv.getPowerLevel();

            //Save inventory after getting a new item
            inv.SaveItems();

            inv.gold -= cost;
        }
        cost = Mathf.FloorToInt(inv.PowerLevel) * 100;
        Button.transform.Find("txt_AddItem").GetComponent<Text>().text = "Buy Item\n" + cost;
    }

    public void Update()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        cost = Mathf.FloorToInt(inv.PowerLevel) * 100;
        Button = GameObject.Find("btn_AddItem");

        Button.transform.Find("txt_AddItem").GetComponent<Text>().text = "Buy Item\n" + cost;
    }
}
