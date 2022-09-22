using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
        bool isDead = enemyUnit.TakeDamage(ElementCheck(playerUnit, enemyUnit));

        enemyHUD.SetHpAndMana(enemyUnit.currentHp, enemyUnit.currentMana);

        Debug.Log(ElementCheck(playerUnit, enemyUnit));

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

        displayText.text = enemyUnit.unitName + " attacked!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(ElementCheck(enemyUnit, playerUnit));

        playerHUD.SetHpAndMana(playerUnit.currentHp, playerUnit.currentMana);

        yield return new WaitForSeconds(2f);

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

    private int ElementCheck(Unit attackingUnit, Unit damagedUnit)
    {
        int damage;
        float calculatedDamage;

        if(attackingUnit.unitElement == damagedUnit.unitStrongAgainstElement)
        {
            calculatedDamage = attackingUnit.attackDamage * attackingUnit.unitWeakToElementModifier;

            displayText.text = "attack is not very effective...";
        }
        else if(attackingUnit.unitElement == damagedUnit.unitWeakToElement)
        {
            calculatedDamage = attackingUnit.attackDamage * attackingUnit.unitStrongAgainstModifier;

            displayText.text = "attack is very effective!";
        }
        else
        {
            calculatedDamage = attackingUnit.attackDamage;

            displayText.text = "attacked";
        }

        damage = ((int)calculatedDamage);

        return damage;
    }
}
