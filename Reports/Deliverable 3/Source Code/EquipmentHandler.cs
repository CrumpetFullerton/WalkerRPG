using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentHandler : MonoBehaviour {


    public Item Weapon1;
    public Item Weapon2;
    public Item Weapon3;
    public Item Head;
    public Item Body;
    public Item Legs;

    public bool Equipping = false;
    public int SelectorSlot = -1;
    public Equipment SelectorEquip = Equipment.NONE;
    public ItemDatabase.ItemType SelectType = ItemDatabase.ItemType.WEAPON;

    public Inventory inv;

    public enum Equipment
    {
        NONE,
        WEAPON1,
        WEAPON2,
        WEAPON3,
        HEAD,
        BODY,
        LEGS
    }


    private void Awake()
    {
        //When we create the object, make sure it isn't destroyed when we switch scenes.
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start ()
    {
        Weapon1 = new Item();
        Weapon2 = new Item();
        Weapon3 = new Item();
        Head = new Item();
        Body = new Item();
        Legs = new Item();

        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
    }
	
	// Update is called once per frame
	public void Equip (Item item, Equipment equipType)
    {
		switch (equipType)
        {
            case Equipment.BODY:
                Body.Equipped = EquipmentHandler.Equipment.NONE;
                Body = new Item();
                Body = item;
                Body.Equipped = EquipmentHandler.Equipment.BODY;
                Debug.Log(item.Title);
                break;
            case Equipment.HEAD:
                Head.Equipped = EquipmentHandler.Equipment.NONE;
                Head = new Item();
                Head = item;
                Head.Equipped = EquipmentHandler.Equipment.HEAD;
                break;
            case Equipment.LEGS:
                Legs.Equipped = EquipmentHandler.Equipment.NONE;
                Legs = new Item();
                Legs = item;
                Legs.Equipped = EquipmentHandler.Equipment.LEGS;
                break;
            case Equipment.WEAPON1:
                Weapon1.Equipped = EquipmentHandler.Equipment.NONE;
                Weapon1 = new Item();
                Weapon1 = item;
                Weapon1.Equipped = EquipmentHandler.Equipment.WEAPON1;
                break;
            case Equipment.WEAPON2:
                Weapon2.Equipped = EquipmentHandler.Equipment.NONE;
                Weapon2 = new Item();
                Weapon2 = item;
                Weapon2.Equipped = EquipmentHandler.Equipment.WEAPON2;
                break;
            case Equipment.WEAPON3:
                Weapon3.Equipped = EquipmentHandler.Equipment.NONE;
                Weapon3 = new Item();
                Weapon3 = item;
                Weapon3.Equipped = EquipmentHandler.Equipment.WEAPON3;
                break;
        }
	}

    public bool validEquip(ItemDatabase.ItemType itemType)
    {
        if (itemType == ItemDatabase.ItemType.HEAD && SelectorEquip == Equipment.HEAD ||
            itemType == ItemDatabase.ItemType.BODY && SelectorEquip == Equipment.BODY ||
            itemType == ItemDatabase.ItemType.LEGS && SelectorEquip == Equipment.LEGS ||
            itemType == ItemDatabase.ItemType.WEAPON && SelectorEquip == Equipment.WEAPON1 ||
            itemType == ItemDatabase.ItemType.WEAPON && SelectorEquip == Equipment.WEAPON2 ||
                itemType == ItemDatabase.ItemType.WEAPON && SelectorEquip == Equipment.WEAPON3 ||
                SelectorEquip == Equipment.NONE)
            return true;
        else
            return false;
    }

    public void Select()
    {
        if (SelectorSlot != -1)
        {
            Equip(inv.items[SelectorSlot], SelectorEquip);
            GameObject.Find("PlayerHandle").GetComponent<Player>().UpdateItemStats();
        }
    }
}
