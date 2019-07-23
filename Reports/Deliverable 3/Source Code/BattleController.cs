using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleController : MonoBehaviour {
    public Text turn;
    public Text PlayerHPText;
    public Text EnemyHPText;
    public Text WLTB;
    public bool SlashPressed = false;
    public bool StabPressed = false;
    public bool DefendPressed = false;
    public float damageMult = 1.0f;
    public int damage;
    public float EnemyPower;


    int playerHP;
    int enemyHP;
    //basePlayer player = new basePlayer();
    //player.baseHP = 100;


    bool PlayerTurn = true;
    // Use this for initialization
    void Start ()
    {

        EnemyPower = 1 + (GameObject.Find("Zone").GetComponent<ZoneHandler>().CurrentZone.Path - 1)*0.05f + (GameObject.Find("Zone").GetComponent<ZoneHandler>().CurrentZone.ID) * 0.1f;
        playerHP = 100; //* player.powerlevel
        enemyHP = Mathf.FloorToInt(100 * EnemyPower);
        PlayerHPText.text = "HP " + playerHP + " /100";
        EnemyHPText.text = "HP " + enemyHP + " /" + Mathf.FloorToInt(100 * EnemyPower).ToString();
        StartPlayerTurn();
        
    }
    

    // Update is called once per frame
    void Update ()
    {
        if(PlayerTurn)
        { //get buttonclicked         
            playerFight();
        }
        PlayerHPText.text = "HP " + playerHP +  " /100";
        EnemyHPText.text = "HP " + enemyHP +  " /" + Mathf.FloorToInt(100 * EnemyPower).ToString();
        GameObject.Find("APpannel").GetComponent<Image>().fillAmount = playerHP / 100.0f;
        GameObject.Find("APenemypannel").GetComponent<Image>().fillAmount = enemyHP / 100.0f;


    }

    void StartPlayerTurn()
    {
        turn.text = "Your Turn Pick an Attack";

    }
    void playerFight()
    {
        damage = 0;
        //int damageBonus = basePlayer.AP;
        if (SlashPressed)
        { 
            damage = Random.Range(3, 8); //+ damageBonus;
            SlashPressed = false;
            SwitchPlayers();
            damageMult = 1.2f;
        }
        else if(StabPressed)
        {
            damage = Random.Range(2, 4); //+ damageBonus;
            StabPressed = false;
            SwitchPlayers();
            damageMult = 1.0f;
        }
        else if(DefendPressed)
        {
            damageMult = 0.4f;
            DefendPressed = false;
            SwitchPlayers();
        }
        enemyHP -= damage;
        if (enemyHP <= 0)
        {   enemyHP = 0;//win
            GameObject.Find("Inventory").GetComponent<Inventory>().gold += 50 * (Mathf.FloorToInt(
                (GameObject.Find("Zone").GetComponent<ZoneHandler>().CurrentZone.Path - 1) * 0.05f + (GameObject.Find("Zone").GetComponent<ZoneHandler>().CurrentZone.ID) * 0.1f));
            turn.text = "YOU WON!";
            StartCoroutine (WinPause());
            //Application.LoadLevel ("StepScreen");
            SceneManager.LoadScene("StepScreen");
        }


        }
    void SwitchPlayers()
    {

        PlayerTurn = !PlayerTurn;
        if (PlayerTurn) StartPlayerTurn();
        else startEnemyTurn();
    }
    IEnumerator WinPause()
    {
        yield return new WaitForSeconds(2);
    }

    void startEnemyTurn()
    {
        turn.text = "Enemy's Turn";
        StartCoroutine(enemyPause());
    }
    IEnumerator enemyPause()
    {
        yield return new WaitForSeconds(Random.Range(1, 2));
        EnemyFight();
        SwitchPlayers();
    }
    void EnemyFight()
    {
        damageMult = damageMult *EnemyPower;
        damage = Mathf.FloorToInt(Random.Range(3, 5) *damageMult);
        playerHP = playerHP -damage;

        if (playerHP <= 0)
        {    playerHP = 0;
        GameObject.Find("Inventory").GetComponent<Inventory>().gold += 10;
        turn.text = "YOU LOST!";
        StartCoroutine(WinPause());
        SceneManager.LoadScene("StepScreen");
        }
    }
}
