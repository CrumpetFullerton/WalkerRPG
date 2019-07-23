/*RadialLoadingBar.cs
 * Created By: Phillip Buckreis 11/4/17
 * 
 * This creates a radial loading bar with information of the zone and path.
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialLoadingBar : MonoBehaviour {

    public Transform LoadingBar;
    public Transform Percent;
    public Transform ZonePath;
    public Transform PathTotal;
    public Zone Zone;

    [SerializeField] float currentAmount;
    [SerializeField] float speed;

	// Use this for initialization
	void Start ()
    {
        //Set Zone to the zone object in the scene
        Zone = GameObject.Find("Zone").GetComponent<ZoneHandler>().CurrentZone;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Update the path graphic to show progress, current zone and path.
        currentAmount = Zone.findPercent();
        if (currentAmount < 100)
        {
            Percent.GetComponent<Text>().text = currentAmount.ToString("f1") + "%";
            ZonePath.GetComponent<Text>().text = (Zone.ID + 1) + " - " + Zone.Path;
            PathTotal.GetComponent<Text>().text = Zone.StepsUntilNextPath.ToString("f0");
        }
        else
        {
            Percent.GetComponent<Text>().text = "100%";
        }

        LoadingBar.GetComponent<Image>().fillAmount = currentAmount / 100;
	}
}
