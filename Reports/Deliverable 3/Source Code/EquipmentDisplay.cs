using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentDisplay : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        if (GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().Weapon1.ID != -1)
        {
            GameObject.Find("img_Weapon1").SetActive(true);
            GameObject.Find("img_Weapon1").GetComponent<Image>().sprite = GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().Weapon1.Sprite;
        }
        else
            GameObject.Find("img_Weapon1").SetActive(false);


        if (GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().Weapon2.ID != -1)
        {
            GameObject.Find("img_Weapon2").SetActive(true);
            GameObject.Find("img_Weapon2").GetComponent<Image>().sprite = GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().Weapon2.Sprite;
        }
        else
            GameObject.Find("img_Weapon2").SetActive(false);


        if (GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().Weapon3.ID != -1)
        {
            GameObject.Find("img_Weapon3").SetActive(true);
            GameObject.Find("img_Weapon3").GetComponent<Image>().sprite = GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().Weapon3.Sprite;
        }
        else
            GameObject.Find("img_Weapon3").SetActive(false);


        if (GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().Head.ID != -1)
        {
            GameObject.Find("img_Head").SetActive(true);
            GameObject.Find("img_Head").GetComponent<Image>().sprite = GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().Head.Sprite;
        }
        else
            GameObject.Find("img_Head").SetActive(false);


        if (GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().Body.ID != -1)
        {
            GameObject.Find("img_Body").SetActive(true);
            GameObject.Find("img_Body").GetComponent<Image>().sprite = GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().Body.Sprite;
        }
        else
            GameObject.Find("img_Body").SetActive(false);

        if (GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().Legs.ID != -1)
        {
            GameObject.Find("img_Legs").SetActive(true);
            GameObject.Find("img_Legs").GetComponent<Image>().sprite = GameObject.Find("EquipmentHandler").GetComponent<EquipmentHandler>().Legs.Sprite;
        }
        else
            GameObject.Find("img_Legs").SetActive(false);

        Inventory inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        GameObject PowerLevelText = GameObject.Find("txt_PowerLevel");
        PowerLevelText.GetComponent<Text>().text = "◆" + Mathf.FloorToInt(inv.PowerLevel).ToString();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
