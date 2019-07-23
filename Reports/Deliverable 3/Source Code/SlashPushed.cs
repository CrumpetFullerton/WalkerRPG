using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashPushed : MonoBehaviour {

    public void Clicked()
    {
        GameObject.Find("CombatControll").GetComponent<BattleController>().SlashPressed = true;
    }
}
