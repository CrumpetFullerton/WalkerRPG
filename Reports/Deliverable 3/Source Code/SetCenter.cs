using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetCenter : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		this.GetComponent<Image>().sprite = GameObject.Find("Zone").GetComponent<ZoneHandler>().CurrentZone.Sprite;
    }

}
