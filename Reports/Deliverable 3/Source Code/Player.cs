using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Item Weapon1 = new Item();
    public Item Weapon2;
    public Item Weapon3;
    public Item Head;
    public Item Body;
    public Item Legs;

    public float PowerLevel;
    private int WeaponStrength;
    private int ArmorStrength;
    private int AP;
    private int Health;


    private void Awake()
    {
        //When we create the object, make sure it isn't destroyed when we switch scenes.
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start ()
    {
        AP = 3;
        GameObject.Find("SaveManager").GetComponent<SaveManager>().state.PowerLevel = PowerLevel;
    }
	
	// Update is called once per frame
	public void UpdateItemStats ()
    {
        Weapon1 = GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().Weapon1;
        Weapon2 = GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().Weapon1;
        Weapon3 = GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().Weapon1;
        Head = GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().Head;
        Body = GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().Body;
        Legs = GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().Legs;

        WeaponStrength = Weapon1.Strength + Weapon2.Strength + Weapon3.Strength;
        ArmorStrength = Head.Strength + Body.Strength + Legs.Strength;
        AP = Weapon1.APCost + Weapon2.APCost + Weapon3.APCost + Head.APCost + Body.APCost + Legs.APCost;

        Inventory inv = GameObject.Find("Inventory").GetComponent<Inventory>();

        PowerLevel = inv.PowerLevel;
        Health = Mathf.FloorToInt(100f * + ( 100f * (Mathf.FloorToInt(PowerLevel) - 1) * 0.05f));
    }
}
