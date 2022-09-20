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

    [SerializeField] Text playerUIName;
    [SerializeField] Text enemyUIName;

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

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    private void PlayerTurn()
    {

    }
    
    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.attackDamage);

        enemyHUD.SetHpAndMana(enemyUnit.currentHp, enemyUnit.currentMana);

        //Debug.Log(enemyUnit.currentHp);

        yield return new WaitForSeconds(2f);

        Debug.Log("Suca forte");

        if(isDead)
        {
            state = BattleState.WON;
        }
        else
        {
            state = BattleState.ENEMYTURN;
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
