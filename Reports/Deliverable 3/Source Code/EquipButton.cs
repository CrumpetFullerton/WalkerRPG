using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipButton : MonoBehaviour {

    public EquipmentHandler.Equipment EquipType;

    public void Clicked()
    {
        GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().Equipping = true;
        GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().SelectorEquip = EquipType;
        Application.LoadLevel("InventoryScreen");    }
}
