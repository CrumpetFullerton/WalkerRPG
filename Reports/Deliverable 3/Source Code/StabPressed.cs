using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabPressed : MonoBehaviour {

    public void Clicked()
    {
        GameObject.Find("CombatControll").GetComponent<BattleController>().StabPressed = true;
    }

}
