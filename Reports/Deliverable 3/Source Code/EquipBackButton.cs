using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipBackButton : MonoBehaviour {

    public void Clicked()
    {
        GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().Equipping = false;
        GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().SelectorSlot = -1;
        GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().SelectorEquip = EquipmentHandler.Equipment.NONE;
        Application.LoadLevel("CharacterScreen");
    }
}
