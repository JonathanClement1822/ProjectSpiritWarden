using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }


public class BattleSystem : MonoBehaviour
{
    //public Unit[] players;
    //public BattleHUD[] playerHUDs;

    public List <Unit> players = new List <Unit> ();
    private int turnIndex;
    //public List <BattleHUD> playerHUDs = new List <BattleHUD> ();

    public GameObject playerPrefab1;
    public GameObject playerPrefab2;
    public GameObject enemyPrefab;

    public Transform player1BattleStation;
    public Transform player2BattleStation;
    public Transform enemyBattleStation;

    //Unit playerUnit;
    Unit enemyUnit;

    public Text dialogueText;

    public BattleHUD enemyHUD;

    public BattleState state;


    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    private void TurnProcessor()
    {
        if (turnIndex > 2)
        {

            turnIndex = 0;
            
        }
        Debug.Log(turnIndex);

        if (turnIndex == 0)
        {
            Debug.Log(turnIndex);
            state = BattleState.PLAYERTURN;

            StartCoroutine(PlayerAttack(players[0]));
            //Player 1 attack
        }
        else if (turnIndex == 1)
        {
            if (players.Count > 1)
            {
                Debug.Log(turnIndex);
                state = BattleState.PLAYERTURN;
                //Player 2 attack
                StartCoroutine(PlayerAttack(players[1]));

            }
            else
            {
                Debug.Log(turnIndex);
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());

            }

        }
        else if (turnIndex == 2)
        {
            Debug.Log(turnIndex);
            // enemy attack
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        else
        {
            Debug.Log("This Isn't Working as intended");
        }

        turnIndex++;

    }

    IEnumerator SetupBattle()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject player1GO = Instantiate(playerPrefab1, player1BattleStation);
        player1GO.transform.localPosition = Vector3.zero;
        Unit playerUnit1 = player1GO.GetComponent<Unit>();
        players.Add(playerUnit1);

        GameObject player2GO = Instantiate(playerPrefab2, player2BattleStation);
        player2GO.transform.localPosition = Vector3.zero;
        Unit playerUnit2 = player2GO.GetComponent<Unit>();
        players.Add(playerUnit2);


        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "A wild " + enemyUnit.unitName + " apporoaches...";

        Debug.Log(" 1- " + playerUnit1.name);
        Debug.Log(" 2- " + playerUnit1.HUD.name);

        playerUnit1.HUD.SetHUD(playerUnit1);
        playerUnit2.HUD.SetHUD(playerUnit2);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();

    }

    IEnumerator PlayerAttack(Unit player)
    {
       bool isDead = enemyUnit.TakeDamage(player.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "The attack is successful!";

        yield return new WaitForSeconds(2f);

        if(isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
            {

            TurnProcessor();
            //StartCoroutine(EnemyTurn());
            }
        // Change state based on what happened
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(1f);

        int index = Random.Range(0,players.Count);

        var player = players[index];

        player.HUD.SetHP(player.currentHP);

        bool b = player.TakeDamage(enemyUnit.damage);

        if (b)
        {
            var temp = player.gameObject;
            players.Remove(player);
            Destroy(temp);

        }

        bool isDead = b && players.Count == 0; 

        yield return new WaitForSeconds(1f);

        if(isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            //state = BattleState.PLAYERTURN;
            //PlayerTurn();
            TurnProcessor();
        }

    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated.";
        }


    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose an action";
    }

    IEnumerator PlayerHeal()
    {
        //playerUnit.Heal(5);

        //playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = "You feel renewed strength";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        //StartCoroutine(PlayerAttack());
        TurnProcessor();
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }
}
