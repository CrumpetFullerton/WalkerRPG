using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendPressed : MonoBehaviour {

    public void Clicked()
    {
        GameObject.Find("CombatControll").GetComponent<BattleController>().DefendPressed = true;
    }
}
