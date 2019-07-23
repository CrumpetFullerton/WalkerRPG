/*AddArmor.cs
 * Created By: Phillip Buckreis 11/27/17
 * 
 * This script simply adds an armor to the player's inventory when the player
 * clicks a button
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddArmor : MonoBehaviour
{
    public int cost;
    public Inventory inv;
    public GameObject Button;


    // Use this for initialization
    public void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        cost = Mathf.FloorToInt(inv.PowerLevel) * 150;
        Button = GameObject.Find("btn_AddArmor");

        Button.transform.Find("txt_AddArmor").GetComponent<Text>().text = "Buy Armor\n" + cost;
    }

    public void Clicked()
    {
        if (inv.gold >= cost)
        {
            //Add an item. The range represents the types of items the player can recieve
            inv.AddItem(Random.Range(9, 12));

            //Get the new power level of the player
            inv.getPowerLevel();

            //Save inventory after getting a new item
            inv.SaveItems();

            inv.gold -= cost;
        }
        cost = Mathf.FloorToInt(inv.PowerLevel) * 150;
        Button.transform.Find("txt_AddArmor").GetComponent<Text>().text = "Buy Armor\n" + cost;
    }

    public void Update()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        cost = Mathf.FloorToInt(inv.PowerLevel) * 150;
        Button = GameObject.Find("btn_AddArmor");

        Button.transform.Find("txt_AddArmor").GetComponent<Text>().text = "Buy Armor\n" + cost;
    }
}
