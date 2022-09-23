using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Collider2D collider;
    private BattleManager battleManager;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        battleManager = FindObjectOfType<BattleManager>();
    }
    private void OnMouseUpAsButton()
    {
        if (battleManager.state == BattleState.PLAYERSELECT)
        {
            if(gameObject.transform.position == battleManager.enemyTransform1.position)
            {
                battleManager.selectedEnemy = battleManager.enemyGO1.GetComponent<Unit>();
                battleManager.PlayerAction();
            }
            else if(gameObject.transform.position == battleManager.enemyTransform2.position)
            {
                battleManager.selectedEnemy = battleManager.enemyGO2.GetComponent<Unit>();
                battleManager.PlayerAction();
            }
            else if(gameObject.transform.position == battleManager.enemyTransform3.position)
            {
                battleManager.selectedEnemy = battleManager.enemyGO3.GetComponent<Unit>();
                battleManager.PlayerAction();
            }
            else if(gameObject.transform.position == battleManager.enemyTransform4.position)
            {
                battleManager.selectedEnemy = battleManager.enemyGO4.GetComponent<Unit>();
                battleManager.PlayerAction();
            }
            else if(gameObject.transform.position == battleManager.enemyTransform5.position)
            {
                battleManager.selectedEnemy = battleManager.enemyGO5.GetComponent<Unit>();
                battleManager.PlayerAction();
            }
            else
            {
                Debug.Log("errore");
                return;
            }
        }
    }
}
