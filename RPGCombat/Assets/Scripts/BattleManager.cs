using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST };

public class BattleManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] Transform playerTransform;
    [SerializeField] Transform enemyTransform;

    [SerializeField] Text displayText;

    [SerializeField] BattleHUD playerHUD;
    [SerializeField] BattleHUD enemyHUD;

    private Unit playerUnit;
    private Unit enemyUnit;

    public BattleState state;

    private void Start()
    {
        state = BattleState.START;

        StartCoroutine(SetUpBattle());
    }

    IEnumerator SetUpBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerTransform);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyTransform);
        enemyUnit = enemyGO.GetComponent<Unit>();

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        displayText.text = "Setting up the battle...";

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    private void PlayerTurn()
    {
        displayText.text = "chose an action...";
    }
    
    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.attackDamage);

        enemyHUD.SetHpAndMana(enemyUnit.currentHp, enemyUnit.currentMana);

        displayText.text = "attacked";

        //Debug.Log(enemyUnit.currentHp);

        yield return new WaitForSeconds(2f);

        if(isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        displayText.text = enemyUnit.unitName + "'s turn..."; 

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.attackDamage);

        playerHUD.SetHpAndMana(playerUnit.currentHp, playerUnit.currentMana);

        displayText.text = enemyUnit.unitName + " attacked!";

        yield return new WaitForSeconds(1f);

        if(isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    private void EndBattle()
    {
        if(state == BattleState.WON)
        {
            displayText.text = "You won!";
        }
        else if(state == BattleState.LOST)
        {
            displayText.text = "You lost...";
        }
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        else
        {
            StartCoroutine(PlayerAttack());
        }
    }

}
