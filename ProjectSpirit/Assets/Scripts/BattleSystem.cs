using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }


public class BattleSystem : MonoBehaviour
{
    public Animator transition;
    public float TransitionTime = 1f;
    //public Unit[] players;
    //public BattleHUD[] playerHUDs;

    public List<Unit> players = new List<Unit>();
    //private int turnIndex;
    //public List <BattleHUD> playerHUDs = new List <BattleHUD> ();

    public GameObject playerPrefab1;
    public GameObject playerPrefab2;
    public GameObject enemyPrefab;

    public Transform player1BattleStation;
    public Transform player2BattleStation;
    public Transform enemyBattleStation;

    //Unit playerUnit;
    Unit enemyUnit;
    Unit activePlayer;

    public Text dialogueText;

    //public BattleHUD enemyHUD;

    public BattleState state;

    private Queue<Unit> turnOrder = new Queue<Unit>();

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    private void SetUpTurnOrder()
    {
        for (int i = 0; i < players.Count; i++)
        {
            turnOrder.Enqueue(players[i]);

        }
        //You would add another for loop for enemies if you want more
        turnOrder.Enqueue(enemyUnit);


        SetTurnState(turnOrder.Peek());
    }

    private void SetTurnState(Unit unit)
    {
        if (unit.gameObject.tag == "Enemy")
        {
            state = BattleState.ENEMYTURN;
        }
        else
        {
            state = BattleState.PLAYERTURN;
        }

        switch (state)
        {
            case BattleState.PLAYERTURN:
                PlayerTurn(unit);
                break;
            case BattleState.ENEMYTURN:
                TakeTurn();
                break;
        }

    }

    private void TakeTurn()
    {
        //if (turnOrder.Count == 0)
        //{
        //    SetUpTurnOrder();
        //}

        var unit = turnOrder.Dequeue();


        if (unit.gameObject.tag == "Enemy")
        {
            //state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());

        }
        else
        {
            //state = BattleState.PLAYERTURN;
            StartCoroutine(PlayerAttack(unit));

        }

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
        enemyUnit.HUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        //state = BattleState.PLAYERTURN;
        //PlayerTurn();

        SetUpTurnOrder();


    }

    IEnumerator PlayerAttack(Unit player)
    {
        bool isDead = enemyUnit.TakeDamage(player.damage);

        //enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "The attack is successful!";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));

        }
        else
        {

            if (turnOrder.Count > 0)
            {
                SetTurnState(turnOrder.Peek());
            }
            else
            {
                SetUpTurnOrder();
            }


            //TurnProcessor();
            //StartCoroutine(EnemyTurn());
        }
        // Change state based on what happened
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(1f);

        int index = Random.Range(0, players.Count);

        var player = players[index];

        //player.HUD.SetHP(player.currentHP);

        bool b = player.TakeDamage(enemyUnit.damage);
        dialogueText.text = "The attack is successful!";
        if (b)
        {
            var temp = player.gameObject;
            players.Remove(player);
            Destroy(temp);

        }

        bool isDead = b && players.Count == 0;

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            if (turnOrder.Count > 0)
            {
                SetTurnState(turnOrder.Peek());
            }
            else
            {
                SetUpTurnOrder();
            }
            //state = BattleState.PLAYERTURN;
            //PlayerTurn();
            //TurnProcessor();
        }

    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated.";
            SceneManager.LoadScene("GameOver");
        }


    }

    void PlayerTurn(Unit player)
    {
        activePlayer = player;

        dialogueText.text = "Choose an action";
        //StopAllCoroutines();
    }

    IEnumerator PlayerHeal(Unit player)
    {

        //TurnProcessor();
        //StartCoroutine(EnemyTurn());
        player.Heal(Random.Range(5, 12));
        //playerUnit.Heal(5);
        //activePlayer = player;
        //playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = "You feel renewed strength";

        yield return new WaitForSeconds(2f);

        if (turnOrder.Count > 0)
        {
            SetTurnState(turnOrder.Peek());
        }
        else
        {
            SetUpTurnOrder();
        }

        //state = BattleState.ENEMYTURN;
        //StartCoroutine(EnemyTurn());
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        //StartCoroutine(PlayerAttack(activePlayer));
        TakeTurn();
        //TurnProcessor();

    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        var unit = turnOrder.Dequeue();
        //StartCoroutine(PlayerAttack(unit));



        StartCoroutine(PlayerHeal(unit));
    }
    IEnumerator LoadLevel(int LevelIndex)
    {
        //play animation
        transition.SetTrigger("Start");

        //Wait
        yield return new WaitForSeconds(TransitionTime);

        //load scene

        SceneManager.LoadScene(LevelIndex);
        
    }
}
