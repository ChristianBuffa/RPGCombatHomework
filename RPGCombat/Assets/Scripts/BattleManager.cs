using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERSELECT, PLAYERACTION, ENEMYTURN, WON, LOST };

public class BattleManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab1;
    [SerializeField] GameObject playerPrefab2;
    [SerializeField] GameObject playerPrefab3;
    [SerializeField] GameObject playerPrefab4;
    [SerializeField] GameObject playerPrefab5;

    [SerializeField] GameObject enemyPrefab1;
    [SerializeField] GameObject enemyPrefab2;
    [SerializeField] GameObject enemyPrefab3;
    [SerializeField] GameObject enemyPrefab4;
    [SerializeField] GameObject enemyPrefab5;

    [SerializeField] Transform playerTransform1;
    [SerializeField] Transform playerTransform2;
    [SerializeField] Transform playerTransform3;
    [SerializeField] Transform playerTransform4;
    [SerializeField] Transform playerTransform5;

    [SerializeField] Transform enemyTransform1;
    [SerializeField] Transform enemyTransform2;
    [SerializeField] Transform enemyTransform3;
    [SerializeField] Transform enemyTransform4;
    [SerializeField] Transform enemyTransform5;

    [SerializeField] Text displayText;

    [SerializeField] Text abilityOneText;
    [SerializeField] Text abilityTwoText;
    [SerializeField] Text abilityThreeText;
    [SerializeField] Text abilityFourText;

    [SerializeField] BattleHUD playerHUD1;
    [SerializeField] BattleHUD playerHUD2;
    [SerializeField] BattleHUD playerHUD3;
    [SerializeField] BattleHUD playerHUD4;
    [SerializeField] BattleHUD playerHUD5;
    [SerializeField] BattleHUD enemyHUD1;
    [SerializeField] BattleHUD enemyHUD2;
    [SerializeField] BattleHUD enemyHUD3;
    [SerializeField] BattleHUD enemyHUD4;
    [SerializeField] BattleHUD enemyHUD5;

    private Unit playerUnit;
    [HideInInspector] public Unit enemyUnit;

    GameObject playerGO1;
    GameObject playerGO2;
    GameObject playerGO3;
    GameObject playerGO4;
    GameObject playerGO5;

    GameObject enemyGO1;
    GameObject enemyGO2;
    GameObject enemyGO3;
    GameObject enemyGO4;
    GameObject enemyGO5;

    public BattleState state;

    private void Start()
    {
        state = BattleState.START;

        StartCoroutine(SetUpBattle());
    }

    IEnumerator SetUpBattle()
    {
        playerGO1 = Instantiate(playerPrefab1, playerTransform1);
        playerGO2 = Instantiate(playerPrefab2, playerTransform2);
        playerGO3 = Instantiate(playerPrefab3, playerTransform3);
        playerGO4 = Instantiate(playerPrefab4, playerTransform4);
        playerGO5 = Instantiate(playerPrefab5, playerTransform5);

        enemyGO1 = Instantiate(enemyPrefab1, enemyTransform1);
        enemyGO2 = Instantiate(enemyPrefab2, enemyTransform2);
        enemyGO3 = Instantiate(enemyPrefab3, enemyTransform3);
        enemyGO4 = Instantiate(enemyPrefab4, enemyTransform4);
        enemyGO5 = Instantiate(enemyPrefab5, enemyTransform5);

        enemyUnit = enemyGO1.GetComponent<Unit>();

        playerHUD1.SetHUD(playerGO1.GetComponent<Unit>());
        playerHUD2.SetHUD(playerGO2.GetComponent<Unit>());
        playerHUD3.SetHUD(playerGO3.GetComponent<Unit>());
        playerHUD4.SetHUD(playerGO4.GetComponent<Unit>());
        playerHUD5.SetHUD(playerGO5.GetComponent<Unit>());
        enemyHUD1.SetHUD(enemyGO1.GetComponent<Unit>());
        enemyHUD2.SetHUD(enemyGO2.GetComponent<Unit>());
        enemyHUD3.SetHUD(enemyGO3.GetComponent<Unit>());
        enemyHUD4.SetHUD(enemyGO4.GetComponent<Unit>());
        enemyHUD5.SetHUD(enemyGO5.GetComponent<Unit>());

        displayText.text = "Setting up the battle...";

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERSELECT;
        PlayerEnemySelect();
    }

    private void PlayerEnemySelect()
    {
        int turnCounter = 1;

        if (turnCounter == 1)
            playerUnit = playerGO1.GetComponent<Unit>();
        else if (turnCounter == 2)
            playerUnit = playerGO2.GetComponent<Unit>();
        else if (turnCounter == 3)
            playerUnit = playerGO3.GetComponent<Unit>();
        else if (turnCounter == 4)
            playerUnit = playerGO4.GetComponent<Unit>();
        else if (turnCounter == 5)
            playerUnit = playerGO5.GetComponent<Unit>();

        //select enemy
        displayText.text = "select an enemy";

        turnCounter++;

        if (turnCounter > 5)
        {
            turnCounter = 1;
            Debug.Log(turnCounter);
        }

        PlayerAction();
    }

    private void PlayerAction()
    {
        state = BattleState.PLAYERACTION;

        if (playerUnit.abilityOne != null)
            abilityOneText.text = playerUnit.abilityOne.abilityName;

        if (playerUnit.abilityTwo != null)
            abilityTwoText.text = playerUnit.abilityTwo.abilityName;

        if (playerUnit.abilityThree != null)
            abilityThreeText.text = playerUnit.abilityThree.abilityName;

        if (playerUnit.abilityFour != null)
            abilityFourText.text = playerUnit.abilityFour.abilityName;

        displayText.text = "chose an action...";
    }
    
    IEnumerator PlayerAbility(AbilityData ability)
    {
        if (ability.abilityType == AbilityType.ATTACK)
        {
            bool isDead = enemyUnit.TakeDamage(ElementCheck(ability, enemyUnit));

            enemyHUD1.SetHpAndMana(enemyUnit.currentHp, enemyUnit.currentMana);

            Debug.Log(ElementCheck(ability, enemyUnit));

            yield return new WaitForSeconds(2f);

            if (isDead)
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
        else if(ability.abilityType == AbilityType.HEAL)
        {
            if (playerUnit.currentHp < playerUnit.currentMana)
            {
                playerUnit.Heal(ability.abilityHealCapacity);

                displayText.text = "healed!";

                yield return new WaitForSeconds(2f);

                StartCoroutine(EnemyTurn());
            }
            else
            {
                displayText.text = "hp already at max";

                yield return new WaitForSeconds(2f);

                displayText.text = "chose an action...";
            }
        }
    }

    IEnumerator EnemyTurn()
    {
        float randNumber = Random.Range(1, 4);

        displayText.text = "enemy " + enemyUnit.unitName + "'s turn..."; 

        yield return new WaitForSeconds(1f);

        int abilitySelection = ((int)randNumber);

        bool isDead = playerUnit.TakeDamage(ElementCheck(enemyUnit.abilityOne, playerUnit));

        playerHUD1.SetHpAndMana(playerUnit.currentHp, playerUnit.currentMana);

        yield return new WaitForSeconds(2f);

        if(isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERACTION;
            PlayerAction();
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

    public void AbilityOneButton()
    {
        if (playerUnit.abilityOne != null)
        {
            if (state != BattleState.PLAYERACTION)
            {
                return;
            }
            else
            {
                if (playerUnit.abilityOne != null)
                {
                    StartCoroutine(PlayerAbility(playerUnit.abilityOne));
                }
                else
                {
                    return;
                }
            }
        }
        else
        {
            return;
        }
    }
    public void AbilityTwoButton()
    {
        if (playerUnit.abilityTwo != null)
        {
            if (state != BattleState.PLAYERACTION)
            {
                return;
            }
            else
            {
                if (playerUnit.abilityOne != null)
                {
                    StartCoroutine(PlayerAbility(playerUnit.abilityTwo));
                }
                else
                {
                    return;
                }
            }
        }
        else
        {
            return;
        }
    }

    public void AbilityThreeButton()
    {
        if (playerUnit.abilityThree != null)
        {
            if (state != BattleState.PLAYERACTION)
            {
                return;
            }
            else
            {
                if (playerUnit.abilityOne != null)
                {
                    StartCoroutine(PlayerAbility(playerUnit.abilityThree));
                }
                else
                {
                    return;
                }
            }
        }
        else
        {
            return;
        }
    }

    public void AbilityFourButton()
    {
        if (playerUnit.abilityFour != null)
        {
            if (state != BattleState.PLAYERACTION)
            {
                return;
            }
            else
            {
                if (playerUnit.abilityOne != null)
                {
                    StartCoroutine(PlayerAbility(playerUnit.abilityFour));
                }
                else
                {
                    return;
                }
            }
        }
        else
        {
            return;
        }
    }

    private int ElementCheck(AbilityData ability, Unit damagedUnit)
    {
        int damage;
        float calculatedDamage;

        if(ability.abilityElementType == damagedUnit.unitStrongAgainstElement)
        {
            calculatedDamage = ability.abilityDamage * ability.weakToElementModifier;

            displayText.text = "attack is not very effective...";
        }
        else if(ability.abilityElementType == damagedUnit.unitWeakToElement)
        {
            calculatedDamage = ability.abilityDamage * ability.strongAgainstElementMultiplier;

            displayText.text = "attack is very effective!";
        }
        else
        {
            calculatedDamage = ability.abilityDamage;

            displayText.text = "attacked";
        }

        damage = ((int)calculatedDamage);

        return damage;
    }
}
